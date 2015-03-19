using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USFMreader.GeneralizedFileSystem
{
   public abstract class FileSystemItem : IEqualityComparer
   {
      public String Name { get; set; }
      public DateTime TimeStamp { get; set; }
      public String FileSize { get; set; }
      public DirectoryItem Parent { get; internal set; }

      public abstract List<FileSystemItem> GetChildren();

      public abstract void AddChild(FileSystemItem fileSystemItem);

      public virtual String GetPathAndFileName()
      {
         return this.Parent.GetPathAndFileName() +  this.Name;
      }

      public static FileSystemItem CreateFromUnfoldingWorldServerString(String serverString)
      {
         char[] parseTokens = { ' ', '<', '>', };
         var strings = serverString.Split(parseTokens,StringSplitOptions.RemoveEmptyEntries);
         String itemName = strings[2];
         DateTime itemTimeStamp = DateTime.Parse(strings[4] + " " + strings[5]);
         String itemSize = strings[6];

         if (itemSize.Equals("-"))
            return new DirectoryItem(itemName, itemTimeStamp);
         else
            return new FileItem(itemName, itemTimeStamp, itemSize);
      }

      public new bool Equals(Object x_, Object y_)
      {
         FileSystemItem x = x_ as FileSystemItem;
         FileSystemItem y = y_ as FileSystemItem;
         if (!(x.Name.Equals(y.Name)))
         {
            return false;
         }
         return x.GetPathAndFileName().Equals(y.GetPathAndFileName());
      }

      public int GetHashCode(Object item_)
      {
         FileSystemItem item = item_ as FileSystemItem;
         int returnValue = 0;
         int index = 0;
         byte[] fullName = Encoding.ASCII.GetBytes(item.GetPathAndFileName());
         for (index = 0; index < fullName.Length; index++)
         {
            int multiplier = index % 2 == 0 ? 1 : -1;
            returnValue += multiplier * (fullName[index]);
         }
         returnValue += fullName.Length * 3;

         return returnValue;
      }
   }
}
