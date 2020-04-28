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
         StartNumber = 0,
         EndNumber = 1000000,
         Length = 24
      }

      public enum DefaultTimeRange
      {
         StartTime = 0,
         EndTime = 86400  //24 hours * 3600
      }

      //There needs to be a warning on this, as it is a x^y function due to the decimal places.
      public enum DefaultDecimalRanges
      {
         StartNumber = 0,
         EndNumber = 50,
         DecimalPlaces = 3
      }

      public enum DefaultStringRanges
      {
         StartNumber = 0,
         EndNumber = 50,
         Length = 250
      }

      public enum LoadDataMimicMethod
      {
         File,
         Incremental,
         Random 
      }

   }
}
