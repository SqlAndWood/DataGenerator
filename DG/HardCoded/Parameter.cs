using System.Collections.Generic;

namespace DG
{
   public class Parameter
   {
      
      public List<ParameterTuple> ListOfParameterStrings { get;  set; }

      public Parameter()
      {
         ListOfParameterStrings = new List<ParameterTuple>();
  
         //Issue here will be expanding to multiple Source JSON and multiple MockData Files.
         ParameterTuple.AddNewParameterString(ListOfParameterStrings, "OutputDefinitionsPathAndName", (string)@"C:\git\DataGenerator\GeneratorOutputDefinitions\Presenter.json", "Represents the file to be processed today.");
         //ParameterTuple.AddNewParameterString(ListOfParameterStrings, "MockDataPath", parameterValue:(string)@"C:\git\DataGenerator\MockData\", ".");

         ParameterTuple.AddNewParameterString(ListOfParameterStrings, "MockData123YN", parameterValue: (string)@"C:\git\DataGenerator\MockData\123YN.txt", ".");
         ParameterTuple.AddNewParameterString(ListOfParameterStrings, "Surname", parameterValue: (string)@"C:\git\DataGenerator\MockData\Surname.txt", ".");
         ParameterTuple.AddNewParameterString(ListOfParameterStrings, "1234567890", parameterValue: (string)@"C:\git\DataGenerator\MockData\1234567890.txt", ".");
         

         ParameterTuple.AddNewParameterString(ListOfParameterStrings, "DefaultInteger", (int)-99, "");
         ParameterTuple.AddNewParameterString(ListOfParameterStrings, "DefaultString", (string)@"", "Represents the file to be processed today.");
         //Set low for initial tetsting.
         //ParameterTuple.AddNewParameterString(ListOfParameterStrings, "batchProcessing", (int)5, "");

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
