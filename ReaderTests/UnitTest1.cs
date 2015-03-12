using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using USFMreader;
using System.Text;
using System.IO;

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
      public void WorkingText_Arabic_IsNotNull()
      {
         preliminaries();
         Assert.IsNotNull(arabicBible);
      }

   }
}
