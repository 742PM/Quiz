using System;
using System.Linq;
using Infrastructure.DDD;

namespace QuizBotCore
{
    public class DialogMessages : Entity
    {
        public static readonly char[] Alphabet = Enumerable.Range('A', 26).Select(x => (char) x).ToArray();
        public string CorrectAnswer { get; private set; } = "Верно ✅";
        public string WrongAnswer { get; private set; } = "Неверно ❌";
        public string FeedbackMessage { get; private set; } = "Есть вопрос? Пиши нам!";
        public string FeedbackWelcomeMessage { get; private set; } = "Отправляй вопрос прямо сюда :)";
        public  string FeedbackContact { get; private set; } = "telegram.me/funfine telegram.me/vaspahomov";
        public string NextTaskNotAvailable { get; private set; } = "Реши эту, а потом подумаем о следующей";

        public string LevelSelection { get; private set; } = "Вижу с темой ты определился. " +
                                                     "Выбирай уровень:";

        public string Progress { get; private set; } = "Прогресс:";
        public string ClosedLevel { get; private set; } = "[Заблокирован]";
        public string TopicName { get; private set; } = "Тема:";
        public string LevelName { get; private set; } = "Уровень:";

        public string Welcome { get; private set; } = "Привет! Я Quiz-бот, представляю из себя бесконечную викторину. \n" +
                                              "Решай задачки, открывай новые уровни, становись лучше. \n" +
                                              "Но не все так просто - каждая задача уникальна! \n" +
                                              "Нужно решить определенное количество похожих задач подряд и без ошибок.\n" +
                                              "В случае неверного ответа твой прогресс откатывается назад, " +
                                              "так что не удивляйся, когда твой прогресс уменьшится.\n\n" +
                                              "Пора перейти к задачкам,\n" +
                                              "выбирай тему и начинай!\n\n";


        public string ReportThanks { get; private set; } = "Спасибо за обратную связь. " +
                                                   "Мы обязательно рассмотрим твое сообщение 🙌";

        public string ReportRequesting { get; private set; } = "Опиши пожалуйста проблему:";

        public string LevelSolved { get; private set; } = "🏆🏆🏆 Уровень пройден 🏆🏆🏆\n";

        public string LevelCompleted { get; private set; } = "Молодец! Ты решил все задачки из этого уровня." +
                                                     "Можешь продолжать решать этот уровень и дальше" +
                                                     "или выбрать новый уровень, вернувшись назад";

        public string NoServiceConnection { get; private set; } = "У нас какие-то неполадки 😢\n" +
                                                                  " Попробуй позже.";

        public DialogMessages(Guid id) : base(id)
        {
        }
    }
}