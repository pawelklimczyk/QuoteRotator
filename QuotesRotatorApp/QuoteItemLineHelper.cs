using System;

namespace QuotesRotatorApp
{
    public static class QuoteItemLineHelper
    {
        public static bool IsGroupItem(string line)
        {
            return line.StartsWith("##");
        }

        public static bool IsLogicalItem(string line)
        {
            return !String.IsNullOrWhiteSpace(line) && !line.StartsWith("//");
        }

        public static string StripCommandPrefixFromItem(string line)
        {
            return line.Substring(2, line.Length - 2);
        }
    }
}