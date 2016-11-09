using System;
using QuotesRotatorApp;

namespace QuotesRotatorTests
{
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