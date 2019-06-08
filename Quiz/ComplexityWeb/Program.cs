using Infrastructure.Logger;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace QuizWebApp
{

    public static class Program
    {
        public static void Main(string[] args) =>
            CreateWebHostBuilder(args)
                .Build()
                .Run();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).ConfigureLogging(b => b.AddTelegram())
                .UseStartup<Startup>();
    }

}