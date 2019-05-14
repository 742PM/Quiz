namespace QuizRequestService.DTO
{
    public class ProgressDTO
    {
        public int TasksCount { get; set; }
        public int TasksSolved { get; set; }

        public ProgressDTO(int tasksCount, int tasksSolved)
        {
            TasksCount = tasksCount;
            TasksSolved = tasksSolved;
        }
    }
}