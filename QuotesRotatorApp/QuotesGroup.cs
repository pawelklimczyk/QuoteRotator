using System.Collections.Generic;

namespace QuotesRotatorApp
{
    public class QuotesGroup
    {
        public QuotesGroup(bool isSelected=false)
        {
            Quotes = new List<string>();
            IsSelected = isSelected;
        }

        public string Name { get; set; }
        public List<string> Quotes { get; set; }
        public bool IsSelected { get; set; }

        public void AddQuote(string line)
        {
            if (!Quotes.Contains(line))
            {
                Quotes.Add(line);
            }
        }
    }
}