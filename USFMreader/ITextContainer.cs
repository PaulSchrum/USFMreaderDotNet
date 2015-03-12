using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USFMreader
{
   public interface ITextContainer
   {
      void AppendContents(ITextContainer TextContainer);
      Dictionary<int, ITextContainer> GetContents();
   }
}
