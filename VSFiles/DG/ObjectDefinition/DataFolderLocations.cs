namespace DG
{
    class DataFolderLocations
    {

      public string DataFoldersLocation { get; }
      public string DataDefinitionsPath { get; }

      public DataFolderLocations()
      {
         //DataFoldersLocation = Config.GetValue("LocalPath").ToString() + "\\";

        // DataDefinitionsPath = DataFoldersLocation + AppConst.DataFolders.DataDefinitions + "\\";

      }
   }
}
