using System;
using System.Linq;
using Infrastructure.DDD;

namespace QuizBotCore
{
    public class DialogMessages : Entity
    {
        public static readonly char[] Alphabet = Enumerable.Range('A', 26).Select(x => (char) x).ToArray();
        public string CorrectAnswer { get; private set; } = "Верно ✅";
        public string WrongAnswer { get; private set; } = "Подумай еще ❌";
        public string FeedbackMessage { get; private set; } = "Есть вопрос? Пиши нам!";
        public string FeedbackWelcomeMessage { get; private set; } = "Отправляй вопрос прямо сюда :)";
        public  string FeedbackContact { get; private set; } =  "telegram.me/funfine";
        public string NextTaskNotAvailable { get; private set; } = "Реши эту, а потом подумаем о следующей";

        public string LevelSelection { get; private set; } = "Вижу с темой ты определился. " +
                                                     "Выбирай уровень:";

        public string Progress { get; private set; } = "Прогресс:";
        public string ClosedLevel { get; private set; } = "[Заблокирован]";
        public string TopicName { get; private set; } = "Тема:";
        public string LevelName { get; private set; } = "Уровень:";

        public string Welcome { get; private set; } = "Привет! Я Quibble бот, представляю из себя бесконечную викторину. \n" +
                                              "Решай задачки, открывай новые уровни, становись лучше. \n" +
                                              "Но не все так просто!\n" +
                                              "Каждая задача уникальна и создается на основе шаблона." +
                                              "На каждый шаблон нужно решить определенное количество задач подряд и без ошибок.\n" +
                                              "В случае неверного ответа, прогресс по шаблону откатывается." +
                                              "Не удивляйся, когда прогресс уменьшится.\n\n" +
                                              "Пора перейти к задачкам)\n" +
                                              "Выбирай тему и начинай!\n\n";


        public string RequestForRotateDevice { get; private set; } =
            "ВАЖНО: Из-за ограничения на максимальную ширину сообщения в Телеграм " +
            "задачи могут отображаться не самым комфортным образом. " +
            "Советуем повернуть ваш мобильный телефон в горизонтальное положение.";

        public string RequestForRotateDeviceGif { get; private set; } = "https://i.imgur.com/KPxuZ8H.png";

        public string ReportThanks { get; private set; } = "Спасибо за обратную связь. " +
                                                   "Мы обязательно рассмотрим ваше сообщение 🤙🏿";

        public string ReportRequesting { get; private set; } = "Опишите вашу проблему:";

        public string LevelSolved { get; private set; } = "🏆🏆🏆 Уровень пройден 🏆🏆🏆\n";

        public string LevelCompleted { get; private set; } = "Молодец! Ты решил все задачки из этого уровня." +
                                                     "Можешь продолжать решать этот уровень и дальше, задачки будут бесконечны," +
                                                     "или же, можешь вернуться в меню выбора уровней и выбрать новый.";

        public DialogMessages(Guid id) : base(id)
        {
        }
    }
}