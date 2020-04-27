using System.Collections.Generic;

namespace DG
{
   public class CreateIncrementingBooleanList
    {
      //    CreateIncrementingBooleanList idt = new CreateIncrementingBooleanList();
      //    List<dynamic> dl = new List<dynamic>();
      //    dl = idt.GenerateIncrementalBooleanList();

      public List<dynamic> GenerateIncrementalBooleanList()
        {

            List <dynamic> BooleanIncremental = new List<dynamic>();

            BooleanIncremental.Add(true);
            BooleanIncremental.Add(false);

            return BooleanIncremental;
         
      }
      
    }
}
