using System;
using System.Collections.Generic;
using System.Text;

namespace DG
{
   
   public class CreateIncrementingStringList
    {
      //    CreateIncrementingStringList idt = new CreateIncrementingStringList();
      //    List<dynamic> rs = new List<dynamic>();
      //    rs = idt.GenerateIncrementalStringList( );

      public Random Random ; 

      public List<dynamic> GenerateIncrementalStringList(int end)
        {
           Random = new Random();

         List <dynamic> si = new List<dynamic>();

            for (int i = 0; i < end; i++)
            {
               si.Add(createRandomString());
            }

         return si;
        }


        private string createRandomString()
        {

           {
              //TODO: This min max to be determined by appConst.
              int length = (int)GeneralMethods.RandomNumberBetween(0, 22);

              StringBuilder strBuild = new StringBuilder();
       
              for (int i = 0; i < length; i++)
              {
                 double flt = Random.NextDouble();
                 int shift = Convert.ToInt32(Math.Floor(25 * flt));

                //  int rand = (int)GeneralMethods.RandomNumberBetween(0, 1);
                 
                  char letter = Convert.ToChar(shift + 65);

               
             
               strBuild.Append(letter);
            }
              return strBuild.ToString();
           }
      }

    }
}
