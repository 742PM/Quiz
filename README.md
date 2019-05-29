
# Quiz

Quiz Service:

[![Build Status](https://dev.azure.com/AntonVoitsishevsky/Quiz/_apis/build/status/quiz-service%20-%201%20-%20CI?branchName=master)](https://dev.azure.com/AntonVoitsishevsky/Quiz/_build/latest?definitionId=16&branchName=master)

Quiz Telegram Bot:

[![Build Status](https://dev.azure.com/AntonVoitsishevsky/Quiz/_apis/build/status/quiz-bot%20-%20CI?branchName=bot_develop)](https://dev.azure.com/AntonVoitsishevsky/Quiz/_build/latest?definitionId=15&branchName=bot_develop)



<!-- TABLE OF CONTENTS -->
## Навигация

* [Описание проектной работы для курса ООП](#OOP)
* [Задумка Quiz сервиса](#about-the-project)
* [Wiki](https://github.com/complexitybot/Quiz/wiki/)
* [Contributing](#contributing)
* [Contributors](#contributors)


<!-- OOP -->
## Описание проекта
### Проблема
> Людям требуется разное время, чтобы освоить практические задачи по некоторой теме.
> Более того, никому не нравится решать однотипные скучные задачи.
> И всем хочется решать задачки без сложностей с регистрацией, новыми сервисами и прочими неприятностями.
### Предметная область
> * [Представление](Quiz/ComplexityWeb)
> * [Приложение](Quiz/Application) 
> * [Предметная область](Quiz/Domain) 
> * [Инфраструктура](Quiz/Infrastructure)
### Точки расширения
> Приложение представляет из себя Веб-сервис:
> * [Веб-сервис](Quiz/ComplexityWeb/)

> Коммуникация между ними происходит по HTTP, что позволяет как добавлять неограниченное количество клиентов к сервису,
> так и создавать реплики сервисов для одного клиента.

> * [Генерация задач](Quiz/Domain/Entities/TaskGenerators/) может расширяться с помощью разных методик рандомизации и создания.

> * [Стратегия выбора порядка задач для пользователя](Quiz/Application/Selectors/ITaskGeneratorSelector.cs) может быть любой.

> * Возможно создание клиента-администратора для настройки и управления сервисом.
### DI-контейнер
> Точкой сборки является [Web-сервис](Quiz/ComplexityWeb/) на ASP.Net Core.
> Пока используется [встроенный в ASP.Net Core контейнер](Quiz/ComplexityWeb/Startup.cs),
> в будущем может измениться.
### Попробовать в действии
> * [Веб-сервис (Swagger)](https://quiz-service.azurewebsites.net/swagger/index.html)
> * [Редактор уровней](https://quiz-editor.azurewebsites.net/index.html)
> * [Телеграм-бот](https://telegram.me/quiblebot)


<!-- ABOUT THE PROJECT -->
## О проекте
Чтобы освоить оценку сложности алгоритмов разным людям требуется разное количество практики. Некоторые схватывают уже после 2-3 примеров. Некоторым нужно прорешать штук 10.

Идея: создать сервис, в котором каждый сможет упражняться столько, сколько ему нужно. Сервис генерирует каждый раз новые задания, постепенно увеличивая уровень сложности.


<!-- CONTRIBUTING -->
## Contributing

Станьте частью нашего проекта, помогите нам сделать этот мир еще лучше. Любой ваш вклад будет **высоко цениться**. 

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request
6. Wait for our feedback. You are wonderful❤️


___
<!-- CONTRIBUTORS -->
## Contributors

| [<img src="https://avatars2.githubusercontent.com/u/31823086?s=460&v=4" width="100px;"/><br /><sub><b>Anton Voitsishevsky </b></sub>](https://github.com/FunFunFine)<br />|
  [<img src="https://avatars1.githubusercontent.com/u/38810090?s=460&v=4" width="100px;"/><br /><sub><b>Artemy Izakov </b></sub>](https://github.com/CGOptimum)<br />|  
  [<img src="https://avatars3.githubusercontent.com/u/19955305?s=460&v=4" width="100px;"/><br /><sub><b>Roman Budlyanskiy </b></sub>](https://github.com/bully434)<br />| 
  [<img src="https://avatars0.githubusercontent.com/u/37302383?s=460&v=4" width="100px;"/><br /><sub><b>Vasiliy Pahomov </b></sub>](https://github.com/vaspahomov)<br />|
|---|---|---|---|
