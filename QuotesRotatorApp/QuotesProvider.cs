using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuotesRotatorApp
{
    public class QuotesProvider
    {
        private IContentProvider contentProvider;

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
                        Name = "Default",
                        Quotes = contentProvider.Lines.Where(s => !String.IsNullOrWhiteSpace(s)).ToList()
                    }
                }
            };

            return container;
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