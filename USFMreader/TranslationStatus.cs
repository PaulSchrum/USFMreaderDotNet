using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USFMreader
{
   public class TranslationStatus
   {
      public String CheckingEntity { get; internal set; }
      public int CheckingLevel { get; internal set; }
      public String Comments { get; internal set; }
      public String Contributors { get; internal set; }
      public DateTime PublishDate { get; internal set; }
      public String SourceText { get; internal set; }
      public String SourceTextVersion { get; internal set; }
      public String Version { get; internal set; }
   }
}
