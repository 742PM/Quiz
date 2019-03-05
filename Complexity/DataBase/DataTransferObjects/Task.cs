namespace DataBase
{
    public class Task
    {
        public int TaskId { get; set; }
        public bool IsSolved { get; set; }
        
        public Answer RightAnswer { get; set; }
        public Answer[] Answers { get; set; }
        
        public string[] Hints { get; set; }
        public string Question { get; set; }
        
    }
}