namespace DG
{
    class ProgramController
    {

      public ProgramController()
      {
         //Grabs the list of JSON files in a specific folder. (actually, grabs all folders, regardless of file type)
         string[] aListOfFiles = System.IO.Directory.GetFiles(AppConst.GetValue(AppConst.LocalPath).ToString() + "\\" + AppConst.DataFolders.DataDefinitions + "\\"); 
         

         foreach (string fileName in aListOfFiles)
         {

            DefinitionController defineDataRequirements = new DefinitionController(fileName);

            GenerateData gpc = new GenerateData(defineDataRequirements);

            //Modify this to Save each line while being populated.
            PopulateData pp = new PopulateData(defineDataRequirements, gpc.PreLoadedFieldData);

            //new SaveData(pp.DTable, defineDataRequirements);
            
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
