using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DG
{
   class DynObject //Not used/
   {

      public static dynamic GetFrequencyOfNumbersOccurences<T>(object objectList)
      {

         var copied = ((IEnumerable)objectList).Cast<object>().ToList();

         List<T> fooList = copied.OfType<T>().ToList();

         var dictionary = fooList.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
         
         return dictionary;
      }

   }




}
