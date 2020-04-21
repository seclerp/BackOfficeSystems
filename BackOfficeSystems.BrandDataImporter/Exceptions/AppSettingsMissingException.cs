using System;

namespace BackOfficeSystems.BrandDataImporter.Exceptions
{
    /// <summary>
    /// Exception that occurs when not optional settings are not found
    /// </summary>
    public class AppSettingsMissingException : Exception
    {
        public AppSettingsMissingException(string settingsName) : base($"App settings is missing: {settingsName}")
        {
        }
    }
}