namespace DG
{

    class DataGeneratorController
   {

      public DataGeneratorController()
      {
        
         string[] aListOfFiles = System.IO.Directory.GetFiles(Config.GetValue("LocalPath").ToString() + "\\" + AppConst.DataFolders.DataDefinitions + "\\");

         foreach (string fileName in aListOfFiles)
         {

            ObtainDataDefinitions dataDefinitions = new ObtainDataDefinitions(fileName);
            
            GeneratePopulateController gpc = new GeneratePopulateController(dataDefinitions);

            new SaveGeneratedData(gpc.DTable, dataDefinitions);
            
         }

      }

   }

}
/*
 *
 *using System.Reflection
 *
 *string ClassName = MethodBase.GetCurrentMethod().DeclaringType.Name;
 *string CallingRoutine = MethodBase.GetCurrentMethod().Name;
 *
 */
