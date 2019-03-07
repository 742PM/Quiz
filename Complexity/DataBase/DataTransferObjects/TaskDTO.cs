namespace DataBase
{
    public class TaskDTO
    {
        public int TaskId { get; set; }
        public bool IsSolved { get; set; }
        
        public AnswerDTO RightAnswer { get; set; }
        public AnswerDTO[] Answers { get; set; }
        
        public string[] Hints { get; set; }
        public string Question { get; set; }
        
    }
}