using System;
using System.Threading.Tasks;
using FS.LaterList.Common.Models;

namespace FS.LaterList.IoC.Interfaces.App.Service
{
    public interface IWeatherForecastService
    {
        Task<WeatherForecast[]> GetForecastAsync(DateTime startDate);
    }
}