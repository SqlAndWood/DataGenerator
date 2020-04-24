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

      //To be returned to the calling routine.  Public?
      public List<string> RandomData = new List<string>();

      private readonly OutputDefinitions _od;
      private readonly int _columnNullablePercentage;

      public ReadFiles(Parameter parameter, OutputDefinitions outputDefinitions)
      {
         _od = outputDefinitions;

         // string representSecondLoopFilename = @"Surnamea";
         // int representSecondLoopColumnNullablePercentage = outputDefinitions.ColumnDefinitions[1].ColumnNullablePercentage;

         string representSecondLoopFilename = @"1234567890";
         int representSecondLoopColumnNullablePercentage = outputDefinitions.ColumnDefinitions[2].ColumnNullablePercentage;

         //From here on in would be the required code.
         _columnNullablePercentage = representSecondLoopColumnNullablePercentage;
         string fileNameAndPath = (string)Parameter.GetParameterValue(parameter, representSecondLoopFilename);
         
         //Dynamic object to store what ever 'list' of the object i need. String, INT, Decimals, Date, DateTime...
         List<string> allData = File.ReadLines(fileNameAndPath).ToList();
         
         if (_columnNullablePercentage == 0)
         {
            NoNullsRequired( allData,  RandomData);
         }
         else if (_columnNullablePercentage == 100)
         {
            AllNullsRequired(RandomData);
         }
         else {
            PercentageNullsRequired(allData, RandomData);
         }
         
         //if outputDefinitions.ColumnDefinitions[2].ColumnDataType == 'INT' THEN List<int> myStringList = RandomData.Select(s => int.Parse(s)).ToList();

      }

      private void NoNullsRequired(List<string> allData, List<string> randomData)
      {
         var random = new Random();
         
         for (int i = 0; i < _od.TotalRecordCount; i++)
         {
            //test to make sure this is always random!
            randomData.Add(allData[random.Next(allData.Count)]);
         }
      }

      private void AllNullsRequired(List<string> randomData)
      {
         for (int i = 0; i < _od.TotalRecordCount; i++)
         {
            randomData.Add(null);
         }

      }

      private void PercentageNullsRequired(List<string> allData, List<string> randomData)
      {
         var random = new Random();

         //Just a general between two numbers, hovering around the NullablePercentage
         int randomNumber = random.Next((_columnNullablePercentage / 2) + 1, _columnNullablePercentage + (_columnNullablePercentage / 2));

         for (int i = 0; i < _od.TotalRecordCount; i++)
         {
         
            //when n is divisible by ColumnNullablePercentage then force a null value.
            if (i % randomNumber == 0)
            {
               randomData.Add(null);
               randomNumber = random.Next((_columnNullablePercentage / 2) + 1 , _columnNullablePercentage + (_columnNullablePercentage / 2));
            }
            else
            {
               randomData.Add(allData[random.Next(allData.Count)]);
            }

         }

      }

   }

}