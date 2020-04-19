using System.IO;
using BackOfficeSystems.BrandDataImporter.Exceptions;
using BackOfficeSystems.BrandDataImporter.Models;

namespace BackOfficeSystems.BrandDataImporter.Contracts
{
    /// <summary>
    /// Service that provides <see cref="TsvFile"/> from raw view
    /// </summary>
    public interface ITsvParser
    {
        /// <summary>
        /// Creates <see cref="TsvFile"/> from given short name and stream
        /// </summary>
        /// <param name="shortFileName">Short name of a TsvFile</param>
        /// <param name="stream">Stream to get data from</param>
        /// <returns>New <see cref="TsvFile"/> instance</returns>
        /// <exception cref="TsvParsingException">Throws when input was not in correct TSV format</exception>
        TsvFile FromStream(string shortFileName, Stream stream);
    }
}