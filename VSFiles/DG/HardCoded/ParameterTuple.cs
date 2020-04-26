using System.Collections.Generic;

namespace DG
{

   public class ParameterTuple 
   {
      public string ParameterName { get; set; }
      public dynamic ParameterValue { get; set; }

      public string ParameterDescription { get; set; }

      public static void AddNewParameterString(List<ParameterTuple> parameterString, string parameterName, dynamic parameterValue, string parameterDescription)
      {
        
         parameterString.Add(new ParameterTuple
         {
            ParameterName = parameterName.ToLower(),
            ParameterValue = parameterValue,
            ParameterDescription = parameterDescription,

         });
                              
      }

   }

}
