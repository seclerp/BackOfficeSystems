using System;

namespace BackOfficeSystems.BrandDataImporter.Exceptions
{
    public class TsvValidationException : Exception
    {
        public TsvValidationException(string message) : base(message)
        {
        }
    }
}