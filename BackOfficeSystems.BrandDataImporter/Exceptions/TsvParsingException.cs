using System;

namespace BackOfficeSystems.BrandDataImporter.Exceptions
{
    public class TsvParsingException : Exception
    {
        public string Error { get; }

        public TsvParsingException(string error)
        {
            Error = error;
        }
    }
}