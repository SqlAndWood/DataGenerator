using System.Collections.Generic;
using System.Data;

//https://www.codeproject.com/Articles/17169/Copy-Data-from-a-DataTable-to-a-SQLServer-Database
namespace DG
{

   class DefineDataTable
   {

      public static DataTable Create( ObtainColumnDefinitions ocd)
      {
         
         var PrimaryKeyColumns = new List<string>();
         DataTable DTable = new DataTable(ocd.SourceFilename);

         DataColumn column;
         

         foreach (OutputColumnDefinition cd in ocd.ColumnDefinitions)
         {
            string s = SystemDataTypes.GetSystemType(cd.ColumnDataType);
            column = new DataColumn(cd.ColumnName) {DataType = System.Type.GetType(s), ReadOnly = true, Unique = false};



            if (cd.ColumnIdentityField.ToUpper() == "YES")
            {
               PrimaryKeyColumns.Add(cd.ColumnName);
               column.AllowDBNull = false;
               AddPrimaryKey(cd, PrimaryKeyColumns, DTable);
            }
            else
            {
               column.AllowDBNull = true;
            }
            // Add the Column to the DataColumnCollection.
            DTable.Columns.Add(column);

         }

         return DTable;

      }


      private static void AddPrimaryKey(OutputColumnDefinition cd, List<string> PrimaryKeyColumns, DataTable table)
      {

         if (cd.ColumnIdentityField.ToUpper() == "YES")
         {
            DataColumn[] PrimaryKeyDataColumns = new DataColumn[PrimaryKeyColumns.Count];

            int lowerFirstArray = PrimaryKeyDataColumns.GetLowerBound(0);
            int upperFirstArray = PrimaryKeyDataColumns.GetUpperBound(0);

            for (int i = lowerFirstArray; i <= upperFirstArray; i++)
            {
               PrimaryKeyDataColumns[i] = table.Columns[PrimaryKeyColumns[i]];
            }

            table.PrimaryKey = PrimaryKeyDataColumns;

         }
      }

   }
}
