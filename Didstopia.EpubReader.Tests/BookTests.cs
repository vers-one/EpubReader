using System;
using System.Text;

using HtmlAgilityPack;
using Xunit;

namespace Didstopia.EpubReader.Tests
{
    public class BookTests
    {
        #region Constants
        private const string SampleBookLocalPath = "Samples/TheThreeBears.epub";
        private const string SampleBookRemotePath = "http://bobdc.s3.amazonaws.com/books/epubkidsbooks/TheThreeBears.epub";
        #endregion

        #region Tests
        [Theory]
        [InlineData(SampleBookLocalPath)]
        [InlineData(SampleBookRemotePath)]
        public void TestBookParsing(string filePath)
        {
            EpubBook book = EpubReader.ReadBook(filePath);
            TestBook(book);
        }

        [Theory]
        [InlineData(SampleBookLocalPath)]
        [InlineData(SampleBookRemotePath)]
        public async void TestBookParsingAsync(string filePath)
        {
            // Create the book and start testing it
            EpubBook book = await EpubReader.ReadBookAsync(filePath);
            TestBook(book);
        }
        #endregion

        #region Test helpers
        private void TestBook(EpubBook book)
        {
            // Test the book
            Assert.NotNull(book);
            Assert.False(string.IsNullOrEmpty(book.Title));
            Assert.False(string.IsNullOrEmpty(book.Author));
            Assert.NotNull(book.Content.Html);
            Assert.NotEmpty(book.Content.Html);

            // Test each chapter recursively
            foreach (EpubChapter chapter in book.Chapters)
                TestChapter(chapter);
        }

        private void TestChapter(EpubChapter chapter)
        {
            // Test the chapter
            Assert.NotNull(chapter);
            Assert.False(string.IsNullOrEmpty(chapter.Title));
            Assert.False(string.IsNullOrEmpty(chapter.HtmlContent));

            // Recursively test sub chapters if any exist
            foreach (EpubChapter subChapter in chapter.SubChapters)
                TestChapter(subChapter);
        }
        #endregion
    }
}
