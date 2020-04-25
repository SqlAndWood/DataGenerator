using System.Collections.Generic;

namespace DG
{

   class EController
   {

      public Parameter Parameters { get; set; }

      public ObtainColumnDefinitions ObtainColumnDefinitions { get; set; }
      
      public EController()
      {

         Parameters = new Parameter();
         ObtainColumnDefinitions = new ObtainColumnDefinitions(Parameters);

         //For each File in ObjectDefinition... or.. Read all files in a folder.
         //Pretend this has happened with : C:\git\DataGenerator\GeneratorOutputDefinitions\Presentor.json
         CreateRandomRecords crr = new CreateRandomRecords(Parameters, ObtainColumnDefinitions);

         //Save to GeneratedData
         SaveGeneratedData sgd = new SaveGeneratedData(crr);


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
