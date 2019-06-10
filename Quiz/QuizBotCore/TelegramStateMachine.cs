using System;
using System.Linq;
using Infrastructure.Result;
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
        private readonly ServiceManager serviceManager;

        public TelegramStateMachine(IQuizService service, IUserRepository userRepository, ServiceManager serviceManager)
        {
            this.service = service;
            this.userRepository = userRepository;
            this.serviceManager = serviceManager;
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
                case var t when t.state is ReportState reportState:
                    return ProcessReportState(reportState, t.transition);
                case var t when t.state is TopicSelectionState topicSelectionState:
                    return ProcessTopicSelectionState(topicSelectionState, t.transition);
                case var t when t.state is LevelSelectionState levelSelectionState:
                    return ProcessLevelSelectionState(levelSelectionState, t.transition);
                case var t when t.state is TaskState taskState:
                    return ProcessTaskState(taskState, t.transition);
            }
            return default;
        }

        private static (State, ICommand) ProcessReportState(ReportState state, Transition transition)
        {
            switch (transition)
            {
                case ReplyReportTransition reportTransition:
                    return (new TaskState(state.TopicDto, state.LevelDto),
                        new SendReportTaskCommand(state, state.MessageId, reportTransition.MessageId));
                case CancelTransition cancelTransition:
                    return (new TaskState(state.TopicDto, state.LevelDto),
                        new ShowTaskCommand(state.TopicDto, state.LevelDto));
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
                case ReportTransition reportTransition:
                    return (new ReportState(
                            reportTransition.MessageId,
                            reportTransition.TopicDto,
                            reportTransition.LevelDto),
                        new ReportTaskCommand());
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
                    var request = service.GetLevels(state.TopicDto.Id).Result;
                    if (request.HasValue)
                    {
                        var levelDto = request.Value.First(x => x.Id == Guid.Parse(correctTransition.Content));
                        return
                            (new TaskState(state.TopicDto, levelDto),
                                new ShowTaskCommand(state.TopicDto, levelDto));
                    }
                    return (state, new NoConnectionCommand());
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
                    var request = service.GetTopics().Result;
                    if (request.HasValue)
                    {
                        var topicDto = request.Value.First(x => x.Id == Guid.Parse(correctTransition.Content));
                        return (new LevelSelectionState(topicDto),
                            new SelectLevelCommand(topicDto));
                    }
                    return (state, new NoConnectionCommand());
            }

            return (new TopicSelectionState(), new SelectTopicCommand());
        }
    }
}