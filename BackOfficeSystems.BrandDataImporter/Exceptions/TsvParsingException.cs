using System;

namespace BackOfficeSystems.BrandDataImporter.Exceptions
{
    /// <summary>
    /// Exception that occurs when parsing was unsuccessful
    /// </summary>
    public class TsvParsingException : Exception
    {
        public string Error { get; }

        public TsvParsingException(string error)
        {
            Error = error;
        }
    }
}