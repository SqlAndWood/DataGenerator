using System.Collections.Generic;

namespace DG
{
   public class CreateFileList
    {

      public List<dynamic> GenerateFileList(List<string> fileList)
      {

         List<dynamic> f = new List<dynamic>();

         foreach (string entry in fileList)
         {
            f.Add(entry);
         }
         
         return f;

      }

   }
}
