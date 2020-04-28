namespace DG
{
   class ColumnDefinition
   {
      public int ColumnPosition { get; set; }  //This may or may not be correct
      public string ColumnName { get; set; } //There is a chance this overlaps with another columnName.

      public string ColumnDataType { get; set; }
      
      public int ColumnNullablePercentage { get; set; }
      public string ColumnRatios { get; set; }

      //This information is useful per Column.
      public string ColumnLoadDataMimicMethod { get; set; }

      public string ColumnDataMimicFilename { get; set; }
      public string ColumnDataMimicPathFileName { get; set; }

      public string ColumnStartWith { get; set; }
      public string ColumnEndWith { get; set; }
      public string ColumnLength { get; set; }

   }
   
}