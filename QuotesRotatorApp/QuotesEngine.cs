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
            LoadQuotesToContainer();
            StartRotationThread();
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
            //TODO fix: this reset currently selected group. 
            //What in case when currently selected group is removed after reload?

            Start();
        }

        public void ChangeCurrentGroup(QuotesGroup group)
        {
            lock (_locker)
            {
                container.SetCurrentGroup(group);
            }

            StartRotationThread();
        }

        private void LoadQuotesToContainer()
        {
            lock (_locker)
            {
                container = provider.GetQuotesList();
            }
        }

        private void StartRotationThread()
        {
            //clean prievious rotation thread, if exist
            flag.Set();
            Thread worker = new Thread(Work) { IsBackground = true };

            worker.Start(callbackForUiUpdateAction);
        }
    }
}