using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DG
{

   class DefinitionController
   {
      public DefinitionTable TableDefinition { get; set; }
      
      public DefinitionController(string fileName)
      {

         var dataFoldersLocation = AppConst.GetValue(AppConst.LocalPath).ToString() + "\\"; 

         var dataDefinitionsPath = dataFoldersLocation + AppConst.DataFolders.DataDefinitions + "\\"; 
    
         JObject jsonToken;

         using (System.IO.StreamReader reader = System.IO.File.OpenText(fileName))
         {
            //https://www.newtonsoft.com/json/help/html/LINQtoJSON.htm
            jsonToken = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
         }
         
         TableDefinition = new DefinitionTable
         {
            OutputFilename = (string)jsonToken["OutputFilename"],
            ColumnDefinitionCount = (jsonToken["ColumnDefinitions"] ?? 0).Count(),
            OutputFileType = ((string)jsonToken["OutputFileType"])?.ToLower(),
            OutputDelimiter = (string)jsonToken["OutputDelimiter"],
            OutputRecordCount = (int)jsonToken["OutputRecordCount"],

            DataFoldersLocation = dataFoldersLocation,
            DataGeneratedPath = dataFoldersLocation + AppConst.DataFolders.DataGenerated + "\\",
            DataDefinitionsPath = dataDefinitionsPath
         };
          
         var defaultInteger = AppConst.GetValue(AppConst.ParameterNames.DefaultInteger.ToString()); //cast as int?
         var defaultString = AppConst.GetValue(AppConst.ParameterNames.DefaultString.ToString()); 

         var outputColumnCount = TableDefinition.ColumnDefinitionCount;

         for (int i = 0; i <= outputColumnCount - 1; i++)
         {
         
            string columnDataMimicFilename = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["DataMimicFilename"], JTokenType.String, defaultString);
            
            TableDefinition.ColumnDefinitions.Add (
                new DefinitionColumn()
                                        {

                                           ColumnPosition = i + 1, // ConvertToken((int)jsonToken["ColumnDefinitions"]?[i]?["ColumnPosition"], JTokenType.Integer, i);
                                           ColumnName = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["ColumnName"], JTokenType.String, defaultString),

                                           ColumnDataType = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["ColumnDataType"], JTokenType.String, defaultString),

                                           ColumnNullablePercentage = ConvertToken((int)jsonToken["ColumnDefinitions"]?[i]?["ColumnNullablePercentage"], JTokenType.Integer, defaultInteger),
             
                                           ColumnLoadDataMimicMethod = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["LoadDataMimicMethod"], JTokenType.String, defaultString),

                                           ColumnDataMimicFilename = columnDataMimicFilename,
                                           ColumnDataMimicPathFileName = dataFoldersLocation + AppConst.DataFolders.DataMimic + "\\" + columnDataMimicFilename,
                
                                           ColumnStartWith = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["StartWith"], JTokenType.String, defaultString),
                                           ColumnEndWith = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["EndWith"], JTokenType.String, defaultString),
                                           ColumnLength = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["Length"], JTokenType.String, defaultString),

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
