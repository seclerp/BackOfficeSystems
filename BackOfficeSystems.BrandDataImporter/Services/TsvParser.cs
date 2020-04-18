using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BackOfficeSystems.BrandDataImporter.Contracts;
using BackOfficeSystems.BrandDataImporter.Exceptions;
using BackOfficeSystems.BrandDataImporter.Models;
using CsvHelper;
using CsvHelper.Configuration;

namespace BackOfficeSystems.BrandDataImporter.Services
{
    public class TsvParser : ITsvParser
    {
        private CsvConfiguration _configuration;

        public TsvParser()
        {
            _configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Use custom tab delimiter to read TSV files
                Delimiter = "\t"
            };
        }

        public IEnumerable<TsvRow> FromStream(string tableName, Stream stream)
        {
            using var reader = new StreamReader(stream);
            using var csv = new CsvParser(reader, _configuration);

            var rows = new List<TsvRow>();
            var headers = csv.Read();

            // Not using .Select here, because of limitations of low level CsvParser (it need to be iterated manually)
            string[] record;
            while ((record = csv.Read()) != null)
            {
                if (record.Length != headers.Length)
                {
                    throw new TsvValidationException(
                        $"Incorrect number of columns in row, expected {headers.Length}, got {record.Length}, row {csv.FieldReader.Context.Row}"
                    );
                }
                // Assume that we have validated record here, use indices
                // Also it could be done using .Zip, but with less performance
                var columns =
                    headers
                        .Select((columnName, index) =>
                            new TsvColumnValue() {Header = columnName, Value = record[index]})
                        .ToArray();

                var row = new TsvRow
                {
                    TableName = tableName,
                    Columns = columns
                };

                rows.Add(row);
            }

            return rows;
        }
    }
}