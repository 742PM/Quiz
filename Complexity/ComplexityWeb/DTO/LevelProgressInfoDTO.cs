using Newtonsoft.Json;

namespace ComplexityWebApi.DTO
{
    public class LevelProgressInfoDTO
    {
        public LevelProgressInfoDTO(int tasksCount, int tasksSolved)
        {
            TasksCount = tasksCount;
            TasksSolved = tasksSolved;
        }

        [JsonProperty("tasks_count")] public int TasksCount { get; }
        [JsonProperty("tasks_solved")] public int TasksSolved { get; }
    }
}