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

        [JsonProperty("tasksCount")] public int TasksCount { get; }
        [JsonProperty("tasksSolved")] public int TasksSolved { get; }
    }
}