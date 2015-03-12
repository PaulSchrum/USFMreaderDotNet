using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USFMreader
{
   public class VerseNumberRange
   {
      public String stringValue { get; set; }
      public int BeginNumber { get; private set; }
      public int EndNumber { get; private set; }
      public int TotalVerses { get { return EndNumber - BeginNumber + 1; } }

      public VerseNumberRange(String str)
      {
         stringValue = str;
         if(stringValue.Contains("-"))
         {
            var strings = stringValue.Split('-');
            BeginNumber = Convert.ToInt32(strings[0].Trim());
            EndNumber = Convert.ToInt32(strings[1].Trim());
         }
         else
         {
            BeginNumber = EndNumber = Convert.ToInt32(stringValue);
         }
      }

      public override string ToString()
      {
         return stringValue;
      }
   }
}
