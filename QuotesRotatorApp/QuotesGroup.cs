using System.Collections.Generic;

namespace QuotesRotatorApp
{
    public class QuotesGroup
    {
        public QuotesGroup()
        {
            Quotes = new List<string>();
        }

        public string Name { get; set; }
        public List<string> Quotes { get; set; }

        public void AddQuote(string line)
        {
            if (!Quotes.Contains(line))
            {
                Quotes.Add(line);
            }
        }
    }
}