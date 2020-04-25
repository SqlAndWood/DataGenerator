using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace DG
{

   class CreateRandomRecords
   {
     
      public DataTable DTable { get; set; }
      private readonly ObtainColumnDefinitions _od;
      
      public CreateRandomRecords(Parameter parameter, ObtainColumnDefinitions obtainColumnDefinitions)
      {
         _od = obtainColumnDefinitions;

         DTable = DefineDataTable.Create(obtainColumnDefinitions);

         //This still might prove to be more useful as an Object, but will that just be storing the same inforamtion yet again?
         List<dynamic>[] rrd = new List<dynamic>[_od.TotalNumberOfColumns]; //Unsure if I should make this an object

         foreach (var colDef in _od.ColumnDefinitions)
         {

            string mockSourceDataFilename = colDef.MockSourceDataFilename;

            if (mockSourceDataFilename != "")
            {
               string file = (string) Parameter.GetParameterValue(parameter, mockSourceDataFilename);

               rrd[colDef.ColumnPosition - 1] = new List<dynamic>
               {
                  File.ReadLines(file).ToList()
               };

            }
            else //this is not necessary.
            {
               rrd[colDef.ColumnPosition - 1] = new List<dynamic>();
            }
         }

         var random = new Random();

         dynamic value = "";

         //This allows us to set Starting IDENTITY value, and increment  by a specific number (put into JSON File);.
         int startingValue = 0;
         int incrementingValue = 3;

         //Now populate the details with data read into the file.  Using _od.TotalRecordCount
         for (int i = 0; i < _od.TotalRecordCount; i++)
         {

            DataRow dr = DTable.NewRow();

            int columnCount = 1;

            //Loop each known column
            foreach (var colDef in _od.ColumnDefinitions)
            {


               //  rrd[colDef.ColumnPosition - 1].
               if (colDef.ColumnIdentityField.ToUpper() == "YES".ToUpper())
               {
                  dr[colDef.ColumnName] = startingValue; //dont perform a test with this one.
                  startingValue += incrementingValue;
               }
               else
               {
                  int randomNumber = (int)GeneralMethods.RandomNumberBetween(0, 100);
                  
                  if      ( (colDef.ColumnNullablePercentage == 100) || (randomNumber <= colDef.ColumnNullablePercentage ) ) { value = DBNull.Value; }
                  //else if?
                  else if ( (colDef.ColumnNullablePercentage == 0  ) || (randomNumber >= colDef.ColumnNullablePercentage ) ) 
                  {
                  
                     //is there a better way to perform this action?
                     switch (colDef.ColumnDataType.ToUpper())
                     {

                        case "INT":
                           value = Int32.Parse(rrd[columnCount][0][random.Next(rrd[columnCount][0].Count)]);
                           break;
                        case "STRING":
                           value = rrd[columnCount][0][random.Next(rrd[columnCount][0].Count)].ToString();
                           break;
                        case "BOOLEAN":
                           value = Convert.ToBoolean(rrd[columnCount][0][random.Next(rrd[columnCount][0].Count)]);
                           break;
                        case "DECIMAL":
                           var n = colDef.ColumnRatios;
                           var arMinMax = n.Split(',');
                           value = GeneralMethods.RandomNumberBetween(Int32.Parse(arMinMax[0]), Int32.Parse(arMinMax[1]));
                           break;

                        default: break;
                     }

                  }
                  columnCount += 1;

                  dr[colDef.ColumnName] = value;
       
               }
            }

            DTable.Rows.Add(dr);
         }

      }

   }

}