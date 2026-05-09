using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Core;
using Core.Core.Enums;
using Core.Db;
using Core.Email;
using Core.FileStorage;
using Core.Interfaces;
using Core.Repository;
using Core.Services;
using Core.Sms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Core.Configuration
{

    public static class AppJobServiceRunner
    {
        public static IServiceCollection RegisterAppJobServicesApp(this IServiceCollection services, IConfiguration configuration)
        {
            // Build configuration
            configuration = new ConfigurationBuilder()
               .SetBasePath(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory())))      //,"..", "AppJob.Core"
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .Build();
            if (!configuration.GetSection("FileStorage").Exists() || !configuration.GetSection("Email").Exists())
            {
                throw new InvalidOperationException("Missing required configuration sections: FileStorage and/or Email");
            }

            // Register AppJob.Core services
            services.AddStorageEmailServices(configuration);

            return services;
        }
    }

    public static class AppJobServiceCollectionExtensions
    {
        public static IServiceCollection AddStorageEmailServices(this IServiceCollection services, IConfiguration configuration)
        {

            //configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory() + "../../../APPJob.Provider/AppJob")
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    .Build();


            services.AddDbContext<EmailDbContext>(options =>
                  options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionEmailFileDb")));

            // Configure File Storage
            services.Configure<FileStorageOptions>(options =>
                configuration.GetSection("FileStorage").Bind(options));
            services.AddSingleton<LocalDiskStorageService>();
            services.AddSingleton<AzureBlobStorageService>();
            services.AddScoped<IFileStorageService>(provider =>
            {
                var options = provider.GetRequiredService<IOptions<FileStorageOptions>>().Value;
                return options.DefaultStorageLocation switch
                {
                    StorageLocation.LocalDisk => provider.GetRequiredService<LocalDiskStorageService>(),
                    StorageLocation.AzureBlob => provider.GetRequiredService<AzureBlobStorageService>(),
                    _ => throw new ArgumentException($"Unsupported default storage location: {options.DefaultStorageLocation}"),
                };
            });

            // Configure Email Sending
            services.Configure<EmailOptions>(opt => configuration.GetSection("Email").Bind(opt));
            services.AddScoped<SmtpEmailService>();
            services.AddScoped<IEmailService>(provider => provider.GetRequiredService<SmtpEmailService>());

            // Configure Orchestrator
            services.AddScoped<CommunicationOrchestrator>();
            services.AddScoped<ICommunicationOrchestrator>(provider => provider.GetRequiredService<CommunicationOrchestrator>());
            services.AddHostedService<BackgroundEmailSender>();

            // Configure SMS and Link Services
            services.AddHttpClient<SmsService>();
            services.AddScoped<ISmsService, SmsService>();
            services.AddScoped<ILinkGenerator, LinkGeneratorService>();
            services.AddScoped<ILinkRepository, LinkRepository>();
            services.AddScoped<LinkService>(); // Register concrete LinkService
            services.AddScoped<ILinkService, LinkService>();


            return services;
        }
    }

}
