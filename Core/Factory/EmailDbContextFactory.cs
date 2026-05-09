using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Core.Factory
{
    internal class EmailDbContextFactory : IDesignTimeDbContextFactory<EmailDbContext>
    {
        public EmailDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()

                .SetBasePath(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory())))
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnectionEmailFileDb");

            var optionsBuilder = new DbContextOptionsBuilder<EmailDbContext>();
            optionsBuilder.UseSqlServer(connectionString); // Replace with your database provider

            return new EmailDbContext(optionsBuilder.Options);
        }
    }



}
