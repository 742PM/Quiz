namespace Domain
{
    public interface ITaskGeneratorSelector
    {
        ITaskGenerator Select((ITaskGenerator, int)[] generators); //add more parameters?
    }
}