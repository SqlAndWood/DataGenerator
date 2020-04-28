using System.Collections.Generic;

namespace DG
{
   class TableDefinition
   {
      public List<ColumnDefinition> ColumnDefinitions { get; set; }

      public string OutputFilename { get; set; }  //Potentially deprecated, if we take the name of the actual file
      public string OutputFileType { get; set; }
      public string OutputDelimiter { get; set;}

      public int ColumnDefinitionCount { get; set; } 
      public int OutputRecordCount { get; set; }

      public string DataFoldersLocation { get; set; }
      public string DataGeneratedPath { get; set; }
      public string DataDefinitionsPath { get; set; }

      public TableDefinition()
      {
         ColumnDefinitions = new List<ColumnDefinition>();
      }

   }
}
