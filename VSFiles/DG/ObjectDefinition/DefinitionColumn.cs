namespace DG
{
   class DefinitionColumn
   {
      public int ColumnPosition { get; set; } 

      //TODO: Given that ColumnNames must not be duplicated, provide mechanism to over-right duplicated column name.
      public string ColumnName { get; set; } 

      public string ColumnDataType { get; set; }
      
      public int ColumnNullablePercentage { get; set; }
     
      public string ColumnLoadDataMimicMethod { get; set; }

      public string ColumnDataMimicFilename { get; set; }
      public string ColumnDataMimicPathFileName { get; set; }

      public string ColumnStartWith { get; set; }
      public string ColumnEndWith { get; set; }
      public string ColumnLength { get; set; }

   }
   
}