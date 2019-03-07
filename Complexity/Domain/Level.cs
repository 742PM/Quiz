using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    [Entity]
    public class Level
    {
        public Level(IEnumerable<Task> tasks, Guid id, Dictionary<Task, int> taskWeights)
        {
            Tasks = new HashSet<Task>(tasks);
            Id = id;
            TaskWeights = taskWeights;
        }

        public Guid Id { get; }

        public HashSet<Task> Tasks { get; }

        public int CurrentProgress =>
            Tasks.Select(t => TaskWeights[t])
                 .Sum() -
            SolvedTasks.Select(t => TaskWeights[t])
                       .Sum();

        public HashSet<Task> SolvedTasks { get; } = new HashSet<Task>();

        public Dictionary<Task, int> TaskWeights { get; }

        public Level Solve(Task task)
        {
            SolvedTasks.Add(task);
            return this;
        }
    }
}
