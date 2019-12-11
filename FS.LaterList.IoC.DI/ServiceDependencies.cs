using FS.LaterList.Application.Services;
using FS.LaterList.IoC.Interfaces.Application.Service;
using Microsoft.Extensions.DependencyInjection;

namespace FS.LaterList.IoC.DI
{
    public static class ServiceDependencies
    {
        public static IServiceCollection RegisterAppServices(this IServiceCollection services)
        {
            services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
            return services;
        }
    }
}
