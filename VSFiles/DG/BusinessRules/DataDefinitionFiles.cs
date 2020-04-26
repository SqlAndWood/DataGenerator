using System.IO;

namespace DG
{
   class DataDefinitionFiles
   {

      public string DataFoldersLocation { get; }
      public string DataDefinitionsPath { get; }

      public string[] FilesInDirectory;
      
      public DataDefinitionFiles(Parameter p)
      {

         DataFoldersLocation = (string) Parameter.GetParameterValue(p, ParameterNames.DataFolders.ToString()) + "\\";

         DataDefinitionsPath = DataFoldersLocation  + DataFolders.DataDefinitions + "\\";

         FilesInDirectory = Directory.GetFiles(DataDefinitionsPath);

      }
   }
}
