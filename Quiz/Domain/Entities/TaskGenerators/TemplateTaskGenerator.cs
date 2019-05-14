using System;
using System.Linq;
using Domain.Values;
using Infrastructure;
using Scriban;
using Scriban.Runtime;
using static Infrastructure.Storage;

namespace Domain.Entities.TaskGenerators
{
    public class TemplateTaskGenerator : TaskGenerator
    {
        public TemplateTaskGenerator(
            Guid id,
            string[] possibleAnswers,
            string text,
            string[] hints,
            string answer,
            int streak,
            string question) :
            base(id, streak) //TODO: remove default value and fix Database.Filler
        {
            PossibleAnswers = possibleAnswers ?? throw new ArgumentException($"{nameof(possibleAnswers)} are null");
            Text = text;
            Hints = hints ?? throw new ArgumentException($"{nameof(hints)} are null");
            Answer = answer;
            Question = question;
        }

        [MustBeSaved] public string[] PossibleAnswers { get; }

        [MustBeSaved] public string Text { get; }

        [MustBeSaved] public string Question { get; }

        [MustBeSaved] public string[] Hints { get; }

        /// <summary>
        ///     Should not be used as real answer for user;
        /// </summary>
        [MustBeSaved]
        public string Answer { get; }

        /// <summary>
        ///     Создает <see cref="Task" /> из полей шаблона путем рендеринга подстановок в строках.
        ///     Состояние подстановок обрабатывается последовательно.
        ///     <para>
        ///         Порядок рендеринга такой:
        ///         <list type="bullet">
        ///             <see cref="Text" />
        ///             <see cref="Answer" />
        ///             <see cref="Question" />
        ///             <see cref="Hints" />
        ///             <see cref="PossibleAnswers" />
        ///         </list>
        ///     </para>
        /// </summary>
        public override Task GetTask(Random randomSeed)
        {
            var so = CreateScriptObject(randomSeed);

            var simpleFieldsStorage = Concat(Text, Answer, Question);
            var hintsStorage = Concat(Hints ?? new string[0]);
            var answersStorage = Concat(PossibleAnswers ?? new string[0]);

            var fields = new[] { simpleFieldsStorage, hintsStorage, answersStorage };

            var ((code, answer, question), hints, answers)
                = fields.MapMany(vs => Concat(vs).Map(s => Template.Parse(s).Render(so)).Split())
                        .Select(r => r.Split().ToArray())
                        .ToArray();
            return new Task(code, hints, answer, Id, answers, question);
        }

        private static ScriptObject CreateScriptObject(Random random) => TemplateLanguage.Create(random);
    }
}