using FS.LaterList.Repository.SQLite.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace FS.LaterList.IoC.DI
{
    public class DatabaseMigration
    {
        public static void MigrateToLatest(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            using var dbContext = serviceScope.ServiceProvider.GetRequiredService<LaterListDbContext>();
            var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<DatabaseMigration>>();
            var pendingMigrations = dbContext.Database.GetPendingMigrations().ToList();
            if (pendingMigrations.Count == 0)
                return;

            logger.LogInformation("Applying migrations to database. Please be patient ...");
            foreach (var pendingMigration in pendingMigrations)
                logger.LogInformation(pendingMigration);
            dbContext.Database.Migrate();
            logger.LogInformation("Database migration finished.");
        }
    }
}
