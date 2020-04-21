using System;

namespace BackOfficeSystems.BrandDataImporter.Exceptions
{
    /// <summary>
    /// Exception that occurs when validation was unsuccessful
    /// </summary>
    public class TsvValidationException : Exception
    {
        public string Error { get; }

        public TsvValidationException(string error)
        {
            Error = error;
        }
    }
}