namespace DG
{

   class DataGeneratorController
   {

      public DataGeneratorController()
      {

         Parameter p = new Parameter();

         var dataDefinitionFiles = new DataDefinitionFiles(p);

         foreach (string fileName in dataDefinitionFiles.FilesInDirectory)
         {

            //TODO: each loop should de reference these objects. 
            ObtainDataDefinitions dataDefinitions = new ObtainDataDefinitions(p, dataDefinitionFiles, fileName);
            
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
