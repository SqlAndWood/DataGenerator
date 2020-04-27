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
      

      public List<dynamic> GenerateIncrementingDateList(int start, int end)
        {

            int sDated = start % 100;
            int sDatem = (start / 100) % 100;
            int sDatey = start / 10000;
            var StartDate = new DateTime(sDatey, sDatem, sDated);

            int eDated = end % 100;
            int eDatem = (end / 100) % 100;
            int eDatey = end / 10000;
            var EndDate = new DateTime(eDatey, eDatem, eDated);

            List <dynamic> datesIncremental = new List<dynamic>();

            for (var dt = StartDate; dt <= EndDate; dt = dt.AddDays(1))
            {
               datesIncremental.Add(dt);
            }

            return datesIncremental;
        }
        
    }
}
