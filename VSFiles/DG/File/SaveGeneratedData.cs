using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace DG
{

   class SaveGeneratedData
   {

      public SaveGeneratedData(DataTable dt, ObtainColumnDefinitions obtainColumnDefinitions)
      {

         StringBuilder sb = new StringBuilder();

         string[] columnNames = dt.Columns.Cast<DataColumn>().
            Select(column => column.ColumnName).
            ToArray();

         sb.AppendLine(string.Join(",", columnNames));

         foreach (DataRow row in dt.Rows)
         {
            string[] fields = row.ItemArray.Select(field => field.ToString()).
               ToArray();

            sb.AppendLine(string.Join(",", fields));
         }

         //this is a parameter 
         string filename = "C:\\git\\DataGenerator\\GeneratedData\\" +  obtainColumnDefinitions.SourceFilename + ".csv";
         
         File.WriteAllText(filename, sb.ToString());

      }
   }
}
