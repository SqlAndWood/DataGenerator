using System.Collections.Generic;

namespace DG
{
   class DefinitionTable
   {
      public List<DefinitionColumn> ColumnDefinitions { get; set; }

      public string OutputFilename { get; set; }
      public string OutputFileType { get; set; }
      public string OutputDelimiter { get; set;}

      public int ColumnDefinitionCount { get; set; } 
      public int OutputRecordCount { get; set; }

      public string DataFoldersLocation { get; set; }
      public string DataGeneratedPath { get; set; }
      public string DataDefinitionsPath { get; set; }

      public DefinitionTable()
      {
         ColumnDefinitions = new List<DefinitionColumn>();
      }

   }
}
