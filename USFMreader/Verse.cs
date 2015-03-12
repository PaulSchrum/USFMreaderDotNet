using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USFMreader
{
   public class Verse : ITextContainer
   {
      public Verse()
      {
         allText = new Dictionary<int, ITextContainer>();
      }

      public Verse(String verseNo, String textString)
         : this()
      {
         VerseNumber = new VerseNumberRange(verseNo);
         allText.Add(VerseNumber.BeginNumber, new RichTextFragment(textString));
         fragmentCount = 1;
      }

      public Verse(string unparsedVerse) : this()
      {

      }

      protected Dictionary<int, ITextContainer> allText { get; set; }
      public VerseNumberRange VerseNumber { get; internal set; }
      protected int fragmentCount { get; set; }

      public void AppendContents(ITextContainer TextContainer)
      {
         if (!(TextContainer is RichTextFragment))
            throw new ArgumentException();

         RichTextFragment FragmentToAdd = TextContainer as RichTextFragment;
         fragmentCount++;
         allText.Add(fragmentCount, FragmentToAdd);
      }

      public Dictionary<int, ITextContainer> GetContents()
      {
         return this.allText;
      }

      internal void AppendContents(string line)
      {
         RichTextFragment FragmentToAdd = new RichTextFragment(line);
         fragmentCount++;
         while (true == allText.ContainsKey(fragmentCount))
            fragmentCount++;

         allText.Add(fragmentCount, FragmentToAdd);
      }

      public override string ToString()
      {
         StringBuilder sb = new StringBuilder(this.allText.Count);
         foreach(var part in this.allText.OrderBy(item => item.Key))
         {
            sb.Append(part.Value.ToString());
         }
         return sb.ToString();
      }
   }
}
