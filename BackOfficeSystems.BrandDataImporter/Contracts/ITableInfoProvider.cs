using System.Collections.Generic;
using System.Data;

namespace BackOfficeSystems.BrandDataImporter.Contracts
{
    /// <summary>
    /// Provider that provides information about SQL table
    /// </summary>
    public interface ITableInfoProvider
    {
        /// <summary>
        /// Returns dictionary where Key is column name and Value is SQL column type
        /// </summary>
        /// <param name="tableName">Name of a table to get info from</param>
        /// <param name="transaction">Optional transaction that need to used with query</param>
        /// <returns>Dictionary with column type info</returns>
        Dictionary<string, string> GetColumnTypes(string tableName, IDbTransaction? transaction = null);
    }
}