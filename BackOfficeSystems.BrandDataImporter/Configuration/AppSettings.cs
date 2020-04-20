namespace BackOfficeSystems.BrandDataImporter.Configuration
{
    public class AppSettings
    {
        /// <summary>
        /// Array of filenames of TSV files to use as a data source
        /// </summary>
        public string[]? FilesToImport { get; set; }

        /// <summary>
        /// Path to SQL database schema file
        /// </summary>
        public string? DbSchemaFile { get; set; }

        /// <summary>
        /// Name of database that will be used for import
        /// </summary>
        public string? DatabaseName { get; set; }

        /// <summary>
        /// True for verbose logging level, False for warning and error level only
        /// </summary>
        public bool VerboseLogging { get; set; }
    }
}