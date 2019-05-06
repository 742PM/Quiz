namespace Application.Info
{
    public class HintInfo
    {
        public HintInfo(string hintText, bool hasNext)
        {
            HintText = hintText;
            HasNext = hasNext;
        }

        public string HintText { get; }
        public bool HasNext { get; }
    }
}