using System;
using System.IO;
using System.Reflection;
using Application.Info;
using Application.QuizService;
using Application.Repositories;
using Application.Selectors;
using Application.TaskService;
using AutoMapper;
using ComplexityWebApi.DTO;
using DataBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using static System.Environment;

namespace ComplexityWebApi
{
    public class Startup
    {
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

            services.AddTransient(_ => new Random(Guid.NewGuid().GetHashCode()));

            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<ITaskService, TaskService>();

            services.AddScoped<IUserRepository, MongoUserRepository>();
            services.AddScoped<ITaskRepository, MongoTaskRepository>();

            var databaseName = GetEnvironmentVariable(MongoDatabaseNameEnvironmentVariable) ?? "ComplexityBot";
            var username = GetEnvironmentVariable(MongoUsernameEnvironmentVariable) ?? "romutchio";
            var password = GetEnvironmentVariable(MongoPasswordEnvironmentVariable) ?? "romaha434";
            services.AddSingleton(MongoDatabaseInitializer.CreateMongoDatabase(databaseName, username, password));

            services.AddTransient<RandomTaskGeneratorSelector>();
            services.AddTransient<ITaskGeneratorSelector, ProgressTaskGeneratorSelector>(p =>
                new ProgressTaskGeneratorSelector(p.GetService<Random>(), p.GetService<RandomTaskGeneratorSelector>()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Version = "v1", Title = "Complexity bot", Description = "Web API of Complexity bot"
                    });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TaskInfo, TaskInfoDTO>();
                cfg.CreateMap<LevelInfo, LevelInfoDTO>();
                cfg.CreateMap<TopicInfo, TopicInfoDTO>();
                cfg.CreateMap<HintInfo, HintInfoDTO>();
                cfg.CreateMap<LevelProgressInfo, LevelProgressInfoDTO>();
            });

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}