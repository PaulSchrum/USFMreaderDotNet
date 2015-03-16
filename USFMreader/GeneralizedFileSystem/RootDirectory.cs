using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USFMreader.GeneralizedFileSystem
{
   public class RootDirectory : DirectoryItem
   {
      public String path { get; protected set; }

      public static RootDirectory CreateInstance(String rootDir)
      {
         RootDirectory newRootDir = new RootDirectory();
         newRootDir.path = rootDir;
         newRootDir.Parent = null;
         newRootDir.Name = String.Empty;
         return newRootDir;
      }

      public override string GetPathAndFileName(StringBuilder pathFName)
      {
         return path;
      }
   }
}
