namespace QuizBotCore.Transitions
{
    public class CorrectTransition : Transition
    {
        public CorrectTransition(string content)
        {
            Content = content;
        }
        
        public string Content { get;  }
    }
}