﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DG
{
    class GenerateData
   {
        public List<dynamic>[] PreLoadedFieldData { get; set; }
      
        //The Pre Load depends on several things.
        /*
           "LoadDataMimicMethod"
              -> FILE => load File Listed in "DataMimicFilename"
              -> INCREMENTAL => Data Type {Date, Time, Number, Decimal (up to 1 decimal place only)}

           These three fields are used to pre load the data sets, minimise the amount of memory required and the simplicity to randomly select in the next phase.
              "StartWith"
              "EndWith"
              "Length"
         */

      public GenerateData(DefinitionController dd)
      {

         //This still might prove to be more useful as an Object, but will that just be storing the same information yet again?
         PreLoadedFieldData = new List<dynamic>[dd.TableDefinition.ColumnDefinitions.Count()];

         foreach (var colDef in dd.TableDefinition.ColumnDefinitions)
         {

            var colLoadDataMimicMethod = colDef.ColumnLoadDataMimicMethod.ToUpper();
            var colDataType = colDef.ColumnDataType.ToUpper();

            switch (colLoadDataMimicMethod)
            {
               //When a FILE needs to be loaded.
               case "FILE":  //AppConst.LoadDataMimicMethod.File;
                  LoadFromFile(colDef);
                  break;

               case "INCREMENTAL":  //AppConst.LoadDataMimicMethod.Incremental;
               case "RANDOM":       //AppConst.LoadDataMimicMethod.Random;

                  //Load the Data, based on the required Data Types.
                  switch (colDataType)
                  { 

                     case "STRING":
                        LoadIncrementalString(colDef);
                        break; //No such thing to create random String.  Use a file. Or should I allow a fake String generator to be created?

                     case "BOOLEAN":
                        LoadIncrementalBoolean(colDef);
                        break; 

                     case "DATE": //Need to create a dedicated date
                        LoadIncrementalDate(colDef);
                        break;

                     case "DATETIME":
                        LoadIncrementalDateTime(colDef);
                        break;

                     case "TIME":
                        LoadIncrementalTime(colDef);
                        break;

                     case "INTEGER":
                     case "NUMBER":
                     case "INT":
                        LoadIncrementalNumber(colDef);
                        break;

                     case "NUMERIC": 
                     case "DECIMAL":
                     case "DOUBLE":
                        LoadIncrementalDecimal(colDef);
                        break;

                  }

                  break;

               default:
                  //Console.WriteLine($"An unexpected value ("h");
                  break;
            }

         }

      }

      private void LoadFromFile(DefinitionColumn colDef)
      {

         var file = colDef.ColumnDataMimicPathFileName;

         //Note that no 'filtering' is put into place on loading from a file. 
         var fileList = File.ReadLines(file).ToList();

         CreateFileList idt = new CreateFileList();

         var fIncremental = idt.GenerateFileList(fileList);

         PreLoadedFieldData[colDef.ColumnPosition - 1] = new List<dynamic>(fIncremental);

      }
        
      // INCREMENTAL => Data Type { Date, Time, Number, Decimal (up to 1 decimal place only)}
      private void LoadIncrementalDateTime(DefinitionColumn colDef)
      {
         
         var startDate = colDef.ColumnStartWith == ""
            ? (int) AppConst.DefaultDateRanges.StartDate
            : int.Parse(colDef.ColumnStartWith);

         var endDate = colDef.ColumnEndWith == ""
            ? (int) AppConst.DefaultDateRanges.EndDate
            : int.Parse(colDef.ColumnEndWith);

         CreateIncrementingDateTimeList idt = new CreateIncrementingDateTimeList();

         var datesIncremental = idt.GenerateIncrementingDateTimeList(startDate, endDate);

         PreLoadedFieldData[colDef.ColumnPosition - 1] = new List<dynamic>(datesIncremental);
         
      }
      private void LoadIncrementalDate(DefinitionColumn colDef)
      {

         var startDate = colDef.ColumnStartWith == ""
            ? (int)AppConst.DefaultDateRanges.StartDate
            : int.Parse(colDef.ColumnStartWith);

         var endDate = colDef.ColumnEndWith == ""
            ? (int)AppConst.DefaultDateRanges.EndDate
            : int.Parse(colDef.ColumnEndWith);

         CreateIncrementingDateList idt = new CreateIncrementingDateList();

         var datesIncremental = idt.GenerateIncrementingDateList(startDate, endDate);

         PreLoadedFieldData[colDef.ColumnPosition - 1] = new List<dynamic>(datesIncremental);

      }

      private void LoadIncrementalTime(DefinitionColumn colDef)
      {

         var startTime = colDef.ColumnStartWith == ""
            ? (int)AppConst.DefaultTimeRange.StartTime
            : int.Parse(colDef.ColumnStartWith);

         var endTime = colDef.ColumnEndWith == ""
            ? (int)AppConst.DefaultTimeRange.EndTime
            : int.Parse(colDef.ColumnEndWith);
         
         CreateIncrementingTimeList idt = new CreateIncrementingTimeList();

         var ti = idt.GenerateIncrementingTimeList(startTime, endTime);
      
         PreLoadedFieldData[colDef.ColumnPosition - 1] = new List<dynamic>(ti);

      }

      private void LoadIncrementalNumber(DefinitionColumn colDef)
      {

         var startNumber = colDef.ColumnStartWith == ""
            ? (int)AppConst.DefaultNumberRanges.StartNumber
            : int.Parse(colDef.ColumnStartWith);

         var endNumber = colDef.ColumnEndWith == ""
            ? (int)AppConst.DefaultNumberRanges.EndNumber
            : int.Parse(colDef.ColumnEndWith);

         CreateIncrementingNumberList idt = new CreateIncrementingNumberList();

         var numbersIncremental = idt.GenerateIncrementingNumberList(startNumber, endNumber);

         PreLoadedFieldData[colDef.ColumnPosition - 1] = new List<dynamic>(numbersIncremental);

      }

      private void LoadIncrementalDecimal(DefinitionColumn colDef)
      {

         var startNumber = colDef.ColumnStartWith == ""
            ? (decimal)AppConst.DefaultDecimalRanges.StartNumber
            : decimal.Parse(colDef.ColumnStartWith);

         var endNumber = colDef.ColumnEndWith == ""
            ? (decimal)AppConst.DefaultDecimalRanges.EndNumber
            : decimal.Parse(colDef.ColumnEndWith);

         var decimalPlaces = colDef.ColumnLength == ""
            ? (int)AppConst.DefaultDecimalRanges.DecimalPlaces
            : int.Parse(colDef.ColumnLength);

         CreateIncrementingDecimalList idt = new CreateIncrementingDecimalList();

         var ld = idt.GenerateIncrementingDecimalList(startNumber, endNumber, decimalPlaces);

         PreLoadedFieldData[colDef.ColumnPosition - 1] = new List<dynamic>(ld);

      }

      private void LoadIncrementalBoolean(DefinitionColumn colDef)
      {

         CreateIncrementingBooleanList idt = new CreateIncrementingBooleanList();

         List<dynamic> dl = idt.GenerateIncrementalBooleanList();

         PreLoadedFieldData[colDef.ColumnPosition - 1] = new List<dynamic>(dl);

      }

      private void LoadIncrementalString(DefinitionColumn colDef)
      {

         var start = colDef.ColumnLength == ""
            ? (int)AppConst.DefaultStringRanges.StartNumber
            : int.Parse(colDef.ColumnStartWith);

         var end = colDef.ColumnLength == ""
            ? (int)AppConst.DefaultStringRanges.EndNumber
            : int.Parse(colDef.ColumnEndWith);

         var length = colDef.ColumnLength == ""
            ? (int)AppConst.DefaultStringRanges.Length
            : int.Parse(colDef.ColumnLength);

         CreateIncrementingStringList idt = new CreateIncrementingStringList();

         var rs = idt.GenerateIncrementalStringList(start,end, length);

         PreLoadedFieldData[colDef.ColumnPosition - 1] = new List<dynamic>(rs);
         
      }

   }

}