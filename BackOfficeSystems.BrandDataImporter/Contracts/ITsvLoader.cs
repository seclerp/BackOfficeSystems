using System.Collections.Generic;

namespace BackOfficeSystems.BrandDataImporter.Contracts
{
    /// <summary>
    /// Main loading flow executor service
    /// </summary>
    public interface ITsvLoader
    {
        /// <summary>
        /// Executes loading flow
        /// </summary>
        /// <param name="fileNames">Filenames of TSV files to import</param>
        /// <param name="pathToSchema">Path to SQL schema file</param>
        /// <param name="databaseName">Name of database to import</param>
        void Load(IEnumerable<string> fileNames, string pathToSchema, string databaseName);
    }
}