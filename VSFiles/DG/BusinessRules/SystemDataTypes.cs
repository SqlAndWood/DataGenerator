using System;

namespace DG
{
   class SystemDataTypes
   {

      public static string GetSystemType(string providedDataType)
      {
         string returnValue = "System.String";

         providedDataType = providedDataType.ToUpper();

         var cleanDataType = InString(providedDataType, "SYSTEM.");

         if (cleanDataType == "") cleanDataType = providedDataType;

         switch (cleanDataType.ToUpper())
         {
            case "TINYINT":
               returnValue = "System.Int16";
               break;

            case "INT":
               returnValue = "System.Int32";
               break;

            case "BIGINT":
               returnValue = "System.Int64";
               break;

            case "VARCHAR":
            case "NVARCHAR":
            case "CHAR":
            case "NCHAR":
            case "STRING":
               returnValue = "System.String";
               break;
            //TODO: Future iteration will see a separate DATE type.
            case "DATE":
            
            case "DATETIME":
               returnValue = "System.DateTime";
               break;

            case "TIME":
               returnValue = "System.TimeSpan";
               break;



            case "BOOLEAN":
               returnValue = "System.Boolean";
               break;

            case "DECIMAL":
            case "NUMERIC":
               returnValue = "System.Decimal";
               break;


         }

         return returnValue;

      }

      private static string InString(string strSource, string findCharacters)
      {

         strSource = strSource.ToLower();
         findCharacters = findCharacters.ToLower();

         if (strSource.Contains(findCharacters))
         {
            int start = strSource.IndexOf(findCharacters, 0, StringComparison.Ordinal) + findCharacters.Length;

            return strSource.Substring(start, strSource.Length - start);
         }
         else
         {
            return "";
         }
      }
      
   }
}
