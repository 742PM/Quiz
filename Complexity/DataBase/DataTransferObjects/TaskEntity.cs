namespace DataBase
{
    public class TaskEntity
    {
        public int TaskId { get; set; }
        public bool IsSolved { get; set; }
        
        public AnswerEntity RightAnswer { get; set; }
        public AnswerEntity[] Answers { get; set; }
        
        public string[] Hints { get; set; }
        public string Question { get; set; }
        
    }
}