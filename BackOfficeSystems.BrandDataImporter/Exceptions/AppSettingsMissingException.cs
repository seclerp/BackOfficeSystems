using System;

namespace BackOfficeSystems.BrandDataImporter.Exceptions
{
    public class AppSettingsMissingException : Exception
    {
        public AppSettingsMissingException(string settingsName) : base($"App settings is missing: {settingsName}")
        {
        }
    }
}