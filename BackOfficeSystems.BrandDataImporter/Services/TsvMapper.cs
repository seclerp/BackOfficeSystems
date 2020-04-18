using System.Collections.Generic;
using System.Linq;
using BackOfficeSystems.BrandDataImporter.Contracts;
using BackOfficeSystems.BrandDataImporter.Models;

namespace BackOfficeSystems.BrandDataImporter.Services
{
    // We could use AutoMapper here, but for one case mapping it is redundant
    public class TsvMapper : ITsvMapper
    {
        private IEnumerable<TableColumnValue> MapFrom(IEnumerable<TsvColumnValue> columns, Dictionary<string, string> columnMappings) =>
            columns.Select(column =>
                new TableColumnValue
                {
                    // If mapping not found, use TSV column name as a database column
                    Header = columnMappings.ContainsKey(column.Header) ? columnMappings[column.Header] : column.Header,
                    Value = column.Value
                });

        public IEnumerable<TableRow> MapFrom(IEnumerable<TsvRow> rows,
            Dictionary<string, Dictionary<string, string>> mappings) =>
            rows.Select(row => new TableRow
            {
                TableName = row.TableName,
                Columns = MapFrom(row.Columns, mappings[row.TableName]).ToArray()
            });
    }
}