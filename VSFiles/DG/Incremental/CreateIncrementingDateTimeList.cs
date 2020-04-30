using System;
using System.Collections.Generic;

namespace DG
{
   public class CreateIncrementingDateTimeList
   {
      //Potential to create an overload method to accepts Dates instead of int
      public List<dynamic> GenerateIncrementingDateTimeList(int start, int end)
        {
           
         var startDate = new DateTime(start / 10000 , (start / 100) % 100, start % 100); 
         var endDate = new DateTime(end / 10000, (end / 100) % 100,    end % 100);

         List<dynamic> datesIncremental = new List<dynamic>();

         for (var dt = startDate; dt <= endDate; dt = dt.AddDays(1))
         {
            datesIncremental.Add(dt);
         }

         return datesIncremental;

        }
        
    }
}
