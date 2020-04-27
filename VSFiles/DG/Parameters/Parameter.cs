using System.Collections.Generic;

namespace DG
{

   public enum ParameterNames
   {
      DataFolders,
      DefaultInteger,
      DefaultString
   }
   
   public class Parameter
   {
      
      public List<ParameterTuple> ListOfParameterStrings { get;  set; }
      
      public Parameter()
      {
         ListOfParameterStrings = new List<ParameterTuple>();
         
         //For the End User: Enter the Path to these folders.
            //DataGenerated
            //DataDefinitions
            //DataMimic
    
            //I'd rather use referential referencing. but with Visual Studio, unsure how to perform this action.
         ParameterTuple.AddNewParameterString(ListOfParameterStrings, ParameterNames.DataFolders.ToString(), @"C:\git\DataGenerator", "This is the location where ");

         //  ParameterTuple.AddNewParameterString(ListOfParameterStrings, ParameterNames.DataFolders.ToString(), @"B:\Users\wooda01.ADMINSRVAD\git\Applications\DataGenerator", "This is the location where ");

         //Coders parameters. 
         ParameterTuple.AddNewParameterString(ListOfParameterStrings, ParameterNames.DefaultInteger.ToString(), -99, "A default value when expecting an INTEGER");
         ParameterTuple.AddNewParameterString(ListOfParameterStrings, ParameterNames.DefaultString.ToString(), @"", "A default value when expecting a STRING.");
       
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
