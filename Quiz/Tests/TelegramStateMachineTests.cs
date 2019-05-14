using System;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using QuizBotCore;
using QuizBotCore.Commands;
using QuizBotCore.Database;
using QuizBotCore.States;
using QuizBotCore.Transitions;
using QuizRequestService;
using QuizRequestService.DTO;

namespace Tests
{
    [TestFixture]
    public class TelegramStateMachineTests
    {
        private IQuizService quizService;
        private IUserRepository userRepository;
        private IStateMachine<ICommand> stateMachine;
        private TopicDTO testTopic;
        private LevelDTO testLevel;

        [SetUp]
        public void Setup()
        {
            quizService = A.Fake<IQuizService>();
            testTopic = new TopicDTO(Guid.NewGuid(), "TestTopicName");
            testLevel = new LevelDTO(Guid.NewGuid(), "TestLevelName");
            A.CallTo(() => quizService.GetTopics()).Returns(new[] { testTopic });
            A.CallTo(() => quizService.GetLevels(testTopic.Id)).Returns(new[] { testLevel });
            userRepository = A.Fake<IUserRepository>();
            stateMachine = new TelegramStateMachine(quizService, userRepository);
        }

        [Test]
        public void GetNextState_HelpTransition_ReturnTopicSelectionState()
        {
            var unknownUserState = new UnknownUserState();
            var helpTransition = new HelpTransition();
            var (state, command) = stateMachine.GetNextState(unknownUserState, helpTransition);
            state.Should().BeOfType<TopicSelectionState>();
            command.Should().BeOfType<SelectTopicCommand>();
        }

        [Test]
        public void GetNextState_FeedbackTransition_ReturnTopicSelectionState()
        {
            var unknownUserState = new UnknownUserState();
            var feedbackTransition = new FeedbackTransition();
            var (state, command) = stateMachine.GetNextState(unknownUserState, feedbackTransition);
            state.Should().BeOfType<TopicSelectionState>();
            command.Should().BeOfType<FeedBackCommand>();
        }

        [Test]
        public void GetNextState_UnknownState_ReturnTopicSelectionState()
        {
            var unknownUserState = new UnknownUserState();
            var backTransition = new BackTransition();
            var (state, command) = stateMachine.GetNextState(unknownUserState, backTransition);
            state.Should().BeOfType<TopicSelectionState>();
            command.Should().BeOfType<SelectTopicCommand>();
        }

        [Test]
        public void GetNextState_TopicSelectionState_OnBackTransition_ReturnTopicSelectionState()
        {
            var unknownUserState = new TopicSelectionState();
            var backTransition = new BackTransition();
            var (state, command) = stateMachine.GetNextState(unknownUserState, backTransition);
            state.Should().BeOfType<TopicSelectionState>();
            command.Should().BeOfType<SelectTopicCommand>();
        }

        [Test]
        public void GetNextState_TopicSelectionState_OnCorrectTransition_ReturnTopicSelectionState()
        {
            var unknownUserState = new TopicSelectionState();
            var backTransition = new CorrectTransition(testTopic.Id.ToString());
            var (state, command) = stateMachine.GetNextState(unknownUserState, backTransition);
            state.As<LevelSelectionState>()
                .TopicDto.Should()
                .Be(new LevelSelectionState(testTopic).TopicDto);
            command.Should().BeOfType<SelectLevelCommand>();
        }

        [Test]
        public void GetNextState_LevelSelectionState_OnBackTransition_ReturnTopicSelectionState()
        {
            var unknownUserState = new LevelSelectionState(testTopic);
            var backTransition = new BackTransition();
            var (state, command) = stateMachine.GetNextState(unknownUserState, backTransition);
            state.Should().BeOfType<TopicSelectionState>();
            command.Should().BeOfType<SelectTopicCommand>();
        }

        [Test]
        public void GetNextState_LevelSelectionState_OnCorrectTransition_ReturnTopicSelectionState()
        {
            var unknownUserState = new LevelSelectionState(testTopic);
            var backTransition = new CorrectTransition(testLevel.Id.ToString());
            var (state, command) = stateMachine.GetNextState(unknownUserState, backTransition);
            var taskState = state.As<TaskState>();
            taskState.TopicDto.Should().Be(testTopic);
            taskState.LevelDto.Should().Be(testLevel);
            command.Should().BeOfType<ShowTaskCommand>();
        }

        [Test]
        public void GetNextState_TaskState_OnBackTransition_ReturnLevelSelectionState()
        {
            var unknownUserState = new TaskState(testTopic, testLevel);
            var backTransition = new BackTransition();
            var (state, command) = stateMachine.GetNextState(unknownUserState, backTransition);
            var levelSelectionState = state.As<LevelSelectionState>();
            levelSelectionState.TopicDto.Should().Be(testTopic);
            command.Should().BeOfType<SelectLevelCommand>();
        }

        [Test]
        public void GetNextState_TaskState_OnShowHintTransition_ReturnShowHintCommandAndSaveState()
        {
            var unknownUserState = new TaskState(testTopic, testLevel);
            var backTransition = new ShowHintTransition();
            var (state, command) = stateMachine.GetNextState(unknownUserState, backTransition);
            var taskState = state.As<TaskState>();
            taskState.TopicDto.Should().Be(testTopic);
            taskState.LevelDto.Should().Be(testLevel);
            command.Should().BeOfType<ShowHintCommand>();
        }

        [Test]
        public void GetNextState_TaskState_OnCorrectTransition_ReturnCheckTaskCommandAndSaveState()
        {
            var unknownUserState = new TaskState(testTopic, testLevel);
            var backTransition = new CorrectTransition("answer");
            var (state, command) = stateMachine.GetNextState(unknownUserState, backTransition);
            var taskState = state.As<TaskState>();
            taskState.TopicDto.Should().Be(testTopic);
            taskState.LevelDto.Should().Be(testLevel);
            command.Should().BeOfType<CheckTaskCommand>();
        }
    }
}