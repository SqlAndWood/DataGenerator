using System;
using System.Threading;

namespace DG
{
    //When you just want a random number.
    //var d = RandomHelper.Instance.Next();

    //Obtain a random number between two numbers.
    //var n = RandomHelper.Instance.Next(0, 100);

    internal static class RandomHelper
      {
         private static int _seedCounter = new Random().Next();

         [ThreadStatic]
         private static Random _randomNumber;

         internal static Random Instance
         {
            get
            {
               if (_randomNumber == null)
               {
                  int seed = Interlocked.Increment(ref _seedCounter);
                  _randomNumber = new Random(seed);
               }
               return _randomNumber;
            }
         }

      }
}
