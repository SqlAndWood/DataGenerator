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

         DataFoldersLocation = (string) Parameter.GetParameterValue(p, AppConst.ParameterNames.DataFolders.ToString()) + "\\";

         DataDefinitionsPath = DataFoldersLocation  + AppConst.DataFolders.DataDefinitions + "\\";

         FilesInDirectory = Directory.GetFiles(DataDefinitionsPath);

      }
   }
}
