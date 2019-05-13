namespace Application.Info
{
    public class LevelProgressInfo
    {
        public LevelProgressInfo(int tasksCount, int tasksSolved)
        {
            TasksCount = tasksCount;
            TasksSolved = tasksSolved;
        }

        public int TasksCount { get; }
        public int TasksSolved { get; }
    }
}