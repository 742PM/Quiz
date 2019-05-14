using System;
using System.Linq;
using QuizBotCore.Commands;
using QuizBotCore.Database;
using QuizBotCore.Parser;
using QuizBotCore.States;
using QuizBotCore.Transitions;
using QuizRequestService;

namespace QuizBotCore
{
    public class TelegramStateMachine : IStateMachine<ICommand>
    {
        private readonly IQuizService service;
        private readonly IUserRepository userRepository;

        public TelegramStateMachine(IQuizService service, IUserRepository userRepository)
        {
            this.service = service;
            this.userRepository = userRepository;
        }

        public (State state, ICommand command) GetNextState<TState, TTransition>(
            TState currentState,
            TTransition transition) where TState : State where TTransition : Transition
        {
            switch ((state: currentState, transition: transition))
            {
                case var t when t.transition is HelpTransition:
                    return (new TopicSelectionState(), new SelectTopicCommand()); 
                case var t when t.transition is FeedbackTransition:
                    return (new TopicSelectionState(), new FeedBackCommand()); 
                case var t when t.state is UnknownUserState:
                    return (new TopicSelectionState(), new SelectTopicCommand());
                case var t when t.state is ReportState reportState && t.transition is ReplyReportTransition reportTransition:
                    return (new TopicSelectionState(), new SendReportTaskCommand(reportState, reportTransition.MessageId));
                case var t when t.state is TopicSelectionState topicSelectionState:
                    return ProcessTopicSelectionState(topicSelectionState, t.transition);
                case var t when t.state is LevelSelectionState levelSelectionState:
                    return ProcessLevelSelectionState(levelSelectionState, t.transition);
                case var t when t.state is TaskState taskState:
                    return ProcessTaskState(taskState, t.transition);
            }

            return default;
        }

        private static (State, ICommand) ProcessTaskState(TaskState state, Transition transition)
        {
            switch (transition)
            {
                case BackTransition _:
                    return (new LevelSelectionState(state.TopicDto), new SelectLevelCommand(state.TopicDto));
                case ShowHintTransition _:
                    return (state, new ShowHintCommand());
                case ReportTransition _:
                    return (new ReportState(state.TopicDto,state.LevelDto), new ReportTaskCommand());
                case CorrectTransition correctTransition:
                    return (state, new CheckTaskCommand(state.TopicDto, state.LevelDto, correctTransition.Content));
            }

            return (new TopicSelectionState(), new SelectTopicCommand());
        }

        private (State, ICommand) ProcessLevelSelectionState(LevelSelectionState state, Transition transition)
        {
            switch (transition)
            {
                case BackTransition _:
                    return (new TopicSelectionState(), new SelectTopicCommand());
                case CorrectTransition correctTransition:
                    var levelDto = service.GetLevels(state.TopicDto.Id).First(x => x.Id == Guid.Parse(correctTransition.Content));
                    return
                        (new TaskState(state.TopicDto, levelDto),
                            new ShowTaskCommand(state.TopicDto, levelDto));
            }

            return (new TopicSelectionState(), new SelectTopicCommand());
        }

        private (State, ICommand) ProcessTopicSelectionState(TopicSelectionState state, Transition transition)
        {
            switch (transition)
            {
                case BackTransition _:
                    return (new TopicSelectionState(), new SelectTopicCommand());
                case CorrectTransition correctTransition:
                    var topicDto = service.GetTopics().First(x => x.Id == Guid.Parse(correctTransition.Content));
                    return (new LevelSelectionState(topicDto),
                        new SelectLevelCommand(topicDto));
            }
            return (new TopicSelectionState(), new SelectTopicCommand());
        }

    }
}