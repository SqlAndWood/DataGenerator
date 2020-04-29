namespace DG
{
   static class Quoted
   {

      /*
       *text = text.SurroundWithDoubleQuotes(); 
       */
      public static string SurroundWithDoubleQuotes(this string text)
      {
         return SurroundWith(text, "\"");
         //  return  string.Format("\"{0}\"", text);
      }
      /*
       *text = text.SurroundWith("'"); 
       */
      public static string SurroundWith(this string text, string ends)
      {
         return ends + text + ends;
        
      }




   }
}
