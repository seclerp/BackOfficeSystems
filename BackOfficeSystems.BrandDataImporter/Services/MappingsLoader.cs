
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace BackOfficeSystems.BrandDataImporter.Services
{
    using ColumnMappings = Dictionary<string, Dictionary<string, string>>;

    public class MappingsLoader
    {
        public ColumnMappings FromFile(string fileName)
        {
            var fileContent = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<ColumnMappings>(fileContent);
        }
    }
}