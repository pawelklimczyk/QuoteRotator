using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuotesRotatorApp
{
    public class QuotesProvider
    {
        public QuotesContainer GetQuotesList()
        {
            QuotesContainer container = new QuotesContainer
            {
                Groups = new List<QuotesGroup>
                {
                    new QuotesGroup
                    {
                        Name = "Default",
                        Quotes = File.ReadAllLines("quotes.txt").Where(s => !String.IsNullOrWhiteSpace(s)).ToList()
                    }
                }
            };

            return container;
        }
    }
}