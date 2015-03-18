using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("ReaderTests")]
namespace USFMreader.GeneralizedFileSystem
{
   public class WebPoller
   {
      public String baseURL { get; private set; }
      public String testURL { get; private set; }
      public RootDirectoryItem root { get; set; }
      protected Dictionary<int, List<String>> ignoreDirectoryNames { get; set; }
      internal bool isForTesting { get; set; }
      

      public WebPoller()
      {
         baseURL = "https://api.unfoldingword.org/";
         testURL = "https://api.unfoldingword.org/avd/txt/1/avd-ar/";
         root = RootDirectoryItem.CreateInstance(baseURL);
         setupIgnoreDirectoryNames();
      }

      private void setupIgnoreDirectoryNames()
      {
         ignoreDirectoryNames = new Dictionary<int, List<string>>();
         List<String> level0Ignores = 
            new List<string> { "bible", "obs", "t4t", "ts", "td", "tk", "tx" };
         ignoreDirectoryNames.Add(0, level0Ignores);
      }

      internal WebPoller(bool isForTestingOnly) : this()
      {
         this.isForTesting = isForTestingOnly;
      }

      public void BuildDirectory(DirectoryItem directory, int depthLevel)
      {
         // Code copied and adapted from 
         // http://stackoverflow.com/questions/124492/c-sharp-httpwebrequest-command-to-get-directory-listing
         // answer by smink.
         HttpWebRequest request = (HttpWebRequest)WebRequest.Create(directory.GetPathAndFileName());
         using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
         {
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
               List<String> lines = parseToLines(reader);
               foreach (var line in lines)
               {
                  var item = FileSystemItem.CreateFromUnfoldingWorldServerString(line);
                  item.Parent = directory;
                  directory.AddChild(item);
               }
            }
         }

         var directoriesToCrawl = directory
            .GetChildren()
            .Where(item => item is DirectoryItem)
            .Where(item => !(item.Name.Equals("..")))
            ;

         if(depthLevel == 0)
         {
            directoriesToCrawl = directoriesToCrawl
               .Where(dir => false == shouldSkip(dir.Name, 0));
         }

         foreach(var dir in directoriesToCrawl)
         {
            this.BuildDirectory(dir as DirectoryItem, depthLevel + 1);
         }

      }

      private bool shouldSkip(String name, int depthLevel)
      {
         if (!this.ignoreDirectoryNames.ContainsKey(depthLevel))
            return false;

         foreach(var testName in this.ignoreDirectoryNames[depthLevel])
         {
            if(name.Equals(testName + @"/"))
               return true;
         }
         return false;
      }

      public void CrawlUnfoldingWordForUpdates()
      {
         BuildDirectory(this.root, 0);
      }

      private List<string> parseToLines(StreamReader reader)
      {
         return
            reader.ReadToEnd().Split('\n')
            .Where(line => line.StartsWith("<a href="))
            .Select(line => line.Trim())
            .ToList()
            ;
      }
   }
}
