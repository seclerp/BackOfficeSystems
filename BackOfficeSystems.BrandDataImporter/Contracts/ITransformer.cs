namespace BackOfficeSystems.BrandDataImporter.Contracts
{
    /// <summary>
    /// Service that Transforms input to a SQL friendly view
    /// </summary>
    public interface ITransformer
    {
        /// <summary>
        /// Applies transformation to a given value and returns transformed one
        /// </summary>
        /// <param name="input">Raw data need to be transformed</param>
        /// <returns>Transformed value</returns>
        string Transform(string input);
    }
}