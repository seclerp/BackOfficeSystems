namespace BackOfficeSystems.BrandDataImporter.Models
{
    // 1. string array (filename array)
    // 2. TsvFlatColumn array (table name + TsvColumn array)
    // 3. TableFlatColumn array (table name + TableColumn array)
    // 4. TableFlatRow array (table name + TableRow array)
    // 5. TableInsertionInfo array (table name + TableRow array array)

    public class TsvFlatColumn
    {
        public string TableName { get; set; }
        public int RowIndex { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}