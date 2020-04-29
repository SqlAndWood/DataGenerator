using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace DG
{
   class SaveData
   {

      public SaveData(DataTable dt, DefinitionController obtainDataDefinitions)
      {

         StringBuilder sb = new StringBuilder();

         string[] columnNames = dt.Columns.Cast<DataColumn>().
            Select(column => column.ColumnName).
            ToArray();

        var delimiter = (string)obtainDataDefinitions.TableDefinition.OutputDelimiter == null || (string)obtainDataDefinitions.TableDefinition.OutputDelimiter == ""
            ? ","
            : (string)obtainDataDefinitions.TableDefinition.OutputDelimiter;
      
         sb.AppendLine(string.Join(delimiter, columnNames));

         foreach (DataRow row in dt.Rows)
         {
            string[] fields = row.ItemArray.
               Select(field => field.ToString()).
               ToArray();

            //Enable end user to dictate the delimiter.
            sb.AppendLine(string.Join(delimiter, fields));

         }

         //this is a parameter 
         string filename = obtainDataDefinitions.TableDefinition.DataGeneratedPath + obtainDataDefinitions.TableDefinition.OutputFilename + "." + obtainDataDefinitions.TableDefinition.OutputFileType;
            
        //If the folder does not exist, create it. if the folder does exist, do nothing.
        DirectoryInfo di = Directory.CreateDirectory(obtainDataDefinitions.TableDefinition.DataGeneratedPath);

        //Note that this does not close the file if it is open, but it does prevent an error message.
         if (IsFileLocked(filename))
         {
            File.WriteAllText(filename, sb.ToString());
         }

      }

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
