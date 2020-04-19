namespace BackOfficeSystems.BrandDataImporter.Models
{
    /// <summary>
    /// Model that used as a source of data for SQL table
    /// </summary>
    public class TsvFile
    {
        /// <summary>
        /// Short name that is used as a SQL table name when importing
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Headers of TSV file. Used as a SQL column names when importing
        /// </summary>
        public string[] Headers { get; set; }

        /// <summary>
        /// Array of TSV rows. Each of then contains columns in same order as <see cref="Headers"/>. Used as source of SQL column values
        /// </summary>
        public string[][] Rows { get; set; }
    }
}