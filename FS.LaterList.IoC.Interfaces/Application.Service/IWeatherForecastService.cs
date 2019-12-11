using FS.LaterList.Common.Models;
using System;
using System.Threading.Tasks;

namespace FS.LaterList.IoC.Interfaces.Application.Service
{
    public interface IWeatherForecastService
    {
        Task<WeatherForecast[]> GetForecastAsync(DateTime startDate);
    }
}