using System;

namespace QuotesRotatorApp
{
    public interface IContentProvider
    {
        String[] Lines { get; }
    }
}