using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DG
{
   class ObtainColumnDefinitions
    {

      private const string Param = "OutputDefinitionsPathAndName";
      
      public string SchemaPathAndFileName { get; private set; }
   
      public string SourceFilename{ get; private set; }

      public string FileType{ get; private set; }
     
      public string Delimiter{ get; private set; }

      public int FirstRowNumberContainingData{ get; private set; }

      public int TotalRecordCount{ get; private set; }
      public int TotalNumberOfColumns{ get; private set; }

      public List<OutputColumnDefinition> ColumnDefinitions{ get; private set; }

      private JObject _jsonToken;

      public ObtainColumnDefinitions( Parameter p)
      {

        // string ClassName = MethodBase.GetCurrentMethod().DeclaringType.Name;
        // string CallingRoutine = MethodInfo.GetCurrentMethod().Name;

         var defaultInteger = (int)Parameter.GetParameterValue(p, "DefaultInteger");
         var defaultString = (string)Parameter.GetParameterValue(p, "DefaultString");

         SchemaPathAndFileName = (string)Parameter.GetParameterValue(p, Param);
         Schema_Read();

         //Populate from the JSON File into Variables.
         SourceFilename = (string)_jsonToken["OutputFilename"];
     
         FileType = (string)_jsonToken["FileType"];
         Delimiter = (string)_jsonToken["Delimiter"] ;

         if (FileType != null) FileType = FileType.ToLower();

         TotalNumberOfColumns = (int)_jsonToken["TotalNumberOfColumns"];
       
         TotalRecordCount = (int)_jsonToken["TotalRecordCount"];

         //calculate start and end for each field
         //List<defineColumnPositions> columnPositions = new List<defineColumnPositions>();

         ColumnDefinitions = new List<OutputColumnDefinition>();

         for (int i = 0; i <= TotalNumberOfColumns - 1; i++)
         {
            //NB: I only use variables to simplify data checking during run time.
            int columnPos = ConvertToken(_jsonToken["ColumnDefinitions"][i]["ColumnPosition"], JTokenType.Integer, i);

            string columnName = ConvertToken(_jsonToken["ColumnDefinitions"][i]["ColumnName"], JTokenType.String, defaultString);

            string columnIdentityField = ConvertToken(_jsonToken["ColumnDefinitions"][i]["ColumnIdentityField"], JTokenType.String, defaultString);

            string columnDataType = ConvertToken(_jsonToken["ColumnDefinitions"][i]["ColumnDataType"], JTokenType.String, defaultString);
           
            string mockSourceDataFilename = ConvertToken(_jsonToken["ColumnDefinitions"][i]["MockSourceDataFilename"], JTokenType.String, defaultString);

            int columnNullablePercentage = ConvertToken(_jsonToken["ColumnDefinitions"][i]["ColumnNullablePercentage"], JTokenType.Integer, defaultInteger);
           
            string columnRatios = ConvertToken(_jsonToken["ColumnDefinitions"][i]["ColumnRatios"], JTokenType.String, defaultString);

            ColumnDefinitions.Add(new OutputColumnDefinition()
            {

               ColumnPosition = columnPos,
               ColumnName = columnName,

               ColumnIdentityField = columnIdentityField,
               ColumnDataType = columnDataType,
               MockSourceDataFilename = mockSourceDataFilename,
       
               ColumnNullablePercentage = columnNullablePercentage,
               ColumnRatios = columnRatios,
    
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

      private void Schema_Read()
      {

         using (System.IO.StreamReader reader = System.IO.File.OpenText(SchemaPathAndFileName))
         {
            //https://www.newtonsoft.com/json/help/html/LINQtoJSON.htm
            _jsonToken = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
         }

      }
      
   }
}
