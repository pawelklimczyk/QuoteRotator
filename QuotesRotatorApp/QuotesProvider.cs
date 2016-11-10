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

            foreach (string line in contentProvider.Lines.Where(QuoteItemLineHelper.IsLogicalItem).ToList())
            {
                if (QuoteItemLineHelper.IsGroupItem(line))
                {
                    current = container.GetOrCreateGroup(QuoteItemLineHelper.StripCommandPrefixFromItem(line));
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


        private class FileContentProvider : IContentProvider
        {
            public string[] Lines
            {
                get { return File.ReadAllLines("quotes.txt"); }
            }
        }
    }
}