using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DG
{

   class ObtainDataDefinitions
   {

      //TODO:  once we start to utilise >1 JSON DataDefinition File, this would be best utilised as an Array.
      public TableDefinition TableDefinition { get; set; }
      
     //TODO: This should be looped for all files in teh DataDefinitions Folder
      public ObtainDataDefinitions( Parameter p, DataDefinitionFiles dataDefinitionFiles,  string fileName)
      {

         //TODO: These two lines of code have aleady been performed in 
         var dataFoldersLocation = dataDefinitionFiles.DataFoldersLocation; //Parameter.GetParameterValue(p, ParameterNames.DataFolders.ToString()) + "\\";

         var dataDefinitionsPath = dataDefinitionFiles.DataDefinitionsPath; // dataFoldersLocation + "\\" + DataFolders.DataDefinitions + "\\";

         JObject jsonToken;

         using (System.IO.StreamReader reader = System.IO.File.OpenText(fileName))
         {
            //https://www.newtonsoft.com/json/help/html/LINQtoJSON.htm
            jsonToken = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
         }

         //Populate from the JSON File into Variables.
         var outputFilename = (string)jsonToken["OutputFilename"];
     
         var outputFileType = ((string)jsonToken["OutputFileType"])?.ToLower();
       
         var outputDelimiter = (string)jsonToken["OutputDelimiter"] ;

         var outputRecordCount = (int)jsonToken["OutputRecordCount"];

         var outputIdentityStartValue = (int)jsonToken["OutputIdentityStartValue"];

         var outputIncrementValue = (int)jsonToken["OutputIncrementValue"];

         var colCount = (int)(jsonToken["ColumnDefinitions"] ?? 0).Count();

         TableDefinition = new TableDefinition
         {
            OutputFilename = outputFilename,
            ColumnDefinitionCount = colCount,
            OutputFileType = outputFileType,
            OutputDelimiter = outputDelimiter,
            OutputRecordCount = outputRecordCount,
            OutputIdentityStartValue = outputIdentityStartValue,
            OutputIncrementValue = outputIncrementValue,
            DataFoldersLocation = dataFoldersLocation,
            DataGeneratedPath = dataFoldersLocation + AppConst.DataFolders.DataGenerated + "\\",
            DataDefinitionsPath = dataDefinitionsPath
         };

         var defaultInteger = (int)Parameter.GetParameterValue(p, AppConst.ParameterNames.DefaultInteger.ToString() );
         var defaultString = (string)Parameter.GetParameterValue(p, AppConst.ParameterNames.DefaultString.ToString() );

         //This will be in its own method?
         var outputColumnCount = TableDefinition.ColumnDefinitionCount;

         for (int i = 0; i <= outputColumnCount - 1; i++)
         {
            //NB: I only use variables to simplify data checking during run time.
            int columnPosition = ConvertToken((int)jsonToken["ColumnDefinitions"]?[i]?["ColumnPosition"], JTokenType.Integer, i);

            string columnName = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["ColumnName"], JTokenType.String, defaultString);

            string columnIdentityField = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["ColumnIdentityField"], JTokenType.String, defaultString);

            string columnDataType = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["ColumnDataType"], JTokenType.String, defaultString);
               
            int columnNullablePercentage = ConvertToken((int)jsonToken["ColumnDefinitions"]?[i]?["ColumnNullablePercentage"], JTokenType.Integer, defaultInteger);
           
            string columnRatios = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["ColumnRatios"], JTokenType.String, defaultString);

            //This is used for created Data
            string columnLoadDataMimicMethod = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["LoadDataMimicMethod"], JTokenType.String, defaultString);

            string columnDataMimicFilename = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["DataMimicFilename"], JTokenType.String, defaultString);

            string columnStartWith = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["StartWith"], JTokenType.String, defaultString);

            string columnEndWith = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["EndWith"], JTokenType.String, defaultString);

            TableDefinition.ColumnDefinitions.Add (new ColumnDefinition()
            {

               ColumnPosition = columnPosition,
               ColumnName = columnName,

               ColumnIdentityField = columnIdentityField,
               ColumnDataType = columnDataType,

               ColumnNullablePercentage = columnNullablePercentage,
               ColumnRatios = columnRatios,

               ColumnLoadDataMimicMethod = columnLoadDataMimicMethod,
               ColumnDataMimicFilename = columnDataMimicFilename,

               ColumnDataMimicPathFileName = dataFoldersLocation + AppConst.DataFolders.DataMimic + "\\" + columnDataMimicFilename,
                
               ColumnStartWith = columnStartWith,
               ColumnEndWith = columnEndWith,

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
