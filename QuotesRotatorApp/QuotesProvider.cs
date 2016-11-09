using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuotesRotatorApp
{
    public class QuotesProvider
    {
        private IContentProvider contentProvider;
        private const string defaultGroupName = "Default";

        public QuotesProvider()
        {
            contentProvider = new FileContentProvider();
        }

        public QuotesProvider(IContentProvider contentProvider)
        {
            this.contentProvider = contentProvider;
        }

        public QuotesContainer GetQuotesList()
        {
            QuotesContainer container = new QuotesContainer();
            QuotesGroup current = null;

            foreach (string line in contentProvider.Lines.Where(IsLogicalItemLine).ToList())
            {
                if (IsGroup(line))
                {
                    current = container.GetOrCreateGroup(line.Substring(2, line.Length - 2));
                    continue;
                }

                if (current == null)
                {
                    current = container.GetOrCreateGroup(defaultGroupName);
                }

                current.Quotes.Add(line);
            }

            return container;
        }

        private bool IsGroup(string line)
        {
            return line.StartsWith("##");
        }

        private bool IsLogicalItemLine(string input)
        {
            return !String.IsNullOrWhiteSpace(input) && !input.StartsWith("//");
        }

        public class FileContentProvider : IContentProvider
        {
            public string[] Lines
            {
                get { return File.ReadAllLines("quotes.txt"); }
            }
        }
    }
}