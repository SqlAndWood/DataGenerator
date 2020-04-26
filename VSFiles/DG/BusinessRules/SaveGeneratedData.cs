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

        // var Quote = '\u0022';

         string[] columnNames = dt.Columns.Cast<DataColumn>().
            Select(column => column.ColumnName).
            ToArray();

         sb.AppendLine(string.Join( "," , columnNames));

         foreach (DataRow row in dt.Rows)
         {
            string[] fields = row.ItemArray.Select(field => field.ToString()).
               ToArray();

            sb.AppendLine(string.Join( "," , fields));
         }

         //this is a parameter 
         string filename = obtainDataDefinitions.TableDefinition.DataGeneratedPath + obtainDataDefinitions.TableDefinition.OutputFilename + "." + obtainDataDefinitions.TableDefinition.OutputFileType;

         File.WriteAllText(filename, sb.ToString());

      }
   }
}
