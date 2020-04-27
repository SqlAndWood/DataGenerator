using System;
using System.Collections.Generic;
using System.Text;

namespace DG
{
   
   public class CreateIncrementingStringList
    {

      public Random Random ; 
 
      public List<dynamic> GenerateIncrementalStringList(int length)
        {
            Random = new Random();

            List <dynamic> si = new List<dynamic>();

            for (int i = 0; i < length; i++)
            {
               si.Add(CreateRandomString(length));
            }

            return si;
        }

        private string CreateRandomString(int length)
        {

           {
              //TODO: This min max to be determined by appConst.
              int randomLength = (int)GeneralMethods.RandomNumberBetween(0, length);

              StringBuilder strBuild = new StringBuilder();

              for (int i = 0; i < randomLength; i++)
              {
                 double flt = Random.NextDouble();
                 int shift = Convert.ToInt32(Math.Floor(25 * flt));

                 //Had an idea to randomly insert upper or lower case
                  int rand = (int)GeneralMethods.RandomNumberBetween(0, 6);
                  var letter = rand == 0 ? Convert.ToChar(" ") : Convert.ToChar(shift + 65);

                  letter = rand >= 2 ? char.ToLower(letter) : char.ToUpper(letter);

               strBuild.Append(letter);

              }

              return strBuild.ToString();
           }
      }

    }
}
