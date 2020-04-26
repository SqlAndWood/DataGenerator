namespace DG
{

   class EController
   {

    //  public Parameter Parameters { get; set; }

   //   public ObtainDataDefinitions ObtainDataDefinitions { get; set; }
      
      public EController()
      {

         Parameter p = new Parameter();

         ObtainDataDefinitions dataDefinitions = new ObtainDataDefinitions(p);

         //AT the moment, this is locked to one file only. Yet to expand to 'all' files within the DataDefinition folder.
         CreateRandomRecords crr = new CreateRandomRecords(dataDefinitions);

         ////Save to GeneratedData: BTW this needs to be a loop per 'DataDefinition' provided file.
         new SaveGeneratedData(crr.DTable, dataDefinitions);


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
