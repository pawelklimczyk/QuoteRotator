using System;
using System.Collections.Generic;
using System.Threading;

namespace QuotesRotatorApp
{
    public class QuotesEngine
    {
        private readonly Action<string> callbackForUiUpdateAction;
        private ManualResetEvent flag = new ManualResetEvent(false);
        private QuotesProvider provider = new QuotesProvider();
        private QuotesContainer container;
        private static object _locker = new object();

        public QuotesEngine(Action<string> callbackForUIUpdateAction)
        {
            this.callbackForUiUpdateAction = callbackForUIUpdateAction;
        }

        public void Start()
        {
            container = provider.GetQuotesList();

            Thread worker = new Thread(Work) { IsBackground = true };

            worker.Start(callbackForUiUpdateAction);
        }

        public List<QuotesGroup> AvailableGroups
        {
            get
            {
                lock (_locker)
                {
                    return container.Groups;
                }
            }
        }

        private void Work(object c)
        {
            flag.Reset();
            Random random = new Random();
            Action<string> callbackAction = c as Action<string>;
            string nextQuote = String.Empty;

            do
            {
                lock (_locker)
                {
                    nextQuote = container.CurrentGroup.Quotes[random.Next(container.CurrentGroup.Quotes.Count)];
                }

                callbackAction(nextQuote);

                if (flag.WaitOne(60 * 1000)) break;

            } while (true);
        }
        
        public void ReloadQuotes()
        {
            flag.Set();
            Start();
        }

        public void ChangeCurrentGroup(QuotesGroup group)
        {
            lock (_locker)
            {
                container.SetCurrentGroup(group);
            }
        }
    }
}