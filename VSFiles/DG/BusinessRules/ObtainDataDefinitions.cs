using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

         //var outputColumnCount = (int)jsonToken["OutputColumnCount"];

         var outputIdentityStartValue = (int)jsonToken["OutputIdentityStartValue"];

         var outputIncrementValue = (int)jsonToken["OutputIncrementValue"];

         TableDefinition = new TableDefinition
         {
            OutputFilename = outputFilename,
           // OutputColumnCount = outputColumnCount,
            OutputFileType = outputFileType,
            OutputDelimiter = outputDelimiter,
            OutputRecordCount = outputRecordCount,
            OutputIdentityStartValue = outputIdentityStartValue,
            OutputIncrementValue = outputIncrementValue,
            DataFoldersLocation = dataFoldersLocation,
            DataGeneratedPath = dataFoldersLocation + DataFolders.DataGenerated + "\\",
            DataDefinitionsPath = dataDefinitionsPath
         };

         var defaultInteger = (int)Parameter.GetParameterValue(p, ParameterNames.DefaultInteger.ToString() );
         var defaultString = (string)Parameter.GetParameterValue(p, ParameterNames.DefaultString.ToString() );

        //   I'm sure there is a better way, but this works for now.
        var outputColumnCount = 0;
        foreach (var obj in jsonToken["ColumnDefinitions"] )
        {
                outputColumnCount += 1;
        }
        //Still considering the layout of this.
        TableDefinition.ColumnDefinitionCount = outputColumnCount;

         for (int i = 0; i <= outputColumnCount - 1; i++)
         {
            //NB: I only use variables to simplify data checking during run time.
            int columnPosition = ConvertToken((int)jsonToken["ColumnDefinitions"]?[i]?["ColumnPosition"], JTokenType.Integer, i);

            string columnName = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["ColumnName"], JTokenType.String, defaultString);

            string columnIdentityField = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["ColumnIdentityField"], JTokenType.String, defaultString);

            string columnDataType = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["ColumnDataType"], JTokenType.String, defaultString);
           
            string mimicFilename = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["MimicFilename"], JTokenType.String, defaultString);

            int columnNullablePercentage = ConvertToken((int)jsonToken["ColumnDefinitions"]?[i]?["ColumnNullablePercentage"], JTokenType.Integer, defaultInteger);
           
            string columnRatios = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["ColumnRatios"], JTokenType.String, defaultString);

            TableDefinition.ColumnDefinitions.Add (new ColumnDefinition()
            {

               ColumnPosition = columnPosition,
               ColumnName = columnName,

               ColumnIdentityField = columnIdentityField,
               ColumnDataType = columnDataType,

               ColumnNullablePercentage = columnNullablePercentage,
               ColumnRatios = columnRatios,

               ColumnDataMimicPathFileName = dataFoldersLocation + DataFolders.DataMimic + "\\" + mimicFilename,
               ColumnMimicFilename = mimicFilename,
 
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
