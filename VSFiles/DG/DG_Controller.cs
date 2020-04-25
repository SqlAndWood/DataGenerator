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


         //Read all files in a folder. PRetend this has happened with : C:\git\DataGenerator\GeneratorOutputDefinitions\Presentor.json
         ReadFiles rfs = new ReadFiles(Parameters, ObtainColumnDefinitions);
         
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
