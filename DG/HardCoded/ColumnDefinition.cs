namespace DG
{
   class ColumnDefinition
   {
     
      public int ColumnPosition { get; set; }
      public string ColumnName { get; set; }

      public string ColumnIdentityField { get; set; }
      public string ColumnDataType { get; set; }

      public string MockSourceDataFilename { get; set; }

      public int ColumnNullablePercentage { get; set; }
      public string ColumnRatios { get; set; }
      
      public ColumnDefinition()  { }

   }


}
