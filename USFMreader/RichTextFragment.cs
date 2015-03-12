using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USFMreader
{
   public class RichTextFragment : ITextContainer
   {
      public RichTextFragment() { }

      public RichTextFragment(String stringValue)
         : this()
      {
         TextValue = stringValue;
      }

      public String TextValue { get; set; }
      // todo: protected Dictionary<int, Formatting> allFormattings {get; set;}
      // todo: internal void AddFormatting { get; set; }

      public void AppendContents(ITextContainer TextContainer)
      {
         TextValue = (TextContainer as RichTextFragment).TextValue;
      }

      public Dictionary<int, ITextContainer> GetContents()
      {
         Dictionary<int, ITextContainer> retDict = new Dictionary<int, ITextContainer>(1);
         retDict.Add(0, this);
         return retDict;
      }

      public override string ToString()
      {
         return TextValue;
      }
   }
}
