using System;
using System.Collections.Generic;

namespace Domain
{
    
    public class Task
    {
        public Task(IEnumerable<IAnswer> answers, IQuestion question, IEnumerable<IHint> hints, IAnswer rightAnswer)
        {
            Answers = answers;
            Question = question;
            Hints = hints;
            RightAnswer = rightAnswer;
        }

        public IQuestion Question { get;  }

        public IEnumerable<IAnswer> Answers { get;  }
        
        public IEnumerable<IHint> Hints { get; }

        public IAnswer RightAnswer { get; }
    }

    //Возможно, стоит заменить все три интерфейса на просто строки, YAGNI и все такое,
    //но я не уверен, ведь одержимость примитивами и все такое
    public interface IHint : IFormattable
    {
    }

    public interface IAnswer : IFormattable
    {
    }

    public interface IQuestion : IFormattable
    {
    }
}
