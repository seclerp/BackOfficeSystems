using System.Collections.Generic;
using System.Data;
using System.Linq;
using BackOfficeSystems.BrandDataImporter.Contracts;
using Dapper;
using MySql.Data.MySqlClient;

namespace BackOfficeSystems.BrandDataImporter.Services
{
    /// <summary>
    /// Implementation of <see cref="ITableInfoProvider"/> that provides data about MySQL tables
    /// </summary>
    public class TableInfoProvider : ITableInfoProvider
    {
        private const string ColumnTypesQuery =
            @"SELECT COLUMN_NAME AS 'Key', DATA_TYPE AS 'Value' FROM INFORMATION_SCHEMA.COLUMNS
              WHERE table_name = @TableName;";

        private readonly MySqlConnection _connection;

        /// <param name="connection">Opened MySQL connection</param>
        public TableInfoProvider(MySqlConnection connection)
        {
            _connection = connection;
        }

        public Dictionary<string, string> GetColumnTypes(string tableName, IDbTransaction? transaction = null) =>
            _connection
                .Query<KeyValuePair<string, string>>(ColumnTypesQuery, new { TableName = tableName }, transaction)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
    }
}