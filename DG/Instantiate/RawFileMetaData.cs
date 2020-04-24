using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DG
{
   class RawFileMetaData
    {

      struct defineColumnPositions
      {
         public int StartPosition { get; set; }
         public int EndPosition { get; set; }

      }

      private const string PARAM = "filePathAndName";
      
      public string SchemaPathAndFileName { get; private set; }
      //public string Source_Provider{ get; private set; }
      //public string Source_Details{ get; private set; }
      public string Source_Filename{ get; private set; }
     // public string FileEncoding{ get; private set; }
      public string Filetype{ get; private set; }
     // public string LineBreak{ get; private set; }
      public string Delimeter{ get; private set; }
     // public string IncludePrimaryKeys{ get; private set; }

      public int FirstRowNumberContainingData{ get; private set; }
     // public int SkipTheseRecords{ get; private set; }
      public int TotalRecordCount{ get; private set; }
      public int TotalNumberOfColumns{ get; private set; }

     // public string FixedWidth{ get; private set; }
     // public int TotalCharactersInLine{ get; private set; }

     // public List<int> ColumnLengths{ get; private set; }
      public List<ColumnDefinition> ColumnDefinitions{ get; private set; }

    
      private JObject JsonToken;

      private readonly int DefaultInteger;
      private readonly string DefaultString;

      //public string getConcatenatedTableName() {
      //   return Source_Provider + "_" + Source_Details + "_" + Source_Filename.Replace("-", "_");
      //}

      public RawFileMetaData( Parameter p)
      {

        // string ClassName = MethodBase.GetCurrentMethod().DeclaringType.Name;
        // string CallingRoutine = MethodInfo.GetCurrentMethod().Name;

         DefaultInteger = (int)Parameter.GetParameterValue(p, "DefaultInteger");
         DefaultString = (string)Parameter.GetParameterValue(p, "DefaultString");

         SchemaPathAndFileName = (string)Parameter.GetParameterValue(p, PARAM);
         Schema_Read();

         //Populate from the JSON File into Variables.
         Source_Filename = (string)JsonToken["OutputFilename"];
     
         Filetype = (string)JsonToken["Filetype"];
         Delimeter = (string)JsonToken["Delimeter"] ;

         if (Filetype != null) Filetype = Filetype.ToLower();

         TotalNumberOfColumns = (int)JsonToken["TotalNumberOfColumns"];
         FirstRowNumberContainingData = (int)JsonToken["FirstRowNumberContainingData"];
         TotalRecordCount = (int)JsonToken["TotalRecordCount"];

         //calculate start and end for each field
         //List<defineColumnPositions> columnPositions = new List<defineColumnPositions>();

         ColumnDefinitions = new List<ColumnDefinition>();

         //? unsure if necessary 
         //int ic = TotalNumberOfColumns; // -1;// ColumnLengths.Count-2;
         for (int i = 0; i <= TotalNumberOfColumns - 1; i++)
         {

            int ColumnPos = ConvertToken<int>(JsonToken["ColumnDefinitions"][i]["ColumnPosition"], JTokenType.Integer, i);

            string ColumnName = ConvertToken<string>(JsonToken["ColumnDefinitions"][i]["ColumnName"], JTokenType.String, DefaultString);

            string ColumnIdentityField = ConvertToken<string>(JsonToken["ColumnDefinitions"][i]["ColumnIdentityField"], JTokenType.String, DefaultString);

            string ColumnDataType = ConvertToken<string>(JsonToken["ColumnDefinitions"][i]["ColumnDataType"], JTokenType.String, DefaultString);
           
            string MockSourceDataFilename = ConvertToken<string>(JsonToken["ColumnDefinitions"][i]["MockSourceDataFilename"], JTokenType.String, DefaultString);

            int ColumnNullablePercentage = ConvertToken<int>(JsonToken["ColumnDefinitions"][i]["ColumnNullablePercentage"], JTokenType.Integer, DefaultInteger);
           
            string ColumnRatios = ConvertToken<string>(JsonToken["ColumnDefinitions"][i]["ColumnRatios"], JTokenType.String, DefaultString);

            ColumnDefinitions.Add(new ColumnDefinition()
            {

               ColumnPosition = ColumnPos,
               ColumnName = ColumnName,

               ColumnIdentityField = ColumnIdentityField,
               ColumnDataType = ColumnDataType,
               MockSourceDataFilename = MockSourceDataFilename,
       
               ColumnNullablePercentage = ColumnNullablePercentage,
               ColumnRatios = ColumnRatios,
    
            });

         }


      }
           

      private dynamic ConvertToken<T>(JToken Token, JTokenType jt, T defaultReturnValue)
      {
        
            if (Token.Type == jt)  
            {
               return Token;
            }
            return defaultReturnValue;

      }

      private void Schema_Read()
      {

         using (System.IO.StreamReader reader = System.IO.File.OpenText(SchemaPathAndFileName))
         {
            //https://www.newtonsoft.com/json/help/html/LINQtoJSON.htm
            JsonToken = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
         }


      }


   }
}
