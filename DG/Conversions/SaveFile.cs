using System.Collections.Generic;
using System.IO;
// File Types : Plain, Binary

//to save a file (f):

//f.fileName (path)  ->  string connectionString = (string)parameters.getParameterValue(p, "connectionString"); string connectionString = (string)parameters.getParameterValue(p, "connectionString");
//f.headings -> string[] _ParameterNames = new string[] { "a", "b" };
//f.Line(s)  -> string[] _Values = new string[] { "1", "2" };


namespace DG
{
   class SaveFile
   {
      /* how to use
       * 
       * preferene for arrays, asthis is compatible with the SQL class
     
         string Path = (string)parameters.getParameterValue(rfs.Parameters, "Path");
         string fn =  Path + @"\observed." + rfs.FileType.ToLower();

         string[] parameterNames = new string[] { "heading one", "heading two" };
         dynamic[] values = new dynamic[] { 1, "2" };

         SaveFile file = new SaveFile(fn, parameterNames, values);

       */

      //public SaveFile(PotentialFileDetails fd, string heading, string writeLine)
      //{
      //   Save(fd, heading, writeLine);
      //}

      ////represents one line to be saved
      //public SaveFile(PotentialFileDetails fn, string[] parameterNames, dynamic[] values)
      //{
      //   string heading = string.Join(",", parameterNames);
      //   string writeLine = string.Join(",", values);

      //   Save(fn, heading, writeLine);
      //}

      ////represents many lines to be saved
      //public SaveFile(PotentialFileDetails fd, string[] parameterNames, List<dynamic[]> listValues)
      //{

      //   bool saveToFile = app_CONST.saveToFile;
   
      //   if (saveToFile == true)
      //   {

      //      string heading = string.Join(",", parameterNames);

      //      for (int i = 0; i <= listValues.Count - 1; i++)
      //      {
      //         string writeLine = string.Join(",", listValues[i]);

      //         Save(fd, heading, writeLine);
      //      }
      //   }
      //}

      //public void Save(PotentialFileDetails fd, string heading, string writeLine)
      //{
      //   bool saveToFile = app_CONST.saveToFile;
      //   bool saveFileAsBinary = app_CONST.saveFileAsBinary;

      //  // var filenameAsBinary = "";

      //  // bool saveFileNameAsBinary = app_CONST.saveFileNameAsBinary;

      //   //if (saveFileNameAsBinary == true)
      //   //{

      //   //   if (fd.SaveAsFileName !="")
      //   //   {
      //   //      fd.SaveAsFileName = encoding.Encode(fd.SaveAsFileName);
      //   //      if (fd.SaveAsFileName.Length > 26 )
      //   //      {
      //   //         fd.SaveAsFileName = encoding.Encode(fd.SaveAsFileName).Substring(0, 26);
      //   //      }
               
      //   //   }

      //   //}

      //   if (saveToFile == true)
      //   {

      //      if (saveFileAsBinary == true)
      //      {
      //         SaveFileBinary(fd, heading, writeLine);
      //      }
      //      else
      //      {
      //         SaveFilePlainText(fd, heading, writeLine);
      //      }

      //   }
      //}

      //private void SaveFilePlainText(PotentialFileDetails fd, string heading, string writeLine)
      //{
      //   if (File.Exists(fd.FullPathAndFileName))
      //   {
      //      using (StreamWriter file = File.AppendText(fd.FullPathAndFileName))
      //      {
      //         file.WriteLine(writeLine);
      //      }
      //   }
      //   else if (!File.Exists(fd.FullPathAndFileName))
      //   {
      //      using (System.IO.StreamWriter file = new System.IO.StreamWriter(fd.FullPathAndFileName))
      //      {
      //         file.WriteLine(heading);
      //         file.WriteLine(writeLine);
      //      }
      //   }

      //}

      //private void SaveFileBinary(PotentialFileDetails fd, string heading, string writeLine)
      //{
      //   //stub
      //}

   }
   }
