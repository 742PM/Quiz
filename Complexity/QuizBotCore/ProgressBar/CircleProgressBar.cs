namespace QuizBotCore.ProgressBar
{
    internal class CircleProgressBar : IProgressBar
    {
        public string GenerateProgressBar(int solved, int total)
        {
            return new string(DialogMessages.ProgressFilled, solved)
                .PadRight(total, DialogMessages.ProgressEmpty);
        }
    }
}