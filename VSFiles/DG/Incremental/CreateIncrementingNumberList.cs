using System.Collections.Generic;

namespace DG
{
   public class CreateIncrementingNumberList
    {

      //    CreateIncrementingNumberList idt = new CreateIncrementingNumberList();
      //    List<dynamic> numbersIncremental = new List<dynamic>();
      //    datesIncremental = idt.GenerateIncrementingNumberList(
      //                                                          AppConst.DefaultNumberRanges.StartNumber,
      //                                                          AppConst.DefaultNumberRanges.EndNumber
      //                                                         );


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
