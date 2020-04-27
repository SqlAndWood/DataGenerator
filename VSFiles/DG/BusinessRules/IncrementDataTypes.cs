using System.Collections.Generic;
using System.Linq;

namespace DG
{
    public class IncrementDataTypes
    {

        public void TestCreationOfIncrementalCharacters()
        {

            //Test only to create a Incremental CHARACTER data type A-Z, AA-ZZ, AAA-ZZZ etc.
            IncrementDataTypes idt = new IncrementDataTypes();

            List<dynamic> CharactersIncremental = new List<dynamic>();
            CharactersIncremental = idt.GenerateIncrementalCharacterList(500000);

        }


        public List<dynamic> GenerateIncrementalCharacterList(int ArrayLength)
        {

            List <dynamic> CharactersIncremental = new List<dynamic>();

            foreach (var s in generate().Take(ArrayLength))
            {
                CharactersIncremental.Add(s);
            }

            return CharactersIncremental;
        }

        //Called by GenerateIncrementalCharacterList
        IEnumerable<string> generate()
        {
            long n = 0;
            while (true) yield return toBase26(++n);
        }

        //Called by IEnumerable<string> generate()
        string toBase26(long i)
        {
            if (i == 0) return ""; i--;
            return toBase26(i / 26) + (char)('A' + i % 26);
        }




    }
}
