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
            QuotesContainer container = new QuotesContainer
            {
                Groups = new List<QuotesGroup>
                {
                    new QuotesGroup
                    {
                        Name = defaultGroupName,
                        Quotes = contentProvider.Lines.Where(CanBeAddedAsQuote).ToList()
                    }
                }
            };

            return container;
        }

        private bool CanBeAddedAsQuote(string input)
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