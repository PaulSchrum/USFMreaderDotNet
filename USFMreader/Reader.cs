using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USFMreader.GeneralizedFileSystem;

namespace USFMreader
{
   public class Reader
   {
      public RootDirectoryItem AppDataDirectory { get; protected set; }

      public Reader()
      {
         AppDataDirectory = RootDirectoryItem.CreateInstanceAppDataStorage();
      }

   }
}
