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
      public Dictionary<String, WorkingText> allWorkingTexts { get; protected set; }

      public Reader()
      {
         allWorkingTexts = new Dictionary<string, WorkingText>();
         AppDataDirectory = RootDirectoryItem.CreateInstanceAppDataStorage();
      }

   }
}
