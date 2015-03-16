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
      public DirectoryItem()
      {
         children = new List<FileSystemItem>();
      }

      public override List<FileSystemItem> GetChildren()
      {
         return children;
      }

      public override void AddChild(FileSystemItem fileSystemItem)
      {
         children.Add(fileSystemItem);
      }
   }
}
