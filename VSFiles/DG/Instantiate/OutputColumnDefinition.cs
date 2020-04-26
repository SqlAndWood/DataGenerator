namespace DG
{
   class OutputColumnDefinition
   {

      //Outputs: Will become a parent class to the ColumnDefinition
      public string OutputFilename { get; set; }
      public int OutputColumnCount { get; set; }
      public string OutputFileType { get; set; }
      public string OutputDelimiter { get; set; }
      public int OutputRecordCount { get; set; }

      public int OutputIdentityStartValue { get; set; }
      public int OutputIncrementValue { get; set; }

      //Data Location Specific
      public string DataFoldersLocation { get; set; }
      
      public string DataGeneratedPath { get; set; }
      public string DataDefinitionsPath { get; set; }

      //Column Specific
      public int ColumnPosition { get; set; }
      public string ColumnName { get; set; }

      public string ColumnIdentityField { get; set; }
      public string ColumnDataType { get; set; }
      
      public int ColumnNullablePercentage { get; set; }
      public string ColumnRatios { get; set; }
      public string ColumnMimicFilename { get; set; }
      public string ColumnDataMimicPathFileName { get; set; }

     // public OutputColumnDefinition()  { }

   }
   
}