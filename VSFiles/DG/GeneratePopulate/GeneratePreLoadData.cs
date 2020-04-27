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

         //These are not considered with this Class (TBD), they play a part in how data is obtained later on.
         "StartWith":"",
         "EndWith":"",
       */

      private List<dynamic>[] PreLoadedFieldData;

      public GeneratePreLoadData(ObtainDataDefinitions dd,  List<dynamic>[] preLoadedFieldData)
      {
         //Step one is to create the required [] count and populate with dummy data.
         PreLoadedFieldData = preLoadedFieldData;
         
         foreach (var colDef in dd.TableDefinition.ColumnDefinitions)
         {

            var ColLoadDataMimicMethod = colDef.ColumnLoadDataMimicMethod.ToUpper();
            var colDataType = colDef.ColumnDataType.ToUpper();

            switch (ColLoadDataMimicMethod)
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
                     case "INT":
                        LoadIncrementalNumber(colDef);
                        break;

                     case "NUMERIC": 
                     case "DECIMAL":
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

         PreLoadedFieldData[colDef.ColumnPosition - 1] = new List<dynamic>
         {
            File.ReadLines(file).ToList()
         };

      }

      // INCREMENTAL => Data Type { Date, Time, Number, Decimal (up to 1 decimal place only)}
      private void LoadIncrementalDate(ColumnDefinition colDef)
      {
    
         CreateIncrementingDateList idt = new CreateIncrementingDateList();

         //TODO: Replace Start/End with actual parameteres. Ie test for valid parameters first.
         var datesIncremental = idt.GenerateIncrementingDateList(
            (int)AppConst.DefaultDateRanges.StartDate,
            (int)AppConst.DefaultDateRanges.EndDate
         );

         PreLoadedFieldData[colDef.ColumnPosition - 1] = new List<dynamic>(datesIncremental);
         
      }

      private void LoadIncrementalTime(ColumnDefinition colDef)
      {

         CreateIncrementingTimeList idt = new CreateIncrementingTimeList();

         //TODO: Replace Start/End with actual parameteres. Ie test for valid parameters first.
         var ti = idt.GenerateIncrementingTimeList(
            (int)AppConst.DefaultTimeRange.StartTime,
            (int)AppConst.DefaultTimeRange.EndTime
         );

         PreLoadedFieldData[colDef.ColumnPosition - 1] = new List<dynamic>(ti);

      }

      private void LoadIncrementalNumber(ColumnDefinition colDef)
      {

         CreateIncrementingNumberList idt = new CreateIncrementingNumberList();

         //TODO: Replace Start/End with actual parameteres. Ie test for valid parameters first.
         var numbersIncremental = idt.GenerateIncrementingNumberList(
            (int) AppConst.DefaultNumberRanges.StartNumber,
            (int)AppConst.DefaultNumberRanges.EndNumber
         );

         PreLoadedFieldData[colDef.ColumnPosition - 1] = new List<dynamic>(numbersIncremental);

      }

      private void LoadIncrementalDecimal(ColumnDefinition colDef)
      {

         CreateIncrementingDecimalList idt = new CreateIncrementingDecimalList();

         //TODO: Replace Start/End with actual parameteres. Ie test for valid parameters first.
         var ld = idt.GenerateIncrementingDecimalList(
            (int)AppConst.DefaultDecimalRanges.StartNumber,
            (int)AppConst.DefaultDecimalRanges.EndNumber,
            (int)AppConst.DefaultDecimalRanges.DecimalPlaces
         );


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


         CreateIncrementingStringList idt = new CreateIncrementingStringList();
         List<dynamic> rs = new List<dynamic>();
         rs = idt.GenerateIncrementalStringList((int)AppConst.DefaultNumberRanges.EndNumber);

         PreLoadedFieldData[colDef.ColumnPosition - 1] = new List<dynamic>(rs);


      }


   }

}