using System;

namespace DG
{
   public static class GeneralMethods
   {

      private static readonly Random Random = new Random();

      public static double RandomNumberBetween(double minValue, double maxValue)
      {
         
         var next = Random.NextDouble();

         return minValue + (next * (maxValue - minValue));
      }

      //This is a safe function, to divide by zero 
      //public static double SfDivideByZero(int numerator , int denominator)
      //{
      //   double preventDivisionError = (double)denominator;
      //   if (Math.Abs((double)denominator) < 0.001)
      //   {
      //      preventDivisionError = 1;
      //   }
      //   return ((double)numerator  / preventDivisionError) * 100;

      //}

   }
}
