using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DG
{

   //For each File in ObjectDefinition...
   //    Then; for each Column... 

   class ReadFiles
   {


      private readonly ObtainColumnDefinitions _od;
      private readonly OutputColumnDefinition _colDef;
      private readonly int _columnNullablePercentage;

      public ReadFiles(Parameter parameter, ObtainColumnDefinitions obtainColumnDefinitions)
      {
         _od = obtainColumnDefinitions;

         string outputFilename = _od.SourceFilename;
         int TotalNumberOfColumns = _od.TotalNumberOfColumns;
         int totalRecordCount = _od.TotalRecordCount;

         //To be returned to the calling routine.  Public?
         //Should be a dynamic object oif type <t>?
         //needs to be an array : matching column positions
          List<dynamic>[] randomData = new List<dynamic>[TotalNumberOfColumns];


         foreach (var colDef in _od.ColumnDefinitions)
         {
            _colDef = colDef;

            //is this ok? ie: Class level parameter changing each loop.
            _columnNullablePercentage = colDef.ColumnNullablePercentage;

            //available from colDef
            int columnPosition = colDef.ColumnPosition;
            string columnName = colDef.ColumnName;
            string columnIdentityField = colDef.ColumnIdentityField;
            string columnDataType = colDef.ColumnDataType;
            string mockSourceDataFilename = colDef.MockSourceDataFilename;
            string columnRatios = colDef.ColumnRatios;


            if (columnIdentityField.ToUpper() == "YES".ToUpper())
            {

               randomData[colDef.ColumnPosition - 1] = new List<dynamic>(_od.TotalRecordCount);
   
              // var items = Enumerable.Range(0, _od.TotalRecordCount).ToList();
              // randomData[colDef.ColumnPosition - 1] = items;
               ////it might be possible to do this in one step, rather than a loop.
               for (int i = 0; i < _od.TotalRecordCount; ++i) //in C# ++i is faster than i++
               {
                  randomData[colDef.ColumnPosition - 1].Add(i);
               }
            }
            else
            {

               string fileNameAndPath = (string) Parameter.GetParameterValue(parameter, mockSourceDataFilename);

               randomData[colDef.ColumnPosition - 1] = new List<dynamic>();

               //Only do this if not a IDENTITY Column

               //Dynamic object to store what ever 'list' of the object i need. String, INT, Decimals, Date, DateTime...
               List<string> allData = File.ReadLines(fileNameAndPath).ToList();

               if (_columnNullablePercentage == 0)
               {
                  NoNullsRequired( allData, randomData[colDef.ColumnPosition-1]);
               }
               else if (_columnNullablePercentage == 100)
               {
                  AllNullsRequired(randomData[colDef.ColumnPosition-1]);
               }
               else
               {
                  PercentageNullsRequired(allData, randomData[colDef.ColumnPosition-1]);
               }

            }

         }

      }

      private void NoNullsRequired(List<string> allData, List<dynamic> randomData)
      {
         var random = new Random();

         dynamic value = "";

         for (int i = 0; i < _od.TotalRecordCount; i++)
         {
            //is there a better way to perform this action?
            switch (_colDef.ColumnDataType.ToUpper())
            {

               case "INT": value = Int32.Parse(allData[random.Next(allData.Count)]); break;
               case "STRING": value = allData[random.Next(allData.Count)].ToString(); break;
               case "BOOLEAN": value = Convert.ToBoolean(allData[random.Next(allData.Count)]); break;
               case "DECIMAL":
                  var n = _colDef.ColumnRatios;
                  var arMinMax = n.Split(',');
                  value = GeneralMethods.RandomNumberBetween(Int32.Parse(arMinMax[0]), Int32.Parse(arMinMax[1]));
                  break; 

               default: break;
            }

            randomData.Add(value);

         }
      }

      private void AllNullsRequired(List<dynamic> randomData)
      {
         for (int i = 0; i < _od.TotalRecordCount; i++)
         {
            randomData.Add(null);
         }

      }

      private void PercentageNullsRequired(List<string> allData, List<dynamic> randomData)
      {
         var random = new Random();

         for (int i = 0; i < _od.TotalRecordCount; i++)
         {
            //Roll a random number. If the Random number is LESS THAN the ColumnNullablePercentage, then create the NULL Value.
            int randomNumber = (int)GeneralMethods.RandomNumberBetween(0, 100);

            if (randomNumber <= _columnNullablePercentage)
            {
               randomData.Add(null);
            }
            else //Do not add Null
            {
               dynamic value = "";
               //is there a more elegant way to 'cast' the datatype?
               switch (_colDef.ColumnDataType.ToUpper())
               {
                     
                  case "INT": value = Int32.Parse(allData[random.Next(allData.Count)]); break;
                  case "STRING": value = allData[random.Next(allData.Count)].ToString(); break;
                  case "BOOLEAN":  value = Convert.ToBoolean(allData[random.Next(allData.Count)]); break;
                  //case "DECIMAL": value = GeneralMethods.NextDecimal(random); break;
                  case "DECIMAL":
                     var n = _colDef.ColumnRatios;
                     var arMinMax = n.Split(',');
                     value = GeneralMethods.RandomNumberBetween(Int32.Parse(arMinMax[0]), Int32.Parse(arMinMax[1])); 
                     break;
                     
                  default: break;
               }
               
               randomData.Add(value);

            }

         }

      }



      //public static double RandomDouble(Random rand, double start, double end)
      //{
      //   return (rand.NextDouble() * Math.Abs(end - start)) + start;
      //}


   }

}