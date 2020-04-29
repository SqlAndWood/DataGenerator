using System;
using System.Collections.Generic;
using System.Linq;

namespace DG
{

   public class CreateIncrementingStringList
   {

      public List<dynamic> GenerateIncrementalStringList(int wordCountMin, int wordCountMax, int length)
      {
    
         List<dynamic> si = new List<dynamic>();

         for (int i = 0; i < 10000; i++)
         {
            var source = string.Join(" ", LoremIpsum.WordList(false).Take(RandomHelper.Instance.Next(wordCountMin, wordCountMax))).Replace(@",", "");

            if (source.Length >= length)
            {
               source = source.Substring(0, length);
            }

            if (source != "")
            {
               source = char.ToUpper(source[0]) + source.Substring(1);
            }

            si.Add(source);

         }

         return si;

      }

   }
}
