using System.Configuration;
using System.IO;
using System.Linq;

namespace QuotesRotatorApp
{
    public class QuotesProvider
    {
        private readonly IContentProvider contentProvider;
        public const string DefaultGroupName = "Default";

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
                    current = container.GetOrCreateGroup(DefaultGroupName);
                    current.IsSelected = true;
                }

                current.AddQuote(line);
            }

            return container;
        }

        private class FileContentProvider : IContentProvider
        {
            private const string quotesFileKey = "quotesFile";

            public string[] Lines
            {
                get
                {
                    if (!ConfigurationManager.AppSettings.AllKeys.Contains(quotesFileKey))
                        throw new ConfigurationException($"[{quotesFileKey}] is not specified in .config file");
                    
                    if(!File.Exists(ConfigurationManager.AppSettings[quotesFileKey]))
                        throw new ConfigurationException($"[{ConfigurationManager.AppSettings[quotesFileKey]}] does not exist");

                    return File.ReadAllLines(ConfigurationManager.AppSettings[quotesFileKey]);
                }
            }
        }
    }
}