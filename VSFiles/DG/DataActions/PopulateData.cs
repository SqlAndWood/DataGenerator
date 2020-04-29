﻿using System;
using System.Collections.Generic;
using System.Data;

namespace DG
{
   class PopulateData
   {

      //TODO: Remove this Memory Hog
      public DataTable DTable { get; set; }

      private readonly DataRow _dr;

      private int[] _j;
        
      public PopulateData(DefinitionController dd, List<dynamic>[] preLoadedFieldData)
      {
         //TODO: Be aware this is MEMORY HUNGRY! I'd like to replace it with a less hungry object in the future.
         DTable = DefineDataTable.Create(dd);

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

            //TODO: Remove this Memory Hog
            DTable.Rows.Add(_dr);

         }


     
      }

      //Seems overkill to pass tableDef AND columnDefinitions.
      private void PopulateRecordAtRandom(DefinitionColumn colDef, List<dynamic>[] preLoadedFieldData)
      {
  
         //Set initially to NULL. Only replace the null if this 'Roll of the dice' indicates it should hold a value.
         dynamic value = DBNull.Value;

         //Each column has a % chance of being NULL. The DataDefinition ColumnNullablePercentage indicates that frequency.
         if (colDef.ColumnNullablePercentage == 0 || (int)RandomHelper.Instance.Next(0, 100) >= colDef.ColumnNullablePercentage) 
         {
               
    
                //value = preLoadedFieldData[colDef.ColumnPosition-1][_random.Next(preLoadedFieldData[colDef.ColumnPosition-1].Count)];

                value = preLoadedFieldData[colDef.ColumnPosition - 1][RandomHelper.Instance.Next(preLoadedFieldData[colDef.ColumnPosition - 1].Count)];

         }

         //TODO: Remove this Memory Hog
         _dr[colDef.ColumnName] = value;

      }

      //Would like to improve this so: 1,2, Null, 3, Null, 4 etc.
      private void PopulateRecordIncrementally(DefinitionColumn colDef, List<dynamic>[] preLoadedFieldData, int rowNumber)
      {

         //Set initially to NULL. Only replace the null if this 'Roll of the dice' indicates it should hold a value.
         dynamic value = DBNull.Value;

         //Each column has a % chance of being NULL. The DataDefinition ColumnNullablePercentage indicates that frequency.
         if (colDef.ColumnNullablePercentage == 0 || (int)RandomHelper.Instance.Next(0, 100) >= colDef.ColumnNullablePercentage)
         {

            //The wraparound works fine (base 0) .. Thus StartWith = 0 is prefered over StartWith = 1
            var wrapAroundNumber = _j[colDef.ColumnPosition - 1] % preLoadedFieldData[colDef.ColumnPosition - 1].Count ;

            value = preLoadedFieldData[colDef.ColumnPosition - 1][wrapAroundNumber];
 
            //This is done to maintain an accurate 'Incremental' list of numbers. 
            _j[colDef.ColumnPosition - 1] += 1;
        
         }

         //TODO: Remove this Memory Hog
         _dr[colDef.ColumnName] = value;
      }

   }
}
