using System.Collections.Generic;
using BackOfficeSystems.BrandDataImporter.Models;

namespace BackOfficeSystems.BrandDataImporter.Contracts
{
    public interface ITsvMapper
    {
        IEnumerable<TableRow> MapFrom(IEnumerable<TsvRow> rows, Dictionary<string, Dictionary<string, string>> mappings);
    }
}