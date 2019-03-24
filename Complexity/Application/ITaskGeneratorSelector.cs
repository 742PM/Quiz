namespace Domain
{
    public interface ITaskGeneratorSelector
    {
        TasGenerator Select((TasGenerator, int)[] generators); //add more parameters?
    }
}