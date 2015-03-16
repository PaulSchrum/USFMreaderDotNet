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
      public RootDirectory root { get; set; }
      internal bool isForTesting { get; set; }

      public WebPoller()
      {
         baseURL = "https://api.unfoldingword.org/";
         testURL = "https://api.unfoldingword.org/avd/txt/1/avd-ar/";
         root = RootDirectory.CreateInstance("https://api.unfoldingword.org/");
      }

      internal WebPoller(bool isForTestingOnly) : this()
      {
         this.isForTesting = isForTestingOnly;
      }

      public void CrawlUnfoldingWordForUpdates()
      {
         // Code copied and adapted from 
         // http://stackoverflow.com/questions/124492/c-sharp-httpwebrequest-command-to-get-directory-listing
         // answer by smink.
         HttpWebRequest request = (HttpWebRequest)WebRequest.Create(testURL);
         using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
         {
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
               List<String> lines = parseToLines(reader);
               string html = reader.ReadToEnd();
               Regex itemNameRegex = new Regex("<a href=\".*\">(?<name>.*)</a>");
               MatchCollection itemNames = itemNameRegex.Matches(html);
               foreach (Match match in itemNames)
                     Console.WriteLine(match.Groups["name"]);
               
               foreach (var line in lines)
                  Console.WriteLine(line);
            }
         }
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
