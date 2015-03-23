using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if Win8_Debug || Win8_Release
using System.IO;
#endif

namespace USFMreader.GeneralizedFileSystem
{
   public class RootDirectoryItem : DirectoryItem
   {
      public String path { get; protected set; }

#if Win8_Debug || Win8_Release
      internal static bool TestEstablishedAppDirectory=true;
#endif

      public static RootDirectoryItem CreateInstanceAppDataStorage()
      {
         RootDirectoryItem returnValue;
         #if Win8_Debug || Win8_Release
            returnValue = getRootDirectory_Win8();
         #else
            returnValue = null;
         #endif
            return returnValue;
      }

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

#if Win8_Debug || Win8_Release
      private static RootDirectoryItem getRootDirectory_Win8()
      {
         var parent3 = Directory.GetParent(System.IO.Directory.GetCurrentDirectory());
         var parent2 = Directory.GetParent(parent3.FullName);
         var dir = Directory.GetParent(parent2.FullName).FullName;

         if (true == TestEstablishedAppDirectory)
            dir = dir + @"\EstablishedAppDirectory";
         else
            dir = dir + @"\NewAppDirectory";

         RootDirectoryItem returnValue = CreateInstance(dir);
         return returnValue;
      }
#endif

   }
}
