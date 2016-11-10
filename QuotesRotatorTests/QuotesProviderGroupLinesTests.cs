using QuotesRotatorApp;
using Xunit;

namespace QuotesRotatorTests
{
    public class QuotesProviderGroupLinesTests
    {
        private QuotesProvider sut;
        private TestContentProvider contentProvider = new TestContentProvider();

        public QuotesProviderGroupLinesTests()
        {
            sut = new QuotesProvider(contentProvider);
        }

        [Theory]
        [InlineData("##Group1\r\nQuote1", 1)]
        [InlineData("##Group1\r\nQuote1\r\nQuote2", 2)]
        [InlineData("##Group1\r\nQuote1\r\n\r\nQuote2", 2)]
        public void QuotesProviderGroupLinesTests_providedQuotesWithOneGroupShouldAddThemToTheGroup(string content,
    int expectedQuotesCount)
        {
            contentProvider.SetLinesFromString(content);

            var actual = sut.GetQuotesList();

            Assert.Equal(actual.Groups.Count, 1);
            Assert.Equal(actual.Groups[0].Quotes.Count, expectedQuotesCount);
        }

        [Theory]
        [InlineData("##Group1\r\nQuote1\r\nQuote2\r\n##Group2\r\nQuote1\r\nQuote2", 2, 2)]
        [InlineData("##Group1\r\nQuote1\r\n##Group2\r\nQuote1\r\nQuote2", 1, 2)]
        [InlineData("##Group1\r\nQuote1\r\n##Group2\r\nQuote1\r\nQuote2\r\n##Group1\r\nQuote2", 2, 2)]
        [InlineData("##Group1\r\nQuote1\r\n##Group2\r\nQuote1\r\n##Group1\r\nQuote2\r\n##Group2\r\nQuote2", 2, 2)]
        [InlineData("##Group1\r\n\r\nQuote1\r\n\r\n##Group2\r\n\r\nQuote1\r\n\r\n##Group1\r\n\r\nQuote2\r\n\r\n##Group2\r\n\r\nQuote2", 2, 2)]
        public void QuotesProviderGroupLinesTests_providedQuotesWithTwoGroupsShouldAddThemToCorrectGroup(string content,
int expectedQuotesCountInGroup1, int expectedQuotesCountInGroup2)
        {
            contentProvider.SetLinesFromString(content);

            var actual = sut.GetQuotesList();

            Assert.Equal(actual.Groups.Count, 2);
            Assert.Equal(actual.Groups[0].Quotes.Count, expectedQuotesCountInGroup1);
            Assert.Equal(actual.Groups[1].Quotes.Count, expectedQuotesCountInGroup2);
        }

        [Theory]
        [InlineData("##Group1\r\nQuote1\r\n\r\nQuote1", 1)]
        [InlineData("##Group1\r\nQuote1\r\n\r\nQuote2\r\nQuote1\r\n\r\nQuote2", 2)]
        public void QuotesProviderDuplicatedQuoteLinesTests_providedDuplicatedQuotesShouldBeAddedJustOnce(string content,
int expectedQuotesCount)
        {
            contentProvider.SetLinesFromString(content);

            var actual = sut.GetQuotesList();

            Assert.Equal(actual.Groups.Count, 1);
            Assert.Equal(actual.Groups[0].Quotes.Count, expectedQuotesCount);
        }

        [Theory]
        [InlineData("Quote1", 1)]
        [InlineData("Quote1\r\nQuote2", 2)]
        public void QuotesProviderGroupsAppeareanceTests_providedContentWithQuotesShouldResultDefaultGroup(string content,
int expectedQuotesInDefaultGroup)
        {
            contentProvider.SetLinesFromString(content);

            var actual = sut.GetQuotesList();

            Assert.Equal(actual.Groups.Count, 1);
            Assert.Equal(actual.Groups[0].Name, QuotesProvider.DefaultGroupName);
            Assert.Equal(actual.Groups[0].Quotes.Count, expectedQuotesInDefaultGroup);
        }

        [Theory]
        [InlineData("##Group1\r\nQuote1", 1, new string[] { "Group1" })]
        [InlineData("##Group1\r\nQuote1\r\n##Group2\r\nQuote2", 2, new string[] { "Group1", "Group2" })]
        public void QuotesProviderGroupsAppeareanceTests_providedContentWithOneQuoteAndTwoGroupsShouldResultInCorrectGroupNumbers(string content,
int expectedGroupsCount, params string[] groupNames)
        {
            contentProvider.SetLinesFromString(content);

            var actual = sut.GetQuotesList();

            Assert.Equal(actual.Groups.Count, expectedGroupsCount);

            for (int i = 0; i < expectedGroupsCount; i++)
            {
                Assert.Equal(actual.Groups[i].Name, groupNames[i]);
                Assert.Equal(actual.Groups[i].Quotes.Count, 1);
            }
        }
    }
}