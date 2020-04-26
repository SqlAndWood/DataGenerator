using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DG
{

   public enum DataFolders
   {
      DataGenerated,
      DataDefinitions,
      DataMimic
   }

   class ObtainDataDefinitions
   {

      public List<OutputColumnDefinition> ColumnDefinitions{ get;  set; }

      public ObtainDataDefinitions( Parameter p)
      {

         var dataFoldersLocation = (string)Parameter.GetParameterValue(p, ParameterNames.DataFolders.ToString()) + "\\";

         var dataDefinitionsPath = dataFoldersLocation + "\\" + DataFolders.DataDefinitions + "\\";

         //TODO: for the moment, Hard coded to "Presenter.json"  -> Upgrade to loop each Definition.
         var tempDataDefinitionPathAndFile = dataDefinitionsPath + "Presenter.json";

         JObject jsonToken;

         using (System.IO.StreamReader reader = System.IO.File.OpenText(tempDataDefinitionPathAndFile))
         {
            //https://www.newtonsoft.com/json/help/html/LINQtoJSON.htm
            jsonToken = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
         }

         //Populate from the JSON File into Variables.
         var outputFilename = (string)jsonToken["OutputFilename"];
     
         var outputFileType = ((string)jsonToken["OutputFileType"])?.ToLower();
       
         var outputDelimiter = (string)jsonToken["OutputDelimiter"] ;

         var outputRecordCount = (int)jsonToken["OutputRecordCount"];

         var outputColumnCount = (int)jsonToken["OutputColumnCount"];

         var outputIdentityStartValue = (int)jsonToken["OutputIdentityStartValue"];

         var outputIncrementValue = (int)jsonToken["OutputIncrementValue"];

         ColumnDefinitions = new List<OutputColumnDefinition>();

         var defaultInteger = (int)Parameter.GetParameterValue(p, ParameterNames.DefaultInteger.ToString() );
         var defaultString = (string)Parameter.GetParameterValue(p, ParameterNames.DefaultString.ToString() );

         for (int i = 0; i <= outputColumnCount - 1; i++)
         {
            //NB: I only use variables to simplify data checking during run time.
            int columnPosition = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["ColumnPosition"], JTokenType.Integer, i);

            string columnName = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["ColumnName"], JTokenType.String, defaultString);

            string columnIdentityField = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["ColumnIdentityField"], JTokenType.String, defaultString);

            string columnDataType = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["ColumnDataType"], JTokenType.String, defaultString);
           
            string mimicFilename = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["MimicFilename"], JTokenType.String, defaultString);

            int columnNullablePercentage = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["ColumnNullablePercentage"], JTokenType.Integer, defaultInteger);
           
            string columnRatios = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["ColumnRatios"], JTokenType.String, defaultString);

            ColumnDefinitions.Add(new OutputColumnDefinition()
            {
               //Outputs (TODO: separate object)
               OutputFilename = outputFilename,
               OutputColumnCount = outputColumnCount,
               OutputFileType = outputFileType,
               OutputDelimiter = outputDelimiter,
               OutputRecordCount = outputRecordCount,

               OutputIdentityStartValue = outputIdentityStartValue,
               OutputIncrementValue = outputIncrementValue,

               //Column Specific
               ColumnPosition = columnPosition,
               ColumnName = columnName,

               ColumnIdentityField = columnIdentityField,
               ColumnDataType = columnDataType,

               ColumnNullablePercentage = columnNullablePercentage,
               ColumnRatios = columnRatios,

               ColumnDataMimicPathFileName = dataFoldersLocation + DataFolders.DataMimic + "\\" + mimicFilename,
               ColumnMimicFilename = mimicFilename,
               //Data Location Specific

               DataFoldersLocation = dataFoldersLocation,

               DataGeneratedPath = dataFoldersLocation + DataFolders.DataGenerated + "\\",
               DataDefinitionsPath = dataDefinitionsPath,
      
            });

         }
         
      }

      private static dynamic ConvertToken<T>(JToken token, JTokenType jt, T defaultReturnValue)
      {
         if (token.Type == jt)  
         {
            return token;
         }
         return defaultReturnValue;

      }
      
   }
}
