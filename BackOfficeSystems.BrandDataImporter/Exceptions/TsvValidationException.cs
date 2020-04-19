﻿using System;

namespace BackOfficeSystems.BrandDataImporter.Exceptions
{
    public class TsvValidationException : Exception
    {
        public string Error { get; }

        public TsvValidationException(string error)
        {
            Error = error;
        }
    }
}