using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USFMreader
{
   public class Reader
   {
      public Dictionary<String, WorkingText> allWorkingTexts { get; protected set; }

      public Reader()
      {
         allWorkingTexts = new Dictionary<string, WorkingText>();
      }

   }
}
