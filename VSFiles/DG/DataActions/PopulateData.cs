using System;
using System.Collections.Generic;
using System.Text;

namespace DG
{
   class PopulateData
   {

      private  int[] _j;

      private SaveData _sd;

      public PopulateData(DefinitionController dd, List<dynamic>[] preLoadedFieldData)
      {

         _sd = new SaveData(dd);

         _sd.InitiliseFile(CreateHeading(dd));

         //This is really a deeper loop, where each record will be saved to the file (append)
         CreateRecord(dd, preLoadedFieldData);

      }

      private string CreateHeading(DefinitionController dd)
      {

         string[] fields = new string[dd.TableDefinition.ColumnDefinitionCount];
     
         foreach (var colDef in dd.TableDefinition.ColumnDefinitions)
         {
            fields[colDef.ColumnPosition-1] += colDef.ColumnName;
         }

         StringBuilder columnHeadings = new StringBuilder();
         columnHeadings.AppendLine(string.Join(",", fields));

         return columnHeadings.ToString();
      }


      //Populate File with records
      private void CreateRecord(DefinitionController dd, List<dynamic>[] preLoadedFieldData)
      {
         
         _j = new int[dd.TableDefinition.ColumnDefinitionCount];

         for (int rowNumber = 0; rowNumber < dd.TableDefinition.OutputRecordCount; rowNumber++)
         {

            List<dynamic> dr = new List<dynamic>( );

            foreach (var colDef in dd.TableDefinition.ColumnDefinitions)
            {

               var colLoadDataMimicMethod = colDef.ColumnLoadDataMimicMethod.ToUpper();

               switch (colLoadDataMimicMethod)
               {

                  case "FILE": //AppConst.LoadDataMimicMethod.File -> Load Randomly
                  case "RANDOM": //AppConst.LoadDataMimicMethod.Random -> Load Randomly
                     dr.Add( PopulateRecordAtRandom(colDef, preLoadedFieldData) );
                     break;

                  case "INCREMENTAL": //AppConst.LoadDataMimicMethod.Incremental;-> Load in Array Order a[i] = Value[i]
                     dr.Add( PopulateRecordIncrementally(colDef, preLoadedFieldData, rowNumber) );
                     break;

               }

            }

            SaveRecord(dd, dr);

         }

      }

      private void SaveRecord(DefinitionController dd, List<dynamic> dr)
      {

         dynamic holingCell = "";

         foreach (var ob in dr)
         {
            var l = ob.GetType();

            if (l.Equals(typeof(string)))
            {
               string qs = ob.ToString();
               holingCell += qs.SurroundWithDoubleQuotes() + ",";
            }
            else
            {
               holingCell += ob + ",";
            }

         }
         string lineToWrite = holingCell.ToString();
         int strLength = lineToWrite.Length;
         lineToWrite = lineToWrite.Substring(0, strLength - 1);


         _sd.CacheWriter(lineToWrite);


      }

      //Seems overkill to pass tableDef AND columnDefinitions.
      private dynamic PopulateRecordAtRandom(DefinitionColumn colDef, List<dynamic>[] preLoadedFieldData)
      {
  
         //Set initially to NULL. Only replace the null if this 'Roll of the dice' indicates it should hold a value.
         dynamic value = DBNull.Value;

         //Each column has a % chance of being NULL. The DataDefinition ColumnNullablePercentage indicates that frequency.
         if (colDef.ColumnNullablePercentage == 0 || (int)RandomHelper.Instance.Next(0, 100) >= colDef.ColumnNullablePercentage) 
         {
            value = preLoadedFieldData[colDef.ColumnPosition - 1][RandomHelper.Instance.Next(preLoadedFieldData[colDef.ColumnPosition - 1].Count)];
         }

         return value;
       

      }

      //Would like to improve this so: 1,2, Null, 3, Null, 4 etc.
      private dynamic PopulateRecordIncrementally(DefinitionColumn colDef, List<dynamic>[] preLoadedFieldData, int rowNumber)
      {

         //Set initially to NULL. Only replace the null if this 'Roll of the dice' indicates it should hold a value.
         dynamic value = DBNull.Value;

         //Each column has a % chance of being NULL. The DataDefinition ColumnNullablePercentage indicates that frequency.
         if (colDef.ColumnNullablePercentage == 0 || (int)RandomHelper.Instance.Next(0, 100) >= colDef.ColumnNullablePercentage)
         {

            //The wraparound works fine (base 0) .. Thus StartWith = 0 is preferred over StartWith = 1
            var wrapAroundNumber = _j[colDef.ColumnPosition - 1] % preLoadedFieldData[colDef.ColumnPosition - 1].Count ;

            value = preLoadedFieldData[colDef.ColumnPosition - 1][wrapAroundNumber];
 
            //This is done to maintain an accurate 'Incremental' list of numbers. 
            _j[colDef.ColumnPosition - 1] += 1;
        
         }

         return value;
         
      }

   }
}
