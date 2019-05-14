﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ComplexityWebApi
{
#pragma warning disable CS1591
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>();
    }
#pragma warning restore CS1591
}