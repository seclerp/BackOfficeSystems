using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BackOfficeSystems.BrandDataImporter.Services
{
    public class TsvLoader
    {
        private readonly MappingsLoader _mappingsLoader;
        private readonly TsvParser _tsvParser;
        private readonly TsvMapper _tsvMapper;

        // We could use dependency injection here, but I would like to keep implementation simple and generic as possible
        public TsvLoader()
        {
            _mappingsLoader = new MappingsLoader();
            _tsvParser = new TsvParser();
            _tsvMapper = new TsvMapper();
        }

        private void FillTable(string tableName, string files)
        {

        }

        public void Load(Dictionary<string, string[]> fileNamesByTable, string pathToMappings, string pathToSchema)
        {
            // 1. string array (filename array)
            // 2. TsvFlatColumn array (table name + TsvColumn array)
            // 3. TableFlatColumn array (table name + TableColumn array)
            // 4. TableFlatRow array (table name + TableRow array)
            // 5. TableInsertionInfo array (table name + TableRow array array)

            // 1. Load mappings and scheme
            var mappings = _mappingsLoader.FromFile(pathToMappings);
            var schema = File.ReadAllText(pathToSchema);

            // 2. Resolve file contents by filename
            var contentsByTable =
                fileNamesByTable
                    .SelectMany(kv => kv.Value
                        .Select(fileName => (table: kv.Key, stream: File.OpenRead(fileName)))
                    );

            var tableWithValues =
                fileNamesByTable
                    .AsParallel()
                    .SelectMany(kv =>
                        kv.Value
                            .Select(fileName => (table: kv.Key, stream: File.OpenRead(fileName))))
                    .SelectMany(tableWithStream =>
                        _tsvParser.FromStream(tableWithStream.stream)
                            .Select(rows => (tableWithStream.table, rows)))
                    .Select(tableWithRows => (tableWithRows.table, _tsvMapper.MapFrom(tableWithRows.rows, mappings[tableWithRows.table])))
                    .AsEnumerable();
        }
    }
}