
using System;
using QuotesRotatorApp;
using Xunit;

namespace QuotesRotatorTests
{
    public class QuotesProviderTests
    {
        private QuotesProvider sut;
        private TestContentProvider contentProvider = new TestContentProvider();

        public QuotesProviderTests()
        {
            sut = new QuotesProvider(contentProvider);
        }

        [Fact]
        public void QuotesProvider_shouldReturnQuotesContainerObject()
        {
            Assert.NotNull(sut.GetQuotesList());
        }

        [Theory]
        [InlineData("Quote1", 1)]
        [InlineData("Quote1\r\nQuote2", 2)]
        [InlineData("\r\nQuote1\r\nQuote2\r\n\r\n", 2)]
        public void QuotesProvider_providedQuotesListWithOutGroupShouldPutAllQuotesInDefaultGroup(string content,
            int expectedQuotesCount)
        {
            contentProvider.SetLinesFromString(content);

            var actual = sut.GetQuotesList();

            Assert.Equal(actual.Groups.Count, 1);
            Assert.Equal(actual.Groups[0].Name, "Default");
            Assert.Equal(actual.Groups[0].Quotes.Count, expectedQuotesCount);
        }

        class TestContentProvider : IContentProvider
        {
            private string[] lines = new string[0];

            public string[] Lines { get { return lines; } }

            public void SetLinesFromString(string input)
            {
                lines = input.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            }
        }
    }
}