using System.Data;

//https://www.codeproject.com/Articles/17169/Copy-Data-from-a-DataTable-to-a-SQLServer-Database
namespace DG
{

   class DefineDataTable
   {

      public static DataTable Create(ObtainDataDefinitions ocd)
      {
         
         DataTable dTable = new DataTable(ocd.TableDefinition.OutputFilename);

         foreach (ColumnDefinition cd in ocd.TableDefinition.ColumnDefinitions)
         {
            string s = SystemDataTypes.GetSystemType(cd.ColumnDataType);
            var column = new DataColumn(cd.ColumnName)
                                                         {
                                                            DataType = System.Type.GetType(s), 
                                                            ReadOnly = true, 
                                                            Unique = false
                                                         };
            
            column.AllowDBNull = true;
        
            dTable.Columns.Add(column);

         }

         return dTable;

      }


   }
}
