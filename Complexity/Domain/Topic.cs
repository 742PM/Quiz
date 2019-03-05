using System.Collections.Generic;

namespace Domain
{
    public class Topic
    {
        public Topic(IEnumerable<Level> levels)
        {
            Levels = levels;
        }

        public IEnumerable<Level> Levels { get; }
    }
}
