using System.Collections.Generic;
using System.IO;

namespace DG
{
   public class Parameter
   {
      
      public List<ParameterTuple> ListOfParameterStrings { get;  set; }

      public Parameter()
      {
         ListOfParameterStrings = new List<ParameterTuple>();

         //Testing accessibility of files in a folder.
         string curDirectory = Directory.GetCurrentDirectory();
         string path = $"C:\\git\\DataGenerator\\GeneratorOutputDefinitions";
         string[] filesInDir = Directory.GetFiles(path);

         //Store as much of this in a configuration file; app.config?

         //Issue here will be expanding to multiple Source JSON and multiple MockData Files.
         ParameterTuple.AddNewParameterString(ListOfParameterStrings, "OutputDefinitionsPathAndName", (string)@"C:\git\DataGenerator\GeneratorOutputDefinitions\Presenter.json", "Represents the file to be processed today.");
         //ParameterTuple.AddNewParameterString(ListOfParameterStrings, "MockDataPath", parameterValue:(string)@"C:\git\DataGenerator\MockData\", ".");


         //These will not be here, eventually they will be preloaded as part of the startup phase.
         ParameterTuple.AddNewParameterString(ListOfParameterStrings, "123YN.txt", parameterValue: (string)@"C:\git\DataGenerator\MockData\123YN.txt", ".");
         ParameterTuple.AddNewParameterString(ListOfParameterStrings, "Surname.txt", parameterValue: (string)@"C:\git\DataGenerator\MockData\Surname.txt", ".");
         ParameterTuple.AddNewParameterString(ListOfParameterStrings, "1234567890.txt", parameterValue: (string)@"C:\git\DataGenerator\MockData\1234567890.txt", ".");
         ParameterTuple.AddNewParameterString(ListOfParameterStrings, "YesNo.txt", parameterValue: (string)@"C:\git\DataGenerator\MockData\YesNo.txt", ".");
         ParameterTuple.AddNewParameterString(ListOfParameterStrings, "TrueFalse.txt", parameterValue: (string)@"C:\git\DataGenerator\MockData\TrueFalse.txt", ".");


         

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
