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

        public List<QuotesGroup> Groups { get; set; }

        public QuotesGroup GetOrCreateGroup(string line)
        {
            var existingGroup = Groups.FirstOrDefault(g => g.Name == line);

            if (existingGroup == null)
            {
                existingGroup = new QuotesGroup() { Name = line };
                Groups.Add(existingGroup);
            }

            return existingGroup;
        }
    }
}