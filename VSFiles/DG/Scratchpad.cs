using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace DG
{
   static class Scratchpad
   {

      private static bool ContinueApp = false;
      
      public static bool  Testing()
      {


         string d = ",";

         int i = -123;

         string s = "The Quick Dog";
         string qs = s.SurroundWithDoubleQuotes();

         double dd = 34.44;

         bool boo = true;

         char c = 'c';

         TimeSpan ts = new TimeSpan(5, 0, 0);

         var dt = new DateTime(1956, 5, 15);


         List<dynamic> la = new List<dynamic> { i, qs, dd, boo, c, ts, dt };

         dynamic holingCell = "";

         foreach (var ob in la)
         {
            holingCell += ob + d;

         }

         //Remove the final 'd'


         string phc = holingCell.ToString();

         int strLength = phc.Length;


         phc = phc.Substring(0, strLength - 1);

         //Unsure if I need to do this prior to saving to file.
         phc  = phc.SurroundWithDoubleQuotes();

         return ContinueApp;

      }

   }
}
