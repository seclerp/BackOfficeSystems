using System;
using BackOfficeSystems.BrandDataImporter.Contracts;

namespace BackOfficeSystems.BrandDataImporter.DataTransformers
{
    /// <summary>
    /// Transformer used to transform date or time strings intp SQL-friendly date time view
    /// </summary>
    public class DateTimeTransformer : ITransformer
    {
        public string Transform(string input) => DateTime.Parse(input).ToString("yyyy-MM-dd HH:mm:ss");
    }
}