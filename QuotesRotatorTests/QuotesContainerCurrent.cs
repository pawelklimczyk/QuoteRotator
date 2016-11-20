using QuotesRotatorApp;
using Xunit;

namespace QuotesRotatorTests
{
    public class QuotesContainerCurrent
    {
        private QuotesContainer sut;

        public QuotesContainerCurrent()
        {
            sut = new QuotesContainer();
        }

        [Fact]
        public void QuotesContainerCurrent_CurrentShouldBeNullIfNoGroups()
        {
            Assert.Null(sut.Current);
        }

        [Fact]
        public void QuotesContainerCurrent_CurrentPropertyShouldBeSetForFirstProvidedGroup()
        {
            sut.GetOrCreateGroup("SampleGroup");

            Assert.Equal(sut.Groups[0], sut.Current);
        }
    }
}