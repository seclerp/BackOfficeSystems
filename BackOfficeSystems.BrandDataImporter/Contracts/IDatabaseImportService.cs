using BackOfficeSystems.BrandDataImporter.Models;

namespace BackOfficeSystems.BrandDataImporter.Contracts
{
    /// <summary>
    /// Service that import data from <see cref="TsvFile"/> into the database
    /// </summary>
    public interface IDatabaseImportService
    {
        /// <summary>
        /// Creates database if not exists with given schema and name
        /// </summary>
        /// <param name="schema">SQL schema to be applied to new database</param>
        /// <param name="databaseName">Database name to create</param>
        void EnsureDatabaseCreated(string schema, string databaseName);

        /// <summary>
        ///
        /// </summary>
        /// <param name="file"></param>
        /// <param name="databaseName"></param>
        void ImportFromFile(TsvFile file, string databaseName);
    }
}