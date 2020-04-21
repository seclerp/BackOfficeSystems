using BackOfficeSystems.BrandDataImporter.Exceptions;
using BackOfficeSystems.BrandDataImporter.Models;

namespace BackOfficeSystems.BrandDataImporter.Contracts
{
    /// <summary>
    /// Service that validates <see cref="TsvFile"/>
    /// </summary>
    public interface ITsvFileValidator
    {
        /// <summary>
        /// Validates given <see cref="TsvFile"/> object and returns it on success
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Validated file</returns>
        /// <exception cref="TsvValidationException">Throws when TsvFile has incorrect format or data</exception>
        TsvFile Validate(TsvFile file);
    }
}