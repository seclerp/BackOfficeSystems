using System;
using System.Collections.Generic;
using System.Linq;
using BackOfficeSystems.BrandDataImporter.Contracts;
using BackOfficeSystems.BrandDataImporter.DataTransformers;
using BackOfficeSystems.BrandDataImporter.Models;
using Dapper;
using MySql.Data.MySqlClient;
using Serilog;

namespace BackOfficeSystems.BrandDataImporter.Services
{
    /// <summary>
    /// Implementation of <see cref="IDatabaseImportService"/> that import data into MySQL database
    /// </summary>
    public class MySqlDatabaseImportService : IDisposable, IDatabaseImportService
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly MySqlCommandBuilder _commandBuilder;
        private readonly ITableInfoProvider _tableInfoProvider;
        private readonly Dictionary<string, ITransformer> _transformers;
        private readonly ITransformer _defaultTransformer;

        /// <param name="connectionString">MySQL connection string</param>
        /// <param name="transformers">Transformers that need to be used with raw string values</param>
        /// <param name="defaultTransformer">Default transformer used to transform data when transform for type is not found</param>
        public MySqlDatabaseImportService(string connectionString, Dictionary<string, ITransformer>? transformers = null, ITransformer? defaultTransformer = null)
        {
            _mySqlConnection = new MySqlConnection(connectionString);
            _mySqlConnection.Open();
            _commandBuilder = new MySqlCommandBuilder();
            _tableInfoProvider = new TableInfoProvider(_mySqlConnection);

            _transformers = transformers ??
                            new Dictionary<string, ITransformer>()
                            {
                                {"timestamp", new DateTimeTransformer()},
                                {"datetime", new DateTimeTransformer()}
                            };
            _defaultTransformer = defaultTransformer ?? new StringTransformer();
        }

        public void EnsureDatabaseCreated(string schema, string databaseName)
        {
            using var transaction = _mySqlConnection.BeginTransaction();

            var quotedDatabaseName = _commandBuilder.QuoteIdentifier(databaseName);

            _mySqlConnection.Execute($"CREATE DATABASE IF NOT EXISTS {quotedDatabaseName}", null, transaction);
            _mySqlConnection.Execute($"USE {quotedDatabaseName}", null, transaction);
            _mySqlConnection.Execute(schema, null, transaction);

            transaction.Commit();
        }

        public void ImportFromFile(TsvFile file, string databaseName)
        {
            using var transaction = _mySqlConnection.BeginTransaction();

            var tableColumnsTypes = _tableInfoProvider.GetColumnTypes(file.ShortName, transaction);
            var query = MakeInsertQuery(file.ShortName, file.Headers, databaseName);

            Log.Information("{Query}", query);

            foreach (var row in file.Rows)
            {
                var parameters = new DynamicParameters();

                for (var columnIndex = 0; columnIndex < row.Length; columnIndex++)
                {
                    var paramName = $"@Value{columnIndex}";
                    var transformer =  DetermineTransformer(tableColumnsTypes, file.Headers[columnIndex]);
                    var transformedValue = transformer.Transform(row[columnIndex]);
                    parameters.Add(paramName, transformedValue);
                }

                _mySqlConnection.Execute(query, parameters, transaction);
            }

            transaction.Commit();
        }

        public void Dispose()
        {
            _mySqlConnection?.Dispose();
        }

        private ITransformer DetermineTransformer(Dictionary<string, string> columnTypes, string columnName) =>
            _transformers.ContainsKey(columnTypes[columnName])
                ? _transformers[columnTypes[columnName]]
                : _defaultTransformer;

        private string MakeInsertQuery(string tableName, string[] columns, string databaseName)
        {
            // QuoteIdentifier is used to Protect from SQL injections
            var quotedDatabaseName = _commandBuilder.QuoteIdentifier(databaseName);
            var quotedTableName = _commandBuilder.QuoteIdentifier(tableName);
            var quotedColumnsNames = string.Join(", ", columns.Select(_commandBuilder.QuoteIdentifier));
            var valuesParameters = string.Join(", ", Enumerable.Range(0, columns.Length).Select(number => $"@Value{number}"));

            return $"INSERT INTO {quotedDatabaseName}.{quotedTableName} ({quotedColumnsNames}) VALUES ({valuesParameters})";
        }
    }
}