using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DG
{

   class GeneratePreLoadData
   {

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

      private List<dynamic>[] PreLoadedFieldData;

      public GeneratePreLoadData(ObtainDataDefinitions dd,  List<dynamic>[] preLoadedFieldData)
      {
         //Step one is to create the required [] count and populate with dummy data.
         PreLoadedFieldData = preLoadedFieldData;
         


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

                     case "DATE":
                        LoadIncrementalDate(colDef);
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

      private void LoadFromFile(ColumnDefinition colDef)
      {

         var file = colDef.ColumnDataMimicPathFileName;

         //Note that no 'filtering' is put into place on loading from a file. 
         var fileList = File.ReadLines(file).ToList();

         CreateFileList idt = new CreateFileList();

         var fIncremental = idt.GenerateFileList(fileList);

         PreLoadedFieldData[colDef.ColumnPosition - 1] = new List<dynamic>(fIncremental);

      }


      // INCREMENTAL => Data Type { Date, Time, Number, Decimal (up to 1 decimal place only)}
      private void LoadIncrementalDate(ColumnDefinition colDef)
      {
         
         var startDate = colDef.ColumnStartWith == ""
            ? (int) AppConst.DefaultDateRanges.StartDate
            : int.Parse(colDef.ColumnStartWith);

         var endDate = colDef.ColumnEndWith == ""
            ? (int) AppConst.DefaultDateRanges.EndDate
            : int.Parse(colDef.ColumnEndWith);

         CreateIncrementingDateList idt = new CreateIncrementingDateList();

         var datesIncremental = idt.GenerateIncrementingDateList(startDate, endDate);

         PreLoadedFieldData[colDef.ColumnPosition - 1] = new List<dynamic>(datesIncremental);
         
      }

      private void LoadIncrementalTime(ColumnDefinition colDef)
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

      private void LoadIncrementalNumber(ColumnDefinition colDef)
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

      private void LoadIncrementalDecimal(ColumnDefinition colDef)
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

      private void LoadIncrementalBoolean(ColumnDefinition colDef)
      {

         CreateIncrementingBooleanList idt = new CreateIncrementingBooleanList();

         List<dynamic> dl = idt.GenerateIncrementalBooleanList();

         PreLoadedFieldData[colDef.ColumnPosition - 1] = new List<dynamic>(dl);

      }

      private void LoadIncrementalString(ColumnDefinition colDef)
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