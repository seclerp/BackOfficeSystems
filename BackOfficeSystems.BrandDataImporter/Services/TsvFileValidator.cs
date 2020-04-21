using System.Linq;
using BackOfficeSystems.BrandDataImporter.Contracts;
using BackOfficeSystems.BrandDataImporter.Exceptions;
using BackOfficeSystems.BrandDataImporter.Models;

namespace BackOfficeSystems.BrandDataImporter.Services
{
    /// <summary>
    /// General implementation of <see cref="ITsvFileValidator"/>
    /// </summary>
    public class TsvFileValidator : ITsvFileValidator
    {
        public TsvFile Validate(TsvFile file)
        {
            var errors =
                file.Rows
                    .Where(row => row.Length != file.Headers.Length)
                    .Select((row, index) => $"Row {index} has incorrect number of columns, expected {file.Headers.Length}, got {row.Length}")
                    .ToList();

            if (errors.Any())
            {
                var allErrors = string.Join("\n\t", errors);
                throw new TsvValidationException($"TsvFile is invalid. Errors:\n\t{allErrors}");
            }

            return file;
        }
    }
}