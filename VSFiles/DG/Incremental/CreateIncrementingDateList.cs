using System;
using System.Collections.Generic;

namespace DG
{
   public class CreateIncrementingDateList
   {
      //Potential to create an overload method to accepts Dates instead of int
      public List<dynamic> GenerateIncrementingDateList(int start, int end)
        {

 
      
            var startDate = new DateTime(start % 100, (start / 100) % 100, start / 10000);

            var endDate = new DateTime(end % 100, (end / 100) % 100, end / 10000);

            List <dynamic> datesIncremental = new List<dynamic>();

            for (var dt = startDate; dt <= endDate; dt = dt.AddDays(1))
            {
               datesIncremental.Add(dt);
            }

            return datesIncremental;
        }
        
    }
}
