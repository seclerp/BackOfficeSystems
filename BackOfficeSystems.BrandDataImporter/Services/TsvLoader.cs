using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackOfficeSystems.BrandDataImporter.Contracts;
using Serilog;
using Serilog.Context;

namespace BackOfficeSystems.BrandDataImporter.Services
{
    /// <summary>
    /// General implementation of <see cref="ITsvLoader"/>
    /// </summary>
    public class TsvLoader : ITsvLoader
    {
        private readonly ITsvParser _tsvParser;
        private readonly ITsvFileValidator _validator;
        private readonly IDatabaseImportService _importService;

        /// <param name="tsvParser">Parser that need to be used for parsing .tsv files</param>
        /// <param name="validator">Validator that need to be used for validating parsed files</param>
        /// <param name="importService">Service that need to be used to import validated data into database</param>
        public TsvLoader(ITsvParser tsvParser, ITsvFileValidator validator, IDatabaseImportService importService)
        {
            _tsvParser = tsvParser;
            _validator = validator;
            _importService = importService;
        }

        public void Load(IEnumerable<string> fileNames, string pathToSchema, string databaseName)
        {
            // 1. Resolve TsvFiles by filenames
            using var filesToImportContext = LogContext.PushProperty("FilesToImport", fileNames);
            Log.Information("Start loading and parsing TSV files");
            var tsvFiles =
                fileNames
                    .Select(fileName =>
                    {
                        using var _ = LogContext.PushProperty("FileName", fileName);
                        return (fileName, tsvFile: _tsvParser.FromStream(Path.GetFileNameWithoutExtension(fileName), File.OpenRead(fileName)));
                    });
            Log.Information("TSV files successfully loaded and parsed");

            // 2. Validate files
            Log.Information("Start validating TSV files");
            var validatedFiles =
                tsvFiles
                    .Select(fileMameWithTsvFile =>
                    {
                        var (fileName, tsvFile) = fileMameWithTsvFile;
                        using var _ = LogContext.PushProperty("FileName", fileName);
                        return _validator.Validate(tsvFile);
                    }).ToList();
            Log.Information("TSV files successfully validated");

            // 3. Load scheme
            using var pathToSchemaContext = LogContext.PushProperty("PathToSchema", pathToSchema);
            Log.Information("Loading SQL schema");
            var schema = File.ReadAllText(pathToSchema);
            Log.Information("SQL schema successfully loaded");

            // 4. Ensure that database for insertion exists, create if not
            _importService.EnsureDatabaseCreated(schema, databaseName);

            // 5. Insert data from files into tables
            validatedFiles.ForEach(file => _importService.ImportFromFile(file, databaseName));
            Log.Information("Loading finished successfully");
        }
    }
}