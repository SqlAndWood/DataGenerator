namespace DG
{

   class DataGeneratorController
   {

      public DataGeneratorController()
      {

         Parameter p = new Parameter();

         var dataDefinitionFiles = new DataDefinitionFiles(p);

         //If i am passng dataDefinitionFiles, I could pass an integer Pos i.  (not used at present
         //var lbound = dataDefinitionFiles.FilesInDirectory.GetLowerBound(0);
         //var Ubound = dataDefinitionFiles.FilesInDirectory.GetUpperBound(0);

         foreach (string fileName in dataDefinitionFiles.FilesInDirectory)
         {

            //TODO: Loop all known Files in the DataDefinition. 
            ObtainDataDefinitions dataDefinitions = new ObtainDataDefinitions(p, dataDefinitionFiles, fileName);

            CreateRandomRecords crr = new CreateRandomRecords(dataDefinitions);

            new SaveGeneratedData(crr.DTable, dataDefinitions);

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
