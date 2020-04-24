namespace DG
{
   class StringConversions
   {

      public static string inString(string strSource, string findCharacters)
      {

         strSource = strSource.ToLower();
         findCharacters = findCharacters.ToLower();

         if (strSource.Contains(findCharacters))
         {
            int Start = strSource.IndexOf(findCharacters, 0) + findCharacters.Length;

            return strSource.Substring(Start, strSource.Length - Start);
         }
         else
         {
            return "";
         }
      }

      public static string getBetween(string strSource, string strStart, string strEnd)
      {

         strSource = strSource.ToLower();
         strStart = strStart.ToLower();

         int Start, End;
         if (strSource.Contains(strStart) && strSource.Contains(strEnd))
         {
            Start = strSource.IndexOf(strStart, 0) + strStart.Length;
            End = strSource.IndexOf(strEnd, Start);
            return strSource.Substring(Start, End - Start);
         }
         else
         {
            return "";
         }
      }

   }
}
