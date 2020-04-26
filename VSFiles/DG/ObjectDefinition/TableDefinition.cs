using System.Collections.Generic;

namespace DG
{
   class TableDefinition
   {
      public List<ColumnDefinition> ColumnDefinitions { get; set; }

      public string OutputFilename { get; set; }  //Potentially deprecated, if we take the name of the actual file
      public string OutputFileType { get; set; }
      public string OutputDelimiter { get; set;}

      public int OutputColumnCount { get; set; }  //Potentially deprecated, as we can manually count the number of Columns in the JSON file.
      public int OutputRecordCount { get; set; }

      public int OutputIdentityStartValue { get; set; }
      public int OutputIncrementValue { get; set; }

      public string DataFoldersLocation { get; set; }
      public string DataGeneratedPath { get; set; }
      public string DataDefinitionsPath { get; set; }

      public TableDefinition()
      {
         ColumnDefinitions = new List<ColumnDefinition>();
      }

   }
}
