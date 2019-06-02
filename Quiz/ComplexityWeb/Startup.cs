using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Application.Info;
using Application.QuizService;
using Application.Repositories;
using Application.Selectors;
using Application.TaskService;
using AutoMapper;
using DataBase;
using Domain.Entities.TaskGenerators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizWebApp.Services.QuizService.DTO;
using QuizWebApp.Services.TaskService;
using QuizWebApp.Services.TaskService.DTO;
using Swashbuckle.AspNetCore.Swagger;

namespace QuizWebApp
{
    public class Startup
    {
        public const string MongoUsernameEnvironmentVariable = "MONGO_USERNAME";
        public const string MongoPasswordEnvironmentVariable = "MONGO_PASSWORD";
        public const string MongoDatabaseNameEnvironmentVariable = "MONGO_DB_NAME";
        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddMvc(o => o.InputFormatters.Insert(0, new RawRequestBodyFormatter())).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient(_ => new Random(Guid.NewGuid().GetHashCode()));

            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<ITaskService, TaskService>();

            services.AddScoped<IUserRepository, MongoUserRepository>();
            services.AddScoped<ITaskRepository, MongoTaskRepository>();

            var databaseName = Environment.GetEnvironmentVariable(MongoDatabaseNameEnvironmentVariable);
            var username = Environment.GetEnvironmentVariable(MongoUsernameEnvironmentVariable);
            var password = Environment.GetEnvironmentVariable(MongoPasswordEnvironmentVariable);
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
            app.Use((context, next) =>
            {
                if (context.Request.Headers.Any(k => k.Key.Contains("Origin")) && context.Request.Method == "OPTIONS")
                {
                    context.Response.StatusCode = 200;
                    return context.Response.WriteAsync("handled");
                }

                return next.Invoke();
            });
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
                cfg.CreateMap<TemplateTaskGenerator, AdminTemplateGeneratorDTO>();
            });

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "swagger";
            });

            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}