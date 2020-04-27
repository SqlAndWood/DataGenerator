using System.Collections.Generic;

namespace DG
{
   public class CreateIncrementingNumberList
    {
       
      public List<dynamic> GenerateIncrementingNumberList(int start, int end)
        {

            List <dynamic> numbersIncremental = new List<dynamic>();

            for (int i = start; i < end; i++)
            {
               numbersIncremental.Add(i);
            }

            return numbersIncremental;
        }
        
    }
}
