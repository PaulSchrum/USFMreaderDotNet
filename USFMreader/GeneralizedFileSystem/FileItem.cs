using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USFMreader.GeneralizedFileSystem
{
   class FileItem : FileSystemItem
   {
      public FileItem(String name, DateTime timeStamp, String filesize) : base()
      {
         this.Name = name;
         this.TimeStamp = timeStamp;
         this.FileSize = filesize;
      }

      public override List<FileSystemItem> GetChildren()
      {
         return new List<FileSystemItem>();
      }

      public override void AddChild(FileSystemItem fileSystemItem)
      {
         throw new Exception("Can not add child to type FileItem.");
      }
   }
}
