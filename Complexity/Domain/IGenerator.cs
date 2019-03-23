using System.Collections.Generic;

namespace Domain
{
    public interface IGenerator
    {
        string RightAnswer { get; }
        IEnumerable<string> WrongAnswers { get; }
        IEnumerable<string> Hints { get; }
        Dictionary<string, string> Templates { get; }

        string GetTemplatedCode();
    }
}