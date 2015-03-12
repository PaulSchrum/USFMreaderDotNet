using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USFMreader
{
   public class Chapter : ITextContainer
   {
      public Chapter()
      {
         allVerses = new Dictionary<int, ITextContainer>();
      }

      public Chapter(int chapterNum)
         : this()
      {
         ChapterNumber = chapterNum;
      }

      public Chapter(int chapterNum, String chapterName) : this(chapterNum)
      {
         this.ChapterName = chapterName;
      }

      protected Dictionary<int, ITextContainer> allVerses { get; set; }
      public int ChapterNumber { get; internal set; }
      public String ChapterName { get; internal set; }
      protected int verseCount = 0;
      protected Verse pendingVerse { get; set; }

      public void AppendContents(ITextContainer TextContainer)
      {
         if (!(TextContainer is Verse))
            throw new ArgumentException();

         Verse VerseToAdd = TextContainer as Verse;
         verseCount++;

         allVerses.Add(VerseToAdd.VerseNumber.BeginNumber, VerseToAdd);
      }

      internal void AddNewVerse(String verseNo, String content)
      {
         verseCount++;
         pendingVerse = new Verse(verseNo, content);
         this.allVerses.Add(pendingVerse.VerseNumber.BeginNumber, pendingVerse);
      }

      internal void AppendToLastVerse(String unparsed)
      {
         pendingVerse.AppendContents(unparsed);
      }

      public Verse GetVerseByNumber(int number)
      {
         Verse verse;
         if (true == this.allVerses.ContainsKey(number))
            verse = (Verse) this
               .allVerses
               .Where(v => v.Key == number)
               .FirstOrDefault().Value;
         else
         {
            verse = (Verse)
               allVerses
               .Where(v => isNumberWithinRange(v.Value as Verse, number))
               .FirstOrDefault().Value;
         }
         return verse; 
      }

      private bool isNumberWithinRange(Verse verse, int number)
      {
         return
            verse.VerseNumber.BeginNumber >= number &&
                  verse.VerseNumber.EndNumber <= number;
      }

      public Dictionary<int, ITextContainer> GetContents()
      {
         return allVerses;
      }
   }
}
