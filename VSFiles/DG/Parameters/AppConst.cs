namespace DG
{
   public class AppConst
   {

      public enum DataFolders
      {
         DataGenerated,
         DataDefinitions,
         DataMimic
      }

      public enum ParameterNames
      {
         DataFolders,
         DefaultInteger,
         DefaultString
      }

      public enum DefaultDateRanges
      {
         StartDate = 19000101,
         EndDate = 20991231
      }

      public enum DefaultNumberRanges
      {
         StartDate = 0,
         EndDate = 999999999
      }

      //Unsure if needed
      public enum DefaultNegativeNumberRanges
      {
         StartDate = -999999999,
         EndDate = 0
      }



      //public enum Result { Success, Fail };

      //public enum ColumnDefinitionType { Schema, Column };

      //public enum Action_Type { Extract, Transform, Load };

      //public enum SaveTo { File, SQL, All };

      //public enum SaveFileState { New, Append };

      //public const bool ForceFileProcessing = true;

      //public const bool saveToSQLdb = true;

      //public const bool saveToFile = false;

      //public const bool saveFileAsBinary = false;

      //public const bool saveTimer = true;


      //public const char doubleQuotes = '\u0022';

      //public const int MaxLoopSampling = 3;

      //public const bool RandomiseTable = true;


      //An implementationnexists in SaveFile, however it needs work and not sure of the benifits
      //public const bool saveFileNameAsBinary = true;



   }
}
