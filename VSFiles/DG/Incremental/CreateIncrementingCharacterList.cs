using System.Collections.Generic;
using System.Linq;

namespace DG
{
    public class CreateIncrementingCharacterList
    {
      //    CreateIncrementingCharacterList idt = new CreateIncrementingCharacterList();
      //    List<dynamic> CharactersIncremental = new List<dynamic>();
      //    CharactersIncremental = idt.GenerateIncrementalCharacterList( AppConst.DefaultNumberRanges.EndNumber);

        public List<dynamic> GenerateIncrementalCharacterList(int end)
        {

            List <dynamic> charactersIncremental = new List<dynamic>();

            foreach (var s in Generate().Take(end))
            {
                charactersIncremental.Add(s);
            }

            return charactersIncremental;
        }

        //Called by GenerateIncrementalCharacterList
        IEnumerable<string> Generate()
        {
            long n = 0;
            while (true) yield return ToBase26(++n);
        }

        //Called by IEnumerable<string> generate()
        string ToBase26(long i)
        {
            if (i == 0) return ""; i--;
            return ToBase26(i / 26) + (char)('A' + i % 26);
        }

    }
}
