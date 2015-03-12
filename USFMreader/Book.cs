using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USFMreader
{
   public class Book
   {
      public String FileName { get; internal set; }   //   name as stored on file system
      public String Id { get; internal set; }         //    \id  is the USFM tag
      public String Encoding { get; internal set; }   //    \ide
      public String Header { get; internal set; }     //    \h
      public String TOC1 { get; internal set; }       //    \toc1
      public String TOC2 { get; internal set; }       //    \toc2
      public String TOC3 { get; internal set; }       //    \toc3
      public String MajorTitle { get; internal set; } //    \mt1
      public Dictionary<int, ITextContainer> AllChapters { get; protected set; }
      internal WorkingText parent { get; set; }
      public String PathAndFileName { get; private set; }

      private Chapter pendingChapter { get; set; }
      private int chapterCount { get; set; }

      public Book()
      {
         AllChapters = new Dictionary<int, ITextContainer>();
         chapterCount = 0;
      }

      public Book(WorkingText parent_) : this()
      {
         parent = parent_;
      }

      public Book(WorkingText parent_, String fileName)
         : this(parent_)
      {
         FileName = fileName;
      }

      public Chapter GetChapterByNumber(int number)
      {
         return this
            .AllChapters
            .Where(ch => ch.Key == number-1)
            .FirstOrDefault().Value as Chapter;
      }

      private enum LoadingState
      {
         FrontMatter = 0,
         Chapter = 1,
         Verse = 2
      }
      LoadingState loadState { get; set; }

      public void LoadBook()
      {
         if (!File.Exists(
            parent.ContentDirectory + "\\" + this.FileName))
            return;

         PathAndFileName = parent.ContentDirectory + "\\" + this.FileName;
         loadState = LoadingState.FrontMatter;
         using (StreamReader file = File.OpenText(PathAndFileName))
         {
            String line;
            while ((line = file.ReadLine()) != null)
            {
               if(loadState == LoadingState.FrontMatter)
               {
                  if(String.IsNullOrEmpty(line) ||
                     !line.Substring(0,1).Trim().Equals(@"\"))
                  {
                     continue;
                  }
                  ReadLineAtFrontMatterLevel(line);
               }
               else if(loadState == LoadingState.Chapter)
               {
                  if (String.IsNullOrEmpty(line) ||
                     !line.Substring(0, 1).Trim().Equals(@"\"))
                  {
                     continue;
                  }
                  ReadLineAtChapterLevel(line);
               }
               else if(loadState == LoadingState.Verse)
               {
                  if (String.IsNullOrEmpty(line))
                  {
                     continue;
                  }
                  ReadLineAtVerseLevel(line);
               }
               else
               {
                  throw new Exception("Unexpected loadState value.");
               }
            }
            this.AllChapters.Add(this.chapterCount, this.pendingChapter);
         }
      }

      private void ReadLineAtVerseLevel(string line)
      {
         if(!line.Substring(0,1).Trim().Equals(@"\"))
         {
            this.pendingChapter.AppendToLastVerse(line);
         }
         else
         {
            String[] strings = line.Split(' ');
            if (strings.Length < 2) return;
            String ValueInLine;
            switch (strings[0].ToLower())
            {
               case "\\c":
               {
                  ValueInLine = line.Substring(strings[0].Length).Trim();
                  this.processChapterLine(ValueInLine);
                  break;
               }
               case "\\v":
               {
                  ValueInLine = line.Substring(
                     strings[0].Length + strings[1].Length + 2)
                     .Trim();
                  this.processVerseFirstLine(strings[1], ValueInLine);
                  break;
               }
               default:
               {
                  break;
               }
            }
         }
      }

      private void ReadLineAtChapterLevel(string line)
      {
         String[] strings = line.Split(' ');
         if (strings.Length < 2) return;
         String ValueInLine;
         switch (strings[0].ToLower())
         {
            case "\\c":
            {
               ValueInLine = line.Substring(strings[0].Length).Trim();
               this.processChapterLine(ValueInLine);
               break;
            }
            case "\\v":
            {
               ValueInLine = line.Substring(
                  strings[0].Length + 1 + strings[1].Length)
                  .Trim();
               this.processVerseFirstLine(strings[1], ValueInLine);
               break;
            }
            default:
            {
               break;
            }
         }
      }

      private void processVerseFirstLine(String verseNum, String contentPortionLine)
      {
         pendingChapter.AddNewVerse(verseNum, contentPortionLine);
         loadState = LoadingState.Verse;
      }

      private void processChapterLine(String chapterHeaderText)
      {
         if (this.pendingChapter != null)
         {
            this.AllChapters.Add(this.chapterCount, this.pendingChapter);
            this.chapterCount++;
         }
         this.pendingChapter = new Chapter(chapterCount, chapterHeaderText);
         loadState = LoadingState.Chapter;
      }

      private void ReadLineAtFrontMatterLevel(String line)
      {
         String[] strings = line.Split(' ');
         if (strings.Length < 2) return;
         String ValueInLine = line.Substring(strings[0].Length).Trim();
         switch (strings[0].ToLower())
         {
            case "\\c":
               {
                  this.processChapterLine(ValueInLine);
                  break;
               }
            case "\\id":
               {
                  Id = ValueInLine;
                  break;
               }
            case "\\ide":
            {
               Encoding = ValueInLine;
               break;
            }
            case "\\h":
            {
               Header = ValueInLine;
               break;
            }
            case "\\toc1":
            {
               TOC1 = ValueInLine;
               break;
            }
            case "\\toc2":
            {
               TOC2 = ValueInLine;
               break;
            }
            case "\\toc3":
            {
               TOC3 = ValueInLine;
               break;
            }
            case "\\mt":
            {
               MajorTitle = ValueInLine;
               break;
            }
            default:
            {
               break;
            }
         }
      }
   }
}
