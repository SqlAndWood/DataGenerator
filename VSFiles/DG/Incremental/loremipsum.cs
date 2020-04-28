using System.Collections.Generic;
using System.Linq;


namespace DG
{
   /*
    *       public static string Words(int wordCountMin, int wordCountMax, bool uppercaseFirstLetter = true, bool includePunctuation = false)
      {
        var s =  Words(1, 20, true, false);
      
      
      var source = string.Join(" ", loremipsum.WordList(includePunctuation).Take(RandomHelper.Instance.Next(wordCountMin, wordCountMax)));
      
      //if (uppercaseFirstLetter)
      //{
      //   source = source.UppercaseFirst();
      //}
      
      return source;
      }
    */
   public static class Loremipsum
   {
      public static string Text { get; private set; } = @"lorem, ipsum, amet, pellentesque, mattis, accumsan, maximus, etiam, mollis, ligula, non, iaculis, ornare, mauris, efficitur, ex, eu, rhoncus, aliquam, in, hac, habitasse, platea, dictumst, maecenas, ultrices, purus, at, venenatis, auctor, sem, nulla, urna, molestie, nisi, mi, a, ut, euismod, nibh, id, libero, lacinia, sit, amet, lacinia, lectus, viverra, donec, scelerisque, dictum, enim, dignissim, dolor, cursus, morbi, elementum, magna, sed, sed, velit, consectetur, adipiscing, elit, curabitur, nulla, eleifend, vel, tempor, metus, phasellus, vel, pulvinar, lobortis, quis, nullam, felis, orci, congue, vitae, augue, nisi, tincidunt, id, posuere, fermentum, facilisis, ultricies, mi, nisl, fusce, neque, vulputate, integer, tortor, tempus, praesent, proin, quis, nunc, massa, congue, quam, auctor, eros, placerat, eros, leo, nec, sapien, egestas, duis, feugiat, vestibulum, porttitor, odio, sollicitudin, arcu, et, aenean, sagittis, ante, urna, fringilla, risus, et, vivamus, semper, nibh, eget, finibus, est, laoreet, justo, commodo, sagittis, vitae, nunc, diam, ac, tellus, posuere, condimentum, enim, tellus, faucibus, suscipit, ac, nec, turpis, interdum, malesuada, fames, primis, quisque, pretium, ex, feugiat, porttitor, massa, vehicula, dapibus, blandit, hendrerit, elit, aliquet, nam, orci, fringilla, blandit, ullamcorper, mauris, ultrices, consequat, tempor, convallis, gravida, sodales, volutpat, finibus, neque, pulvinar, varius, porta, laoreet, eu, porta, placerat, lacus, pharetra, erat, bibendum, leo, tristique, cras, rutrum, at, dui, tortor, in, varius, arcu, interdum, vestibulum, magna, ante, imperdiet, erat, luctus, odio, dui, volutpat, bibendum, quam, euismod, class, aptent, taciti, sociosqu, ad, litora, torquent, per, conubia, nostra, inceptos, himenaeos, suspendisse, a, sem, eleifend, commodo, dolor, luctus, lectus,";

      internal static IEnumerable<string> Rearrange(string words)
      {
         return words.Split(' ').OrderBy(x => RandomHelper.Instance.Next());
      }

      internal static IEnumerable<string> WordList(bool includePuncation)
      {
         return includePuncation ? Rearrange(Text) : Rearrange(Text.Remove(','));
      }

      public static void Update(string text)
      {
         Text = text;
      }
   }

   
}
