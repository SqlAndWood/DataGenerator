using System.Collections.Generic;
using System.Linq;

namespace DG
{
   public class CreateIncrementingDecimalList
    {


      public List<dynamic> GenerateIncrementingDecimalList(int start, int end, int decimalPlace)
      {
         //a basic test. needs improvement.
         if (end - start > AppConst.DefaultDecimalRanges.EndNumber - AppConst.DefaultDecimalRanges.StartNumber)
         {
            //Create a formula on this one. Pretty sure its x^y (x=End-Start; y = decimalPlaces) <= 500000
            // OR it's logarithmic in nature. not a high priority.
            if (decimalPlace >= (int)AppConst.DefaultDecimalRanges.DecimalPlaces)
            {
               decimalPlace = (int)AppConst.DefaultDecimalRanges.DecimalPlaces;
            }
         }

         List <dynamic> numbersIncremental = new List<dynamic>();

         var t = "0." + string.Concat(Enumerable.Repeat("0", decimalPlace - 1)) + "1";
         decimal increment = decimal.Parse(t);


         for (decimal i = start; i < end; i += increment)
         {
            numbersIncremental.Add(i);
         }

         return numbersIncremental;
      }
        
    }
}
