namespace QuizBotCore.ProgressBar
{
    internal class CircleProgressBar : IProgressBar
    {
        public string GenerateProgressBar(int solved, int total)
        {
            return new string(ButtonNames.ProgressFilled, solved)
                .PadRight(total, ButtonNames.ProgressEmpty);
        }
    }
}