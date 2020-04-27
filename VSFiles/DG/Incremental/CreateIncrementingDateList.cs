using System;
using System.Collections.Generic;

namespace DG
{
   public class CreateIncrementingDateList
   {

      //    CreateIncrementingDateList idt = new CreateIncrementingDateList();
      //    List<dynamic> datesIncremental = new List<dynamic>();
      //    datesIncremental = idt.GenerateIncrementingDateList(
      //                                                          AppConst.DefaultDateRanges.StartDate,
      //                                                          AppConst.DefaultDateRanges.EndDate
      //                                                         );
      
      public List<dynamic> GenerateIncrementingDateList(DateTime start, DateTime end)
        {

            List <dynamic> datesIncremental = new List<dynamic>();

            for (var dt = start; dt <= end; dt = dt.AddDays(1))
            {
               datesIncremental.Add(dt);
            }

            return datesIncremental;
        }
        
    }
}
