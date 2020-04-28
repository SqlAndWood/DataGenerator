using System;
using System.Collections.Generic;
using System.Data;

namespace DG
{
   class PopulateData
   {

      public DataTable DTable { get; set; }
      private readonly Random _random;

      private readonly DataRow _dr;

      private int[] _j;
      
      public PopulateData(ObtainDataDefinitions dd, List<dynamic>[] preLoadedFieldData)
      {
         //TODO: Be aware this is MEMORY HUNGRY! I'd like to replace it with a less hungry object in the future.
         DTable = DefineDataTable.Create(dd);
         _random = new Random();

         _j = new int[dd.TableDefinition.ColumnDefinitionCount];

         for (int rowNumber = 0; rowNumber < dd.TableDefinition.OutputRecordCount; rowNumber++)
         {
            
            //TODO: Be aware this is MEMORY HUNGRY! I'd like to replace it with a less hungry object in the future.
            _dr = DTable.NewRow();

            foreach (var colDef in dd.TableDefinition.ColumnDefinitions)
            {

               var colLoadDataMimicMethod = colDef.ColumnLoadDataMimicMethod.ToUpper();
 
               switch (colLoadDataMimicMethod)
               {

                  case "FILE": //AppConst.LoadDataMimicMethod.File -> Load Randomly
                  case "RANDOM": //AppConst.LoadDataMimicMethod.Random -> Load Randomly
                     PopulateRecordAtRandom(colDef, preLoadedFieldData);
                     break;

                  case "INCREMENTAL": //AppConst.LoadDataMimicMethod.Incremental;-> Load in Array Order a[i] = Value[i]
                     PopulateRecordIncrementally(colDef, preLoadedFieldData, rowNumber);
                     break;

               }

            }

            //Memory Hog
            DTable.Rows.Add(_dr);

         }
      }

      //Seems overkill to pass tableDef AND columnDefinitions.
      private void PopulateRecordAtRandom(ColumnDefinition colDef, List<dynamic>[] preLoadedFieldData)
      {
  
         //Set initially to NULL. Only replace the null if this 'Roll of the dice' indicates it should hold a value.
         dynamic value = DBNull.Value;

         //Each column has a % chance of being NULL. The DataDefinition ColumnNullablePercentage indicates that frequency.
         if (colDef.ColumnNullablePercentage == 0 || (int) GeneralMethods.RandomNumberBetween(0, 100) >= colDef.ColumnNullablePercentage) 
         {
            //value = preLoadedFieldData[_ColumnPosition][_random.Next(preLoadedFieldData[_ColumnPosition].Count)];
            value = preLoadedFieldData[colDef.ColumnPosition-1][_random.Next(preLoadedFieldData[colDef.ColumnPosition-1].Count)];
         }

         //Memory Hog
         _dr[colDef.ColumnName] = value;

      }

      //Would like to improve this so: 1,2, Null, 3, Null, 4 etc.
      private void PopulateRecordIncrementally(ColumnDefinition colDef, List<dynamic>[] preLoadedFieldData, int rowNumber)
      {

         //Set initially to NULL. Only replace the null if this 'Roll of the dice' indicates it should hold a value.
         dynamic value = DBNull.Value;

         //Each column has a % chance of being NULL. The DataDefinition ColumnNullablePercentage indicates that frequency.
         if (colDef.ColumnNullablePercentage == 0 || (int)GeneralMethods.RandomNumberBetween(0, 100) >= colDef.ColumnNullablePercentage)
         {

            //I need a way to wrap around numbers.
            var wrapAroundNumber = _j[colDef.ColumnPosition - 1] % preLoadedFieldData[colDef.ColumnPosition - 1].Count;

            value = preLoadedFieldData[colDef.ColumnPosition - 1][wrapAroundNumber];

            //This is done to maintain an accurate 'Incremental' list of numbers. 
            _j[colDef.ColumnPosition - 1] += 1;
        
         }
        
         //Memory Hog
         _dr[colDef.ColumnName] = value;
      }

   }
}
