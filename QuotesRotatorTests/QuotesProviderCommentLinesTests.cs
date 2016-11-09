using QuotesRotatorApp;
using Xunit;

namespace QuotesRotatorTests
{
    public class QuotesProviderCommentLinesTests
    {
        private QuotesProvider sut;
        private TestContentProvider contentProvider = new TestContentProvider();

        public QuotesProviderCommentLinesTests()
        {
            sut = new QuotesProvider(contentProvider);
        }

        [Theory]
        [InlineData("//Comment", 0)]
        [InlineData("Quote1\r\n//comment 2", 1)]
        [InlineData("//comment\r\nQuote1\r\n//comment\r\n//comment\r\nQuote2\r\n//comment\r\n", 2)]
        public void QuotesProviderCommentLinesTestsr_providedQuotesListWithCommentLinesShouldSkipThem(string content,
            int expectedQuotesCount)
        {
            contentProvider.SetLinesFromString(content);

            var actual = sut.GetQuotesList();

            Assert.Equal(actual.Groups[0].Quotes.Count, expectedQuotesCount);
        }
    }
}