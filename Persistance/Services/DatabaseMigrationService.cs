using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistance.DatabaseContext.WriteDbContext;

namespace Persistance.Services
{
    public class DatabaseMigrationService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DatabaseMigrationService> _logger;

        public DatabaseMigrationService(IServiceProvider serviceProvider, ILogger<DatabaseMigrationService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Database Migration Service started.");
            await ApplyPendingMigrations();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Database Migration Service stopped.");
            return Task.CompletedTask;
        }

        private async Task ApplyPendingMigrations()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<WriteDbContext>();

                if (context.Database.IsSqlServer())
                {
                    var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

                    if (pendingMigrations.Any())
                    {
                        _logger.LogInformation("Found {Count} pending migrations.", pendingMigrations.Count());
                        try
                        {
                            await context.Database.MigrateAsync();
                            _logger.LogInformation("Successfully applied all pending migrations.");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "An error occurred while applying pending migrations.");
                            // در صورت بروز خطا، می‌توانید منطق سفارشی برای رسیدگی به آن اضافه کنید
                        }
                    }
                    else
                    {
                        _logger.LogInformation("No pending migrations found.");
                    }
                }
                else
                {
                    _logger.LogInformation("Database is not SQL Server. Skipping migrations.");
                }
            }
        }
    }

}
