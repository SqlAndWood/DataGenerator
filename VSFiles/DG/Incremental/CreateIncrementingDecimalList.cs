﻿using System.Collections.Generic;
using System.Linq;

namespace DG
{
   public class CreateIncrementingDecimalList
    {

       //CreateIncrementingDecimalList idt = new CreateIncrementingDecimalList();
       //List<dynamic> numbersIncremental = new List<dynamic>();
       //numbersIncremental = idt.GenerateIncrementingDecimalList(
       //(int) AppConst.DefaultNumberRanges.StartNumber,
       //(int) AppConst.DefaultNumberRanges.EndNumber,
       //1
       //);
       
      public List<dynamic> GenerateIncrementingDecimalList(int start, int end, int decimalPlace)
      {

         if (end - start > AppConst.DefaultDecimalRanges.EndNumber - AppConst.DefaultDecimalRanges.StartNumber)
         {
            //Create a formula on this one. Pretty sure its x^y (x=End-Start; y = decimalPlaces) <= 500000
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
