using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using QuizBotCore;
using QuizBotCore.Commands;
using QuizBotCore.Database;
using QuizBotCore.Parser;
using QuizRequestService;
using QuizWebHookBot.Services;

namespace QuizWebHookBot
{
    public class Startup
    {
        private const string DatabaseName = "telegramUsers";
        const string ServiceUri = "https://quiz-service.azurewebsites.net";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IMongoDatabase CreateDatabase(string connectionString) =>
            new MongoClient(connectionString).GetDatabase(DatabaseName);
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IUpdateService, UpdateService>();
            services.AddSingleton<IBotService, BotService>();
            services.AddScoped<IQuizService> (_ => new Requester(ServiceUri));
//            services.AddSingleton(_ => new MongoClient("mongodb://localhost:27017").GetDatabase("QuizDatabase"));
            services.AddSingleton(_ => new MongoClient(
                "mongodb://romutchio:romaha434" +
                               "@quizcluster-shard-00-00-kzjb8.azure.mongodb.net:27017," +
                               "quizcluster-shard-00-01-kzjb8.azure.mongodb.net:27017," +
                               "quizcluster-shard-00-02-kzjb8.azure.mongodb.net:27017/" +
                               "ComplexityBot?ssl=true&replicaSet=QuizCluster-shard-0&authSource=admin&retryWrites=true")
                .GetDatabase("QuizDatabase"));

            services.AddSingleton<IUserRepository, MongoUserRepository>();
            services.AddSingleton<MessageTextRepository>();
            services.AddScoped<IStateMachine<ICommand>, TelegramStateMachine>();
            services.AddScoped<IMessageParser, MessageParser>();
            services.AddScoped<ServiceManager>();

            services.Configure<BotConfiguration>(Configuration.GetSection("BotConfiguration"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
