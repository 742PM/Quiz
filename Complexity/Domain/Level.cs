using System.Collections.Generic;

namespace Domain
{
    public class Level
    {
        public Level(IEnumerable<Task> tasks)
        {
            Tasks = tasks;
        }

        public IEnumerable<Task> Tasks { get; }
    }
}
