using System;
using Domain;
using Ninject;

namespace Application
{
    public class Application
    {
        //TODO: build asp.net, database etc.
        static Application()
        {
            var container = new StandardKernel();

            container.Bind<IQuizApi>()
                     .To<QuizApi>()
                     .InSingletonScope()
                     .WithConstructorArgument("id", Guid.Empty);
            container.Bind<ITaskGenerator>()
                     .To<ExampleTaskGenerator>()
                     .WithConstructorArgument("hint", "Wow"); //Какое-то странное что-то, просто пример
        }
    }
}
