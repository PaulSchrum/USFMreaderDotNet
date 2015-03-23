using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;

namespace USFMreader
{
   public class WorkingText  // a whole Bible (of one translation)
   {
      public Dictionary<int, Book> allBooks { get; internal set; }
      public String ContentDirectory { get; set; }

      public DateTime dateModified { get; private set; }
      public String Language { get; private set; }
      public String NameTranslation { get; private set; }
      public String Slug { get; private set; }
      public TranslationStatus TranslationStatus { get; private set; }

      public WorkingText()
      {
         allBooks = new Dictionary<int, Book>(66);
         populateAllBooks();
      }

      public WorkingText(String contentDirectory) : this()
      {
         ContentDirectory = contentDirectory;
         GetStatus(contentDirectory);
         LoadAllBooks();
      }

      private void populateAllBooks()
      {
         allBooks.Add(1, new Book(this, "01-GEN.usfm"));
         allBooks.Add(2, new Book(this, "02-EXO.usfm"));
         allBooks.Add(3, new Book(this, "03-LEV.usfm"));
         allBooks.Add(4, new Book(this, "04-NUM.usfm"));
         allBooks.Add(5, new Book(this, "05-DEU.usfm"));
         allBooks.Add(6, new Book(this, "06-JOS.usfm"));
         allBooks.Add(7, new Book(this, "07-JDG.usfm"));
         allBooks.Add(8, new Book(this, "08-RUT.usfm"));
         allBooks.Add(9, new Book(this, "09-1SA.usfm"));
         allBooks.Add(10, new Book(this, "10-2SA.usfm"));
         allBooks.Add(11, new Book(this, "11-1KI.usfm"));
         allBooks.Add(12, new Book(this, "12-2KI.usfm"));
         allBooks.Add(13, new Book(this, "13-1CH.usfm"));
         allBooks.Add(14, new Book(this, "14-2CH.usfm"));
         allBooks.Add(15, new Book(this, "15-EZR.usfm"));
         allBooks.Add(16, new Book(this, "16-NEH.usfm"));
         allBooks.Add(17, new Book(this, "17-EST.usfm"));
         allBooks.Add(18, new Book(this, "18-JOB.usfm"));
         allBooks.Add(19, new Book(this, "19-PSA.usfm"));
         allBooks.Add(20, new Book(this, "20-PRO.usfm"));
         allBooks.Add(21, new Book(this, "21-ECC.usfm"));
         allBooks.Add(22, new Book(this, "22-SNG.usfm"));
         allBooks.Add(23, new Book(this, "23-ISA.usfm"));
         allBooks.Add(24, new Book(this, "24-JER.usfm"));
         allBooks.Add(25, new Book(this, "25-LAM.usfm"));
         allBooks.Add(26, new Book(this, "26-EZK.usfm"));
         allBooks.Add(27, new Book(this, "27-DAN.usfm"));
         allBooks.Add(28, new Book(this, "28-HOS.usfm"));
         allBooks.Add(29, new Book(this, "29-JOL.usfm"));
         allBooks.Add(30, new Book(this, "30-AMO.usfm"));
         allBooks.Add(31, new Book(this, "31-OBA.usfm"));
         allBooks.Add(32, new Book(this, "32-JON.usfm"));
         allBooks.Add(33, new Book(this, "33-MIC.usfm"));
         allBooks.Add(34, new Book(this, "34-NAM.usfm"));
         allBooks.Add(35, new Book(this, "35-HAB.usfm"));
         allBooks.Add(36, new Book(this, "36-ZEP.usfm"));
         allBooks.Add(37, new Book(this, "37-HAG.usfm"));
         allBooks.Add(38, new Book(this, "38-ZEC.usfm"));
         allBooks.Add(39, new Book(this, "39-MAL.usfm"));
         allBooks.Add(40, new Book(this, "40-MAT.usfm"));
         allBooks.Add(41, new Book(this, "41-MRK.usfm"));
         allBooks.Add(42, new Book(this, "42-LUK.usfm"));
         allBooks.Add(43, new Book(this, "43-JHN.usfm"));
         allBooks.Add(44, new Book(this, "44-ACT.usfm"));
         allBooks.Add(45, new Book(this, "45-ROM.usfm"));
         allBooks.Add(46, new Book(this, "46-1CO.usfm"));
         allBooks.Add(47, new Book(this, "47-2CO.usfm"));
         allBooks.Add(48, new Book(this, "48-GAL.usfm"));
         allBooks.Add(49, new Book(this, "49-EPH.usfm"));
         allBooks.Add(50, new Book(this, "50-PHP.usfm"));
         allBooks.Add(51, new Book(this, "51-COL.usfm"));
         allBooks.Add(52, new Book(this, "52-1TH.usfm"));
         allBooks.Add(53, new Book(this, "53-2TH.usfm"));
         allBooks.Add(54, new Book(this, "54-1TI.usfm"));
         allBooks.Add(55, new Book(this, "55-2TI.usfm"));
         allBooks.Add(56, new Book(this, "56-TIT.usfm"));
         allBooks.Add(57, new Book(this, "57-PHM.usfm"));
         allBooks.Add(58, new Book(this, "58-HEB.usfm"));
         allBooks.Add(59, new Book(this, "59-JAS.usfm"));
         allBooks.Add(60, new Book(this, "60-1PE.usfm"));
         allBooks.Add(61, new Book(this, "61-2PE.usfm"));
         allBooks.Add(62, new Book(this, "62-1JN.usfm"));
         allBooks.Add(63, new Book(this, "63-2JN.usfm"));
         allBooks.Add(64, new Book(this, "64-3JN.usfm"));
         allBooks.Add(65, new Book(this, "65-JUD.usfm"));
         allBooks.Add(66, new Book(this, "66-REV.usfm"));
      }

      private void LoadAllBooks()
      {
         for(int i=1;i<=66;i++)
         {
            allBooks[i].LoadBook();
         }
      }

      public void GetStatus(String contentDirectory)
      {
         if (!Directory.Exists(contentDirectory))
            throw new DirectoryNotFoundException(contentDirectory);

         String pathAndFileName = contentDirectory + @"\"
            + "status.json";

         JObject status_json;
         using (StreamReader file = File.OpenText(pathAndFileName))
         {
            using (JsonTextReader reader = new JsonTextReader(file))
            {
              status_json = (JObject) JToken.ReadFrom(reader);
            }
         }

         this.dateModified =
                     DateTime.ParseExact((String)status_json["date_modified"],
                           "yyyyMMdd",
                           CultureInfo.InvariantCulture,
                           DateTimeStyles.None);
         this.NameTranslation = (String)status_json["name"];
         this.Language = (String)status_json["lang"];
         this.Slug = (String)status_json["slug"];
         this.TranslationStatus = new TranslationStatus();
         this.TranslationStatus.CheckingEntity = (String) status_json["status"]["checking_entity"];
         this.TranslationStatus.CheckingLevel = int.Parse(
            (String)status_json["status"]["checking_level"],
            CultureInfo.InvariantCulture);
         this.TranslationStatus.Comments = (String)status_json["status"]["comments"];
         this.TranslationStatus.Contributors = (String)status_json["status"]["contributors"];
         this.TranslationStatus.PublishDate =
                     DateTime.ParseExact((String)status_json["status"]["publish_date"],
                           "yyyyMMdd",
                           CultureInfo.InvariantCulture,
                           DateTimeStyles.None);
         this.TranslationStatus.SourceText = (String)status_json["status"]["source_text"];
         this.TranslationStatus.SourceTextVersion = (String)status_json["status"]["source_text_version"];
         this.TranslationStatus.Version = (String)status_json["status"]["version"];

         // todo: parse status_json["books_published"] 
         // not doing this now because no essential information appears
         // appears to be in this data structure.
      }


      public Book GetBookByName(String name)
      {
         return allBooks
            .Where(bk => bk.Value.TOC2 != null)
            .Where(bk => bk.Value.TOC2.Equals(name))
            .FirstOrDefault().Value;
      }

      public Verse GetVerse(string bookName, int chapterNumber, int verseNumber)
      {
         Book book = this.GetBookByName(bookName);
         Chapter chapter = book.GetChapterByNumber(chapterNumber);
         Verse verse = chapter.GetVerseByNumber(verseNumber);
         return verse;
      }
   }
}
