
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

            case "DATE":
            case "DATETIME":
            case "TIME":
               returnValue = "System.DateTime";
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
            int Start = strSource.IndexOf(findCharacters, 0) + findCharacters.Length;

            return strSource.Substring(Start, strSource.Length - Start);
         }
         else
         {
            return "";
         }
      }


      //public static dynamic GetSystemTypeReturnValue(Type DataType)
      //{
      //   dynamic returnValue = "";

      //   switch (DataType.FullName.ToUpper())
      //   {
      //      case "SYSTEM.TINYINT":
      //      case "SYSTEM.INT":
      //      case "SYSTEM.INT16":
      //      case "SYSTEM.INT32":
      //      case "SYSTEM.INT64":
      //      case "SYSTEM.BIGINT":
      //         returnValue = (int)-99;
      //         break;

      //      case "SYSTEM.VARCHAR":
      //      case "SYSTEM.NVARCHAR":
      //      case "SYSTEM.CHAR":
      //      case "SYSTEM.NCHAR":
      //      case "SYSTEM.STRING":
      //         returnValue = (string)"";
      //         break;

      //      case "SYSTEM.DATE":
      //      case "SYSTEM.DATETIME":
      //      case "SYSTEM.TIME":
      //         returnValue = (DateTime)DateTime.MinValue;
      //         break;

      //      case "SYSTEM.BOOLEAN":
      //         returnValue = (bool?)null;
      //         break;

      //      case "SYSTEM.DECIMAL":
      //      case "SYSTEM.NUMERIC":
      //         returnValue = (decimal)0.000;
      //         break;


      //   }

      //   return returnValue;

      //}

      //public static DataType GetSmoDataType(string dataType, int length, int precission, int scale)
      //{
      //   DataType DTTemp = null;
                  
      //   dataType = dataType.ToUpper();

      //   var cleanDataType = StringConversions.inString(dataType, "SYSTEM.");
      //   if (cleanDataType == "") cleanDataType = dataType;

      //   switch (cleanDataType)
      //   {
      //      //case "SYSTEM.TINYINT":
      //      case "TINYINT":
      //      //case "SYSTEM.INT16":
      //      case "INT16":

      //         DTTemp = DataType.TinyInt;
      //         break;

      //     // case "SYSTEM.INT":
      //      case "INT":
      //      //case "SYSTEM.INT32":
      //      case "INT32":

      //         DTTemp = DataType.Int;
      //         break;

      //     // case "SYSTEM.INT64":
      //      case "INT64":

      //     // case "SYSTEM.BIGINT":
      //      case "BIGINT":

      //         DTTemp = DataType.BigInt;
      //         break;


      //      //case "SYSTEM.VARCHAR":
      //      case "VARCHAR":
      //      //case "SYSTEM.NVARCHAR":
      //      case "NVARCHAR":
      //      //case "SYSTEM.CHAR":
      //      case "CHAR":
      //      //case "SYSTEM.NCHAR":
      //      case "NCHAR":
      //      //case "SYSTEM.STRING":
      //      case "STRING":

      //         if (length == 0  || length==-99) length = 56;
      //         DTTemp = DataType.VarChar(length);
      //         break;

      //      //case "SYSTEM.DATE":
      //      case "DATE":
      //      //case "SYSTEM.DATETIME":
      //      case "DATETIME":
      //     // case "SYSTEM.TIME":
      //      case "TIME":
      //      case "DATETIMEOFFSET":

      //         DTTemp = DataType.DateTimeOffset(2);
      //         break;

      //      //case "SYSTEM.BOOLEAN":
      //      case "BOOLEAN":

      //         DTTemp = DataType.Bit;
      //         break;

      //      //case "SYSTEM.DECIMAL":
      //      case "DECIMAL":
      //      //case "SYSTEM.NUMERIC":
      //      case "NUMERIC":

      //         if (precission == 0 || precission == -99) precission = 10;
      //         if (scale == 0 || scale == -99) scale = 3;
               
      //         DTTemp = DataType.Decimal(precission, scale);
      //         break;

      //   }
      //   return DTTemp;
      //}


   }
}
