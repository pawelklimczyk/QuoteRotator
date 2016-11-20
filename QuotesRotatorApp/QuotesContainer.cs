using System.Collections.Generic;
using System.Linq;

namespace QuotesRotatorApp
{
    public class QuotesContainer
    {
        public QuotesContainer()
        {
            Groups = new List<QuotesGroup>();
        }

        public QuotesGroup CurrentGroup { get; private set; }

        public List<QuotesGroup> Groups { get; }

        public QuotesGroup GetOrCreateGroup(string line)
        {
            var existingGroup = Groups.FirstOrDefault(g => g.Name == line);

            if (existingGroup == null)
            {
                existingGroup = new QuotesGroup { Name = line };
                Groups.Add(existingGroup);
            }

            if (Groups.Count == 1)
            {
                SetCurrentGroup(existingGroup);
            }

            return existingGroup;
        }

        public void SetCurrentGroup(QuotesGroup group)
        {
            CurrentGroup = Groups.Find(g => g == group);
        }
    }
}