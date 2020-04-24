using System.Reflection;

namespace DG
{
   class EController
   {

      public Parameter Parameters { get; set; }

      public RawFileMetaData RawFileMetaData { get; set; }

      
      public EController()
      {

         //string ClassName = MethodBase.GetCurrentMethod().DeclaringType.Name;
         //string CallingRoutine = MethodBase.GetCurrentMethod().Name;

         Parameters = new Parameter();
         RawFileMetaData = new RawFileMetaData(Parameters);

         //string Path = (string)Parameter.getParameterValue(p, "abc");
         //string FileNameAndPath = Path + @"\" + RawFileMetaData.Source_Filename + @"." + RawFileMetaData.Filetype.ToLower();




      }


   }
}
