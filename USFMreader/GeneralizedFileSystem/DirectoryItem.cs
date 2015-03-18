using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USFMreader.GeneralizedFileSystem
{
   public class DirectoryItem : FileSystemItem
   {
      protected List<FileSystemItem> children { get; set; }
      public DirectoryItem() : base()
      {
         children = new List<FileSystemItem>();
      }

      public DirectoryItem(String name, DateTime timeStamp)
      {
         Name = name;
         TimeStamp = timeStamp;
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
         return this.Name;
      }
   }
}
