using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.IO;
using USFMreader;
using USFMreader.GeneralizedFileSystem;

namespace ReaderTests
{
   [TestClass]
   public class UnitTest1
   {
      private String Basedir { get; set; }
      private WorkingText englishBibleLiteralTranslation { get; set; }
      private WorkingText arabicBible { get; set; }

      private void preliminaries()
      {
         if (null != Basedir)
            return;

         var parent3 = Directory.GetParent(System.IO.Directory.GetCurrentDirectory());
         var parent2 = Directory.GetParent(parent3.FullName);
         Basedir = Directory.GetParent(parent2.FullName).FullName;

         StringBuilder englishLiteralDirectory = new StringBuilder(Basedir);
         englishLiteralDirectory
            .Append(@"\TestTexts\Bibles\English\UnfoldingWord_LiteralBible");
         StringBuilder arabicDirectory = new StringBuilder(Basedir);
         arabicDirectory
            .Append(@"\TestTexts\Bibles\Arabic\UnfoldingWord_ar");

         englishBibleLiteralTranslation = new WorkingText(englishLiteralDirectory.ToString());
         arabicBible = new WorkingText(arabicDirectory.ToString());
      }

      [TestMethod]
      public void WorkingText_CreatesOkay()
      {
         WorkingText wt = new WorkingText();
         Assert.IsNotNull(wt);
      }

      [TestMethod]
      public void WorkingText_EnglishLiteral_IsNotNull()
      {
         preliminaries();
         Assert.IsNotNull(englishBibleLiteralTranslation);
      }

      [TestMethod]
      public void WorkingText_EnglishLiteral_Acts4_12_IsCorrect()
      {
         preliminaries();
         String verseText = englishBibleLiteralTranslation
            .GetBookByName("Acts")
            .GetChapterByNumber(4)
            .GetVerseByNumber(12).ToString();
         Assert.IsTrue(
            verseText.Contains
            ("there is no other name under heaven, that is given among men, by which we must be saved.")
            );
      }

      [TestMethod]
      public void WorkingText_ReadsFinalChapter_EnglishLiteral_ReadLuke24_6_ByGetVerse_IsCorrect()
      {
         preliminaries();
         String verseText = englishBibleLiteralTranslation
            .GetVerse("Luke", 24, 6).ToString();
         Assert.IsTrue(
            verseText.Contains
            ("He is not here, but is risen!")
            );
      }

      [TestMethod]
      public void WorkingText_ReadsFinalVerse_EnglishLiteral_ReadGenesis50_26_ByGetVerse_IsCorrect()
      {
         preliminaries();
         String verseText = englishBibleLiteralTranslation
            .GetVerse("Genesis", 50, 26).ToString();
         Assert.IsTrue(
            verseText.Contains
            ("So Joseph died, one hundred ten years old.")
            );
      }

      [TestMethod]
      public void WorkingText_ReadsFinalVerseOfOneChapterBook_EnglishLiteral_ReadJude1_25_ByGetVerse_IsCorrect()
      {
         preliminaries();
         String verseText = englishBibleLiteralTranslation
            .GetVerse("Jude", 1, 25).ToString();
         Assert.IsTrue(
            verseText.Contains
            ("glory, majesty, dominion and power")
            );
      }

      [TestMethod]
      public void WorkingText_Arabic_IsNotNull()
      {
         preliminaries();
         Assert.IsNotNull(arabicBible);
      }

      [TestMethod]
      public void WorkingText_ReadsAVerse_Arabic_ReadExodus2_3_ByGetVerse_IsCorrect()
      {
         preliminaries();
         String verseText = this.arabicBible
            .GetVerse("اَلْخُرُوجُ", 2, 3).ToString();
         Assert.IsTrue(
            verseText.Contains
            ("أَخَذَتْ لَهُ سَفَطًا مِنَ ٱلْبَرْدِيِّ وَطَلَتْهُ")
            );
      }

      //[TestMethod]
      public void WebPoller_GetTopDirectory_returnCorrect()
      {
         WebPoller poller = new WebPoller();
         poller.CrawlUnfoldingWordForUpdates();
      }

   }
}
