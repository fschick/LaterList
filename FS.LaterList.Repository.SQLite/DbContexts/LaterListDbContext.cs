using FS.LaterList.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace FS.LaterList.Repository.SQLite.DbContexts
{
    public class LaterListDbContext : DbContext
    {
        private static readonly ILoggerFactory _loggerFactory =
            LoggerFactory
            .Create(builder => builder
                .AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
                .AddNLog()
            );

        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options
                .UseLoggerFactory(_loggerFactory)
                .UseSqlite("Data Source=FS.LaterList.sqlite");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoList>()
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<TodoItem>()
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
