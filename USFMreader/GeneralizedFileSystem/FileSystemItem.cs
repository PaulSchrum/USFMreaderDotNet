using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USFMreader.GeneralizedFileSystem
{
   public abstract class FileSystemItem
   {
      public String Name { get; set; }
      public DateTime TimeStamp { get; set; }
      public String FileSize { get; set; }
      public DirectoryItem Parent { get; protected set; }

      public abstract List<FileSystemItem> GetChildren();

      public abstract void AddChild(FileSystemItem fileSystemItem);

      public virtual String GetPathAndFileName(StringBuilder pathFName)
      {
         StringBuilder headStringBuilder = null;

         if(null == pathFName)
            headStringBuilder = new StringBuilder(this.Name);
         else
         {
            headStringBuilder = pathFName;
            headStringBuilder.Insert(0, @"\");
            headStringBuilder.Insert(0, this.Name);
         }

         return this.Parent.GetPathAndFileName(headStringBuilder);
      }
   }
}
