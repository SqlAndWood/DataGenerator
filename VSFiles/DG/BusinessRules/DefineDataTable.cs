using System.Data;

//https://www.codeproject.com/Articles/17169/Copy-Data-from-a-DataTable-to-a-SQLServer-Database
namespace DG
{

   class DefineDataTable
   {
      //The DataTable is Memory Hungry. Future iterations should aim to remove the Data Table with a Custom Object Class to perform this on our behalf.
      public static DataTable Create(DefinitionController ocd)
      {
         
         DataTable dTable = new DataTable(ocd.TableDefinition.OutputFilename);

         foreach (DefinitionColumn cd in ocd.TableDefinition.ColumnDefinitions)
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
