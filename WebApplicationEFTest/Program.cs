using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApplicationEFTest
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = new ModelBuilder(new Microsoft.EntityFrameworkCore.Metadata.Conventions.ConventionSet());
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();

            builder.GenerateForeignKey(new Entity.TestDBContext(optionsBuilder.UseSqlServer("Server=.;Database=WebApplicationEFTest;Trusted_Connection=True;").Options));


            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
