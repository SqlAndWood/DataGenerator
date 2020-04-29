using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace DG
{
   class SaveGeneratedData
   {

      public SaveGeneratedData(DataTable dt, ObtainDataDefinitions obtainDataDefinitions)
      {

         StringBuilder sb = new StringBuilder();

         string[] columnNames = dt.Columns.Cast<DataColumn>().
            Select(column => column.ColumnName).
            ToArray();

        var Delimiter = (string)obtainDataDefinitions.TableDefinition.OutputDelimiter == null || (string)obtainDataDefinitions.TableDefinition.OutputDelimiter == ""
            ? ","
            : (string)obtainDataDefinitions.TableDefinition.OutputDelimiter;
      
         sb.AppendLine(string.Join(Delimiter, columnNames));

         foreach (DataRow row in dt.Rows)
         {
            string[] fields = row.ItemArray.
               Select(field => field.ToString()).
               ToArray();

            //Enable end user to dictate the delimiter.
            sb.AppendLine(string.Join(Delimiter, fields));

         }

         //this is a parameter 
         string filename = obtainDataDefinitions.TableDefinition.DataGeneratedPath + obtainDataDefinitions.TableDefinition.OutputFilename + "." + obtainDataDefinitions.TableDefinition.OutputFileType;
            
        //If the folder does not exist, create it. if the folder does exist, do nothing.
        DirectoryInfo di = Directory.CreateDirectory(obtainDataDefinitions.TableDefinition.DataGeneratedPath);
  
        File.WriteAllText(filename, sb.ToString());

      }
   }
}
