using Infrastructure.Logger;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace QuizWebHookBot
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().ConfigureLogging(b=>b.AddTelegram());
    }
}
