using System.Collections.Generic;
using System.IO;

namespace DG
{
   class SaveData
   {
      private readonly string _filename;
      private List<string> _cachedListToWrite;
      public int CacheAmount;
      
      public SaveData(DefinitionController obtainDataDefinitions)
      {

         CacheAmount = int.Parse(AppConst.GetValue(AppConst.FileCacheAmount).ToString());

         _filename = obtainDataDefinitions.TableDefinition.DataGeneratedPath + obtainDataDefinitions.TableDefinition.OutputFilename + "." + obtainDataDefinitions.TableDefinition.OutputFileType;

         //If the folder does not exist, create it. if the folder does exist, do nothing.
         DirectoryInfo di = Directory.CreateDirectory(obtainDataDefinitions.TableDefinition.DataGeneratedPath);

      }

      //TODO: Issue when File already exists
      public void InitiliseFile( string lineToWrite)
      {
         _cachedListToWrite = new List<string>();

         //Note that this does not close the file if it is open, but it does prevent an error message.
         //TODO: it may also be why file is not being overwritten
         // if (IsFileLocked(_filename))
         //  {
         File.WriteAllText(_filename, lineToWrite);
         //  }
      }

      public void CacheWriter(string line)//need a force boolean.
      {
        
         _cachedListToWrite.Add(line);
         
         if (_cachedListToWrite.Count >= CacheAmount)
         {

            using (StreamWriter sw = File.AppendText(_filename))
            {
               foreach (string l in _cachedListToWrite)
               {
                  sw.WriteLine(l);
               }
            }

            _cachedListToWrite.Clear();
         }

      }

      public void AppendFile( string lineToWrite)
      {
         using (StreamWriter sw = File.AppendText(_filename))
         {
            sw.WriteLine(lineToWrite);
         }
      }


      //public SaveData(DataTable dt, DefinitionController obtainDataDefinitions)
      //{

      //   StringBuilder sb = new StringBuilder();

      //   string[] columnNames = dt.Columns.Cast<DataColumn>().
      //      Select(column => column.ColumnName).
      //      ToArray();

      //  var delimiter = (string)obtainDataDefinitions.TableDefinition.OutputDelimiter == null || (string)obtainDataDefinitions.TableDefinition.OutputDelimiter == ""
      //      ? ","
      //      : (string)obtainDataDefinitions.TableDefinition.OutputDelimiter;

      //   sb.AppendLine(string.Join(delimiter, columnNames));

      //   foreach (DataRow row in dt.Rows)
      //   {
      //      string[] fields = row.ItemArray.
      //         Select(field => field.ToString()).
      //         ToArray();

      //      //Enable end user to dictate the delimiter.
      //      sb.AppendLine(string.Join(delimiter, fields));

      //   }

      //   //this is a parameter 
      //   string filename = obtainDataDefinitions.TableDefinition.DataGeneratedPath + obtainDataDefinitions.TableDefinition.OutputFilename + "." + obtainDataDefinitions.TableDefinition.OutputFileType;

      //  //If the folder does not exist, create it. if the folder does exist, do nothing.
      //  DirectoryInfo di = Directory.CreateDirectory(obtainDataDefinitions.TableDefinition.DataGeneratedPath);

      //  //Note that this does not close the file if it is open, but it does prevent an error message.
      //   if (IsFileLocked(filename))
      //   {
      //      File.WriteAllText(filename, sb.ToString());
      //   }

      //}

      private  bool IsFileLocked(string filename)
      {

         FileInfo file = new FileInfo(filename);
         
         try
         {
            using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
            {
               stream.Close();
            }
         }
         catch (IOException)
         {
            //the file is unavailable because it is:
            //still being written to
            //or being processed by another thread
            //or does not exist (has already been processed)
            return true;
         }

         //file is not locked
         return false;
      }
   }
}
