using BackOfficeSystems.BrandDataImporter.Contracts;

namespace BackOfficeSystems.BrandDataImporter.DataTransformers
{
    /// <summary>
    /// String transformer that does nothing with input
    /// </summary>
    public class StringTransformer : ITransformer
    {
        public string Transform(string input) => input;
    }
}