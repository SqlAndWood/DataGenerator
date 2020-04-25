using System;

namespace DG
{
   static class Mathematics
   {

      //This is a safe function, to divide by zero 
      public static double SfDivideByZero(int Renumerator, int denominator)
      {
         double preventDivisionError = (double)denominator;
         if (Math.Abs((double)denominator) < 0.001)
         {
            preventDivisionError = 1;
         }
         return ((double)Renumerator / preventDivisionError) * 100;

      }


   }
}
