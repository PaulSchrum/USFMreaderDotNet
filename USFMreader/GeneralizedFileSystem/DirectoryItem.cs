using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace USFMreader.GeneralizedFileSystem
{
   public class DirectoryItem : FileSystemItem
   {
      public bool IsBible { get; internal set; }
      internal List<FileSystemItem> children { get; set; }
      public DirectoryItem() : base()
      {
         children = new List<FileSystemItem>();
      }

      public DirectoryItem(String name, DateTime timeStamp) : this()
      {
         Name = name;
         TimeStamp = timeStamp;
      }

      public IEnumerable<DirectoryItem> GetChildDirectories()
      {
         return this.children
            .Where(child => child is DirectoryItem)
            .Cast<DirectoryItem>()
            ;
      }

      internal IEnumerable<FileItem> GetChildFiles()
      {
         return this.children
            .Where(child => child is FileItem)
            .Cast<FileItem>();
      }

      internal void determineWhetherThisIsABible()
      {
         this.IsBible = false;
         var statusFile = this.GetChildFiles()
            .Where(file => file.Name.Equals("status.json"))
            .FirstOrDefault();
         if (null == statusFile)
            return;

         JObject status_json = null;
         HttpWebRequest request = 
            (HttpWebRequest)WebRequest.Create(statusFile.GetPathAndFileName());
         using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
         {
            using (StreamReader file = new StreamReader(response.GetResponseStream()))
            {
               using (JsonTextReader reader = new JsonTextReader(file))
               {
                  status_json = (JObject)JToken.ReadFrom(reader);
               }
            }
         }
         if (null == status_json) return;

         Object BooksPublished = status_json["books_published"];
         if (null != BooksPublished)
            this.IsBible = true;

      }

      public override List<FileSystemItem> GetChildren()
      {
         return children;
      }

      public override void AddChild(FileSystemItem fileSystemItem)
      {
         children.Add(fileSystemItem);
      }

      public override string ToString()
      {
         return "Dir: " + this.Name;
      }
   }
}
