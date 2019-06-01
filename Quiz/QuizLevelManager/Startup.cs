using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using QuizRequestExtendedService;
using QuizRequestExtendedService.Database;

namespace QuizLevelManager
{
    public class Startup
    {
        private const string ServerUrlEnvironmentVariable = "SERVER_URL";
        public const string MongoUsernameEnvironmentVariable = "MONGO_USERNAME";
        public const string MongoPasswordEnvironmentVariable = "MONGO_PASSWORD";
        public const string MongoDatabaseNameEnvironmentVariable = "MONGO_DB_NAME";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton(ConnectToDatabase());

            services.AddScoped<IQuizServiceExtended>(_ =>
                new Requester(Environment.GetEnvironmentVariable(ServerUrlEnvironmentVariable)));
            services.AddScoped<IUserRepository<Guid, Guid>>(provider =>
                new UserRepository<Guid, Guid>(provider.GetService<IMongoDatabase>(), Guid.NewGuid));

            services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/build");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment()) spa.UseReactDevelopmentServer("start");
            });
        }

        private static IMongoDatabase ConnectToDatabase(string databaseName = default, string username = default,
            string password = default, string name = "QuizDatabase")
        {
            databaseName = databaseName ?? Environment.GetEnvironmentVariable(MongoDatabaseNameEnvironmentVariable);
            username = username ?? Environment.GetEnvironmentVariable(MongoUsernameEnvironmentVariable);
            password = password ?? Environment.GetEnvironmentVariable(MongoPasswordEnvironmentVariable);
            var connectionString = username is null || password is null
                ? "mongodb://localhost:27017"
                : $"mongodb://{username}:{password}@quizcluster-shard-00-00-kzjb8.azure.mongodb.net:27017," +
                  "quizcluster-shard-00-01-kzjb8.azure.mongodb.net:27017," +
                  "quizcluster-shard-00-02-kzjb8.azure.mongodb.net:27017/" +
                  $"{databaseName}?ssl=true&replicaSet=QuizCluster-shard-0&authSource=admin&retryWrites=true";
            var client = new MongoClient(connectionString);
            return client.GetDatabase(name);
        }
    }
}