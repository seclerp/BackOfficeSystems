namespace BackOfficeSystems.BrandDataImporter.Models
{
    // 1. string array (filename array)
    // 2. TsvFlatColumn array (table name + TsvColumn array)
    // 3. TableFlatColumn array (table name + TableColumn array)
    // 4. TableFlatRow array (table name + TableRow array)
    // 5. TableInsertionInfo array (table name + TableRow array array)

    public class TableFlatColumn
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string RowIndex { get; set; }
        public string Value { get; set; }
    }

    public class TableColumn
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class TableRow
    {
        public string TableName { get; set; }
        public TableColumn[] Columns { get; set; }
    }

    public class TableInsertionInfo
    {
        public string TableName { get; set; }
        public TableRow[] Rows { get; set; }
    }
}