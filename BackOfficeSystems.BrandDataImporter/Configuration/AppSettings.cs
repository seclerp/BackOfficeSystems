using System.Collections.Generic;

namespace BackOfficeSystems.BrandDataImporter.Configuration
{
    public class AppSettings
    {
        public Dictionary<string, string[]> FilesToImport { get; set; }
        public DbSchema DbSchema { get; set; }
    }
}