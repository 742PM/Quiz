using System.Collections.Generic;
using Application.Repositories;
using Application.Repositories.Entities;
using Domain.Entities.TaskGenerators;

namespace Application.Selectors
{
    public class ProgressTaskGeneratorSelector : ITaskGeneratorSelector
    {
        private readonly ITaskRepository taskRepository;

        public ProgressTaskGeneratorSelector(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        public TaskGenerator Select(IEnumerable<TaskGenerator> generators, UserProgressEntity progress)
        {
            
        }
    }
}