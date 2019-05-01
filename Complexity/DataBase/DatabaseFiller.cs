using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Entities.TaskGenerators;
using MongoDB.Driver;

namespace DataBase
{
    public class DatabaseFiller
    {
        public void Fill()
        {
            var db = MongoDatabaseInitializer.CreateMongoDatabase("ComplexityBot", "romutchio", "romaha434");
            var taskRepo = SetupTaskRepository(db);
            var topic = new Topic(Guid.NewGuid(), "Сложность алгоритмов",
                "Описание: Задачи на разные алгоритмы и разные сложности", new Level[0]);
            var singleloopLevels = new Level(Guid.NewGuid(), "Циклы", new TaskGenerator[0], new Guid[0]);
            var doubleLoopLevels = new Level(Guid.NewGuid(), "Двойные Циклы", new TaskGenerator[0], new Guid[0]);
            
            taskRepo.InsertTopic(topic);
            taskRepo.InsertLevel(topic.Id, singleloopLevels);
            taskRepo.InsertLevel(topic.Id, doubleLoopLevels);


            var forLoop1 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] {"Θ(n)", "Θ(n²)","Θ(1)", "Θ(n³)"},
                "for (int i = {{from1}}; i < {{to1}}; i+={{iter1}})\n" +
                "\t\tc++\n",
                new string[0], "Θ(n)", 1);
            
            var forLoop2 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] {"Θ(n)", "Θ(n²)","Θ(1)", "Θ(log n)"},
                "for (int i = {{from1}}; i < {{to1}}; i*={{iter1}})\n" +
                "\t\tc++\n",
                new[] {"Цикл с удвоением"}, "Θ(log n)", 1);
            
            var forLoop3 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] {"Θ(n log n)", "Θ(n)", "Θ(n²)","Θ(n³)", "Θ(log n)"},
                "for (int i = {{from1}}; i < {{to1}}*{{to1}}; i*={{iter1}})\n" +
                "\t\tc++\n",
                new[] {"Свойства логарифма"}, "Θ(log n)", 1);
            
            var forLoop4 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] {"Θ(sqrt(n))", "Θ(n)", "Θ(n²)","Θ(1)", "Θ(log n)"},
                "for (int i = {{from1}}; i * i < {{to1}}; i+={{iter1}})\n" +
                "\t\tc++\n",
                new string[0], "Θ(sqrt(n))", 1);       
            
            var forLoop5 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] {"Θ(sqrt(n))", "Θ(n)", "Θ(n²)","Θ(1)", "Θ(log n)"},
                "for (int i = {{from1}}; i < {{to1}}*{{to1}}; i+={{iter1}})\n" +
                "\t\tc++\n",
                new string[0], "Θ(n²)", 1);
                        
            var forLoop6 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] {"Θ(sqrt(n))", "Θ(n)", "Θ(n²)","Θ(1)", "Θ(log n)"},
                "for (int i = {{from1}}; i % {{from1}} != 0; i+={{iter1}})\n" +
                "\t\tc++\n",
                new string[0], "Θ(1)", 1);
                        
            var forLoop7 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] {"Θ(sqrt(n))", "Θ(n)", "Θ(n²)","Θ(1)", "Θ(log n)"},
                "for (int i = {{from1}}; i < {{to1}}*{{to1}}; i*={{iter1}})\n" +
                "\t\tc++\n",
                new string[0], "Θ(log n)", 1);
            
            

            taskRepo.InsertGenerators(topic.Id, singleloopLevels.Id, new List<TaskGenerator>
            {
                forLoop1,
                forLoop2,
                forLoop3,
                forLoop4,
                forLoop5,
                forLoop6,
                forLoop7
            });


            var double1 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] {"Θ(n)", "Θ(n²)", "Θ(n³)"},
                "for (int i = {{from1}}; i < {{to1}}; i+={{iter1}})\n" +
                "\tfor (int j = {{from2}}; j < {{to2}}; j+={{iter2}})\n" +
                "\t\tc++\n",
                new[] {"Независимые циклы"}, "Θ(n²)", 1);

            var double2 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] {"Θ(nlogn)", "Θ(n²)", "Θ(n³)"},
                "for (int i = {{from1}}; i < {{to1}}; i+={{iter1}})\n" +
                "\tfor (int j = {{from2}}; j < {{to2}}; j*={{iter2}})\n" +
                "\t\tc++\n",
                new[] {"Независимые циклы"}, "Θ(nlogn)", 1);

            var double3 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] {"Θ(nlogn)", "Θ(n²)", "Θ(n³)"},
                "for (int i = {{from1}}; i < {{to1}}; i+={{iter1}})\n" +
                "\tfor (int j = {{from2}}; j < {{to2}}; j+=i)\n" +
                "\t\tc++\n",
                new[] {"Частичная сумма гармонического ряда"}, "Θ(nlogn)", 1);

            var double4 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] {"Θ(nlogn)", "Θ(n²)", "Θ(n³)", "Θ(n)"},
                "for (int i = {{from1}}; i < {{to1}}; i+={{iter1}})\n" +
                "\tfor (int j = {{from2}}; j < {{to2}}; j*=i)\n" +
                "\t\tc++\n",
                new[] {"logn * li(n) = logn * n / logn = n"}, "Θ(n)", 1);

            var double5 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] {"Θ(nlogn)", "Θ(n²)", "Θ(n³)"},
                "for (int i = {{from1}}; i < {{to1}}; i+={{iter1}})\n" +
                "\tfor (int j = {{from2}}; j < i; j+={{iter2}})\n" +
                "\t\tc++\n",
                new[] {"Арифметическая прогрессия"}, "Θ(n²)", 1);

            var double6 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] {"Θ(nlogn)", "Θ(n²)", "Θ(n³)"},
                "for (int i = {{from1}}; i < {{to1}}; i+={{iter1}})\n" +
                "\tfor (int j = {{from2}}; j < i; j*={{iter2}})\n" +
                "\t\tc++\n",
                new[] {"Логарифм факториала, Формула Стирлинга"}, "Θ(nlogn)", 1);

            var double7 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] {"Θ(nlogn)", "Θ(n²)", "Θ(n³)"},
                "for (int i = {{from1}}; i < {{to1}}; i+={{iter1}})\n" +
                "\tfor (int j = {{from2}}; j < i; j*={{iter2}})\n" +
                "\t\tc++\n",
                new[] {"Логарифм факториала, Формула Стирлинга"}, "Θ(nlogn)", 1);

            var double8 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] {"Θ(nlogn)", "Θ(n²)", "Θ(n³)"},
                "for (int i = {{from1}}; i < {{to1}}; i*={{iter1}})\n" +
                "\tfor (int j = {{from2}}; j < i; j+={{iter2}})\n" +
                "\t\tc++\n",
                new[] {"Независимые циклы"}, "Θ(nlogn)", 1);

            var double9 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] {"Θ(log² n)", "Θ(nlogn)", "Θ(n²)"},
                "for (int i = {{from1}}; i < {{to1}}; i*={{iter1}})\n" +
                "\tfor (int j = {{from2}}; j < i; j*={{iter2}})\n" +
                "\t\tc++\n",
                new[] {"Независимые циклы"}, "Θ(log² n)", 1);

            var double10 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] {"Θ(log² n)", "Θ(nlogn)", "Θ(n²)"},
                "for (int i = {{from1}}; i < {{to1}}; i*={{iter1}})\n" +
                "\tfor (int j = {{from2}}; j < i; j+=i)\n" +
                "\t\tc++\n",
                new[] {"Геометрическая прогрессия"}, "Θ(n)", 1);


            var double11 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] {"Θ(logn loglogn)", "Θ(nlogn)", "Θ(n)"},
                "for (int i = {{from1}}; i < {{to1}}; i*={{iter1}})\n" +
                "\tfor (int j = {{from2}}; j < i; j*=i)\n" +
                "\t\tc++\n",
                new[] {"Cмена основания логарифма и частичная сумма гармонического ряда"}, "Θ(logn loglogn)", 1);

            var double12 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] {"Θ(log² n)","Θ(n)", "Θ(nlogn)", "Θ(n²)"},
                "for (int i = {{from1}}; i < {{to1}}; i*={{iter1}})\n" +
                "\tfor (int j = {{from2}}; j < i; j+={{iter2}})\n" +
                "\t\tc++\n",
                new[] {"Геометрическая прогрессия"}, "Θ(n)", 1);

            var double13 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] {"Θ(log² n)", "Θ(nlogn)", "Θ(n²)", "Θ(n)"},
                "for (int i = {{from1}}; i < {{to1}}; i*={{iter1}})\n" +
                "\tfor (int j = {{from2}}; j < i; j*={{iter2}})\n" +
                "\t\tc++\n",
                new[] {"Арифметическая прогрессия"}, "Θ(log² n)", 1);
            
            taskRepo.InsertGenerators(topic.Id, doubleLoopLevels.Id,
                new List<TaskGenerator>
                {
                    double1,
                    double2,
                    double3,
                    double4,
                    double5,
                    double6,
                    double7,
                    double8,
                    double9,
                    double10,
                    double11,
                    double12,
                    double13
                });
        }

        private static MongoUserRepository SetupUserRepository(IMongoDatabase db)
        {
            MongoDatabaseInitializer.SetupDatabase();
            var userRepo = new MongoUserRepository(db);

            return userRepo;
        }


        private static MongoTaskRepository SetupTaskRepository(IMongoDatabase db)
        {
            var taskRepo = new MongoTaskRepository(db);

            return taskRepo;
        }
    }
}