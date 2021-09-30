using FlynnNotesBlog.Data;
using FlynnNotesBlog.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlynnNotesBlog
{
    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    CreateHostBuilder(args).Build().Run();
        //}

        public static async Task Main(string[] args)
        {
            
            var host = CreateHostBuilder(args).Build();

            //Pull out my registered DataService
            var dataService = host.Services
                                  .CreateScope().ServiceProvider
                                  .GetRequiredService<DataService>();

            await dataService.ManageDataAsync();

            var dbContext = host.Services
                                .CreateScope().ServiceProvider
                                .GetRequiredService<ApplicationDbContext>();

            await dbContext.Database.MigrateAsync();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
