namespace DG
{

   class EController
   {

      public Parameter Parameters { get; set; }

      public OutputDefinitions OutputDefinitions { get; set; }
      
      public EController()
      {

         Parameters = new Parameter();
         OutputDefinitions = new OutputDefinitions(Parameters);

         ReadFiles rfs = new ReadFiles(Parameters, OutputDefinitions);
         
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
