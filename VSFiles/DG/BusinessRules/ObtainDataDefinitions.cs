﻿using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DG
{

   class ObtainDataDefinitions
   {

      public TableDefinition TableDefinition { get; set; }
      
      public ObtainDataDefinitions( Parameter p, DataDefinitionFiles dataDefinitionFiles,  string fileName)
      {

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

         var colCount = (int)(jsonToken["ColumnDefinitions"] ?? 0).Count();

         TableDefinition = new TableDefinition
         {
            OutputFilename = outputFilename,
            ColumnDefinitionCount = colCount,
            OutputFileType = outputFileType,
            OutputDelimiter = outputDelimiter,
            OutputRecordCount = outputRecordCount,
           
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
            int columnPosition = i + 1;// ConvertToken((int)jsonToken["ColumnDefinitions"]?[i]?["ColumnPosition"], JTokenType.Integer, i);

            string columnName = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["ColumnName"], JTokenType.String, defaultString);

            string columnDataType = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["ColumnDataType"], JTokenType.String, defaultString);
               
            int columnNullablePercentage = ConvertToken((int)jsonToken["ColumnDefinitions"]?[i]?["ColumnNullablePercentage"], JTokenType.Integer, defaultInteger);
           
          
            //This is used for created Data
            string columnLoadDataMimicMethod = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["LoadDataMimicMethod"], JTokenType.String, defaultString);

            string columnDataMimicFilename = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["DataMimicFilename"], JTokenType.String, defaultString);

            string columnStartWith = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["StartWith"], JTokenType.String, defaultString);

            string columnEndWith = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["EndWith"], JTokenType.String, defaultString);

            string columnLength = ConvertToken(jsonToken["ColumnDefinitions"]?[i]?["Length"], JTokenType.String, defaultString);

            TableDefinition.ColumnDefinitions.Add (new ColumnDefinition()
            {

               ColumnPosition = columnPosition,
               ColumnName = columnName,

               ColumnDataType = columnDataType,

               ColumnNullablePercentage = columnNullablePercentage,
             
               ColumnLoadDataMimicMethod = columnLoadDataMimicMethod,
               ColumnDataMimicFilename = columnDataMimicFilename,

               ColumnDataMimicPathFileName = dataFoldersLocation + AppConst.DataFolders.DataMimic + "\\" + columnDataMimicFilename,
                
               ColumnStartWith = columnStartWith,
               ColumnEndWith = columnEndWith,
               ColumnLength = columnLength,

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
