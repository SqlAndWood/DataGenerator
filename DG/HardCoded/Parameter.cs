using System.Collections.Generic;

namespace DG
{
   public class Parameter
   {

 
      public List<ParameterTuple> ListOfParameterStrings { get;  set; }

      public Parameter()
      {
         ListOfParameterStrings = new List<ParameterTuple>();
  
         ParameterTuple.AddNewParameterString(ListOfParameterStrings, "filePathAndName", (string)@"C:\git\DataGenerator\GeneratorOutputDefinitions\Presenter.json", "Represents the file to be processed today.");

         ParameterTuple.AddNewParameterString(ListOfParameterStrings, "DefaultInteger", (int)-99, "");

         ParameterTuple.AddNewParameterString(ListOfParameterStrings, "DefaultString", (string)@"", "Represents the file to be processed today.");

      }


      public static dynamic GetParameterValue(Parameter p, string parameterName)
      {
         ParameterTuple identified = p.ListOfParameterStrings.Find(x => x.ParameterName.Contains(parameterName.ToLower()));
         
         //TODO: Potentially a better way to do this?
         if (identified != null) { 
            return identified.ParameterValue;
         }
         else
         {
            return "";

         }

      }


   }
}
