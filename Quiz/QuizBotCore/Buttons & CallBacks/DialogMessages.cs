using System.Linq;

namespace QuizBotCore
{
    public static class DialogMessages
    {
        public const string CorrectAnswer = "Верно ✅";
        public const string WrongAnswer = "Подумай еще ❌";
        public const string FeedbackMessage = "Есть вопрос? Пиши нам!";
        public static readonly (string, string) FeedbackContact = ("Антон", "telegram.me/funfine");
        public const string NextTaskNotAvailable = "Реши эту, а потом подумаем о следующей";

        public const string LevelSelection = "Вижу с темой ты определился. " +
                                             "Выбирай уровень:";

        public static readonly char[] Alphabet = Enumerable.Range('A', 26).Select(x => (char) x).ToArray();
        public const string Progress = "Прогресс:";
        public const string ClosedLevel = "[Заблокирован]";
        public const string TopicName = "Тема:";
        public const string LevelName = "Уровень:";
        public const char ProgressFilled = '⬤';
        public const char ProgressEmpty = '◯';

        public const string Welcome = "Привет! Я Quibble бот, представляю из себя бесконечную викторину. \n" +
                                      "Решай задачки, открывай новые уровни, становись лучше. \n" +
                                      "Но не все так просто! Каждая задача уникальна и создается на основе шаблона" +
                                      "На каждый шаблон нужно решить определенное количество задач подряд и без ошибок." +
                                      "В случае неверного ответа, ваш прогресс по данному шаблону откатывается" +
                                      "Не удивляйтесь, когда кружочков ⬤ станет меньше.\n\n" +
                                      "Надеюсь прозвучало не слишком сложно) Выбирай тему и начинай!\n\n";
                                      

        public const string RequestForRotateDevice = "ВАЖНО: Из-за ограничения на максимальную ширину сообщения в Телеграм " +
                                                     "задачи могут отображать не самым комфортным образом. " +
                                                     "Советуем повернуть ваше устройство в горизонтальное положение.";
        public const string RequestForRotateDeviceGif = "https://i.imgur.com/D6Gt7zv.png";

        public const string ReportThanks = "Спасибо за обратную связь. " +
                                           "Мы обязательно рассмотрим ваше сообщение 🤙🏿";

        public const string ReportRequesting = "Опишите вашу проблему:";

        public static readonly string LevelSolved = "🏆🏆🏆 Уровень пройден 🏆🏆🏆\n";

        public const string LevelCompleted = "Молодец! Ты решил все задачки из этого уровня." +
                                              "Можешь продолжать решать этот уровень и дальше, задачки будут бесконечны," +
                                              "или же, можешь вернуться в меню выбора уровней и выбрать новый.";
    }
}