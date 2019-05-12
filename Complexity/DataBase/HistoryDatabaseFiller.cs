using System;
using Domain.Entities;
using Domain.Entities.TaskGenerators;

namespace DataBase
{
    internal class HistoryDatabaseFiller : IDatabaseFiller
    {
        public const string Question =
            "Найдите и запишите порядковые номера терминов, относящихся к другому историческому периоду.";

        public void Fill(MongoTaskRepository repository)
        {
            var topic = new Topic(Guid.NewGuid(), "История",
                                  "Описание: в этих заданиях нужно выбрать один вариант ответа", new Level[0]);

            var level = new Level(Guid.NewGuid(), "История России", new TaskGenerator[0], new Guid[0]);

            repository.InsertTopic(topic);
            repository.InsertLevel(topic.Id, level);

            var h1 = new TemplateTaskGenerator(Guid.NewGuid(),
                                               new[] { "Витте", "Канкрин", "Уваров", "Мен­ши­ков А. Д." },
                                               "Ниже приведён ряд имён го­су­дар­ствен­ных деятелей. Все они, за ис­клю­че­ни­ем одного, относятся к XIX в.",
                                               new[] { "Обратите внимание на спо­движ­ников Петра I." },
                                               "Мен­ши­ков А. Д.", 1, Question);

            var h2 = new TemplateTaskGenerator(Guid.NewGuid(),
                                               new[] { "Юрьев день", "Уроч­ные лета", "Пожилое", "Устав­ная грамота" },
                                               "Ниже приведён пе­ре­чень терминов. Все они, за ис­клю­че­ни­ем одного, свя­за­ны с про­цес­сом за­кре­по­ще­ния крестьян.",
                                               new[] { "Каковы условия освобождения крестьян?" }, "Устав­ная грамота",
                                               1, Question);

            var h3 = new TemplateTaskGenerator(Guid.NewGuid(),
                                               new[] { "помещик", "рядович", "пожилое", "боярин", "закуп" },
                                               "Ниже приведён пе­ре­чень терминов. Все они, за ис­клю­че­ни­ем одного, от­но­сят­ся к пе­ри­о­ду Древнерусского государства.",
                                               new[]
                                               {
                                                   "Что связано с тем, когда воз­ник­ла новая форма землевладения — поместье?"
                                               }, "помещик", 1, Question);

            var h4 = new TemplateTaskGenerator(Guid.NewGuid(),
                                               new[] { "старообрядец", "семибоярщина", "ополчение", "боярин" },
                                               "Ниже приведён пе­ре­чень терминов. Все они, за ис­клю­че­ни­ем одного, от­но­сят­ся к пе­ри­о­ду Смутного времени.",
                                               new[] { "Обратите внимание на про­тив­ников церковных реформ" },
                                               "старообрядец", 1, Question);
            var h5 = new TemplateTaskGenerator(Guid.NewGuid(),
                                               new[] { "стрельцы", "местничество", "поместье", "боярин" },
                                               "Ниже приведён пе­ре­чень терминов. Все они, за ис­клю­че­ни­ем одного, от­но­сят­ся к событиям, явлениям, про­ис­хо­див­шим во вто­рой половине XII—XV в.",
                                               new string[0], "стрельцы", 1, Question);

            var h6 = new TemplateTaskGenerator(Guid.NewGuid(),
                                               new[]
                                               {
                                                   "ордынский выход", "под­вор­ная подать", "за­по­вед­ные лета",
                                                   "воеводы"
                                               },
                                               "Ниже приведён пе­ре­чень терминов. Все они, за ис­клю­че­ни­ем одного, от­но­сят­ся к со­бы­ти­ям XVI—XVII вв.",
                                               new string[0], "ордынский выход", 1, Question);
            repository.InsertGenerators(topic.Id, level.Id,
                                      new[]
                                      {
                                          h1,
                                          h2,
                                          h3,
                                          h4,
                                          h5,
                                          h6
                                      });
        }
    }
}