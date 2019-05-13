namespace QuizRequestService.DTO
{
    public class HintDTO
    {
        public string HintText { get; set; }
        public bool HasNext { get; set; }

        public HintDTO(string hintText, bool hasNext)
        {
            HintText = hintText;
            HasNext = hasNext;
        }
    }
}