using FS.LaterList.Application.Services;
using FS.LaterList.IoC.Interfaces.Application.Services;
using FS.LaterList.IoC.Interfaces.Repository.SQLite.Repositories;
using FS.LaterList.Repository.SQLite.DbContexts;
using FS.LaterList.Repository.SQLite.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FS.LaterList.IoC.DI
{
    public static class ServiceDependencies
    {
        public static IServiceCollection RegisterAppServices(this IServiceCollection services)
            => services
                .AddDbContext<LaterListDbContext>()
                .AddScoped<ILaterListRepository, LaterListRepository>()
                .AddScoped<IInformationService, InformationService>()
                .AddScoped<ITodoListService, TodoListService>()
                .AddScoped<ITodoItemService, TodoItemService>();
    }
}
