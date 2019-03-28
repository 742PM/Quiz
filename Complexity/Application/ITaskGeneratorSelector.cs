using Domain.Entities.TaskGenerators;

namespace Application
{
    public interface ITaskGeneratorSelector
    {
        TaskGenerator Select((TaskGenerator, int)[] generators); //add more parameters?
    }
}