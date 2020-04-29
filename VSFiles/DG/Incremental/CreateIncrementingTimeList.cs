using System;
using System.Collections.Generic;

namespace DG
{
   public class CreateIncrementingTimeList
   {
      
      public List<dynamic> GenerateIncrementingTimeList(int start, int end)
      {
         TimeSpan startTime = new TimeSpan(0, 0, start);
         TimeSpan stopTime = new TimeSpan(0, 0, end);

         TimeSpan trackTime = startTime;
         TimeSpan oneSecond = TimeSpan.FromSeconds(1);

         List<dynamic> timeIncremental = new List<dynamic>();

         for (TimeSpan dt = startTime; trackTime < stopTime; dt = TimeSpan.FromSeconds(1) )
         {
            timeIncremental.Add(trackTime);
            trackTime = trackTime.Add(oneSecond);
         }

         return timeIncremental;
      }
        
    }
}
