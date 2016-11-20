using System;
using System.Collections.Generic;
using System.Threading;

namespace QuotesRotatorApp
{
    public class QuotesEngine
    {
        private readonly Action<string> callbackForUiUpdateAction;
        private ManualResetEvent flag = new ManualResetEvent(false);
        QuotesProvider provider = new QuotesProvider();

        public QuotesEngine(Action<string> callbackForUIUpdateAction)
        {
            this.callbackForUiUpdateAction = callbackForUIUpdateAction;
        }

        public void Start()
        {
            Thread worker = new Thread(Work) { IsBackground = true };

            worker.Start(callbackForUiUpdateAction);
        }

        private void Work(object c)
        {
            flag.Reset();
            var  container = provider.GetQuotesList();
            Random random = new Random();
            Action<string> callbackAction = c as Action<string>;

            do
            {
                callbackAction(container.Current.Quotes[random.Next(container.Current.Quotes.Count)]);

                if (flag.WaitOne(60 * 1000)) break;

            } while (true);
        }


        public void ReloadQuotes()
        {
            flag.Set();
            Start();
        }
    }
}