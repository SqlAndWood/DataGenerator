using System.Collections.Generic;

namespace DG
{
   public class CreateIncrementingBooleanList
    {

      public List<dynamic> GenerateIncrementalBooleanList()
        {

            List <dynamic> BooleanIncremental = new List<dynamic>();

            BooleanIncremental.Add(true);
            BooleanIncremental.Add(false);

            return BooleanIncremental;
         
      }
      
    }
}
