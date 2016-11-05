using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace QuotesRotatorApp
{
    public class QuotesProvider
    {
        private readonly Action<string> callback;
        private ManualResetEvent flag = new ManualResetEvent(false);
        public QuotesProvider(Action<string> callback)
        {
            this.callback = callback;

        }

        public void Start()
        {
            Thread worker = new Thread(Work) { IsBackground = true };

            worker.Start(callback);
        }

        private void Work(object c)
        {
            flag.Reset();
            List<string> quotes = ReadQuotesFromFile();
            Random random = new Random();
            Action<string> callbackAction = c as Action<string>;

            do
            {
                callbackAction(quotes[random.Next(quotes.Count)]);

                if (flag.WaitOne(60*1000)) break;

            } while (true);
        }

        private List<string> ReadQuotesFromFile()
        {
            return File.ReadAllLines("quotes.txt").Where(s => !String.IsNullOrWhiteSpace(s)).ToList();
        }

        public void ReloadQuotes()
        {
            flag.Set();
            Start();

        }
    }
}