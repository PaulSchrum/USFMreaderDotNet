using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USFMreader.GeneralizedFileSystem
{
   public class RootDirectoryItem : DirectoryItem
   {
      public String path { get; protected set; }

      public static RootDirectoryItem CreateInstance(String rootDir)
      {
         RootDirectoryItem newRootDir = new RootDirectoryItem();
         newRootDir.path = rootDir;
         newRootDir.Parent = null;
         newRootDir.Name = String.Empty;
         return newRootDir;
      }

      public override string GetPathAndFileName()
      {
         return path;
      }

   }
}
