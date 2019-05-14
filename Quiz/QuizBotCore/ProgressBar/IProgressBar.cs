namespace QuizBotCore.ProgressBar
{
    interface IProgressBar
    {
        string GenerateProgressBar(int solved, int total);
    }
}