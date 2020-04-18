using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BackOfficeSystems.BrandDataImporter.Models;

namespace BackOfficeSystems.BrandDataImporter.Contracts
{
    public interface ITsvParser
    {
        IEnumerable<TsvRow> FromStream(string tableName, Stream stream);
    }
}