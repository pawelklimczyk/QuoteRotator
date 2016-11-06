using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuotesRotatorApp
{
    public class QuotesProvider
    {
        public List<string> GetQuotesList()
        {
            return File.ReadAllLines("quotes.txt").Where(s => !String.IsNullOrWhiteSpace(s)).ToList();
        }
    }
}