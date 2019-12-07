using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace FS.LaterList.UI.Blazor.Extensions
{
    public static class NavigationManagerExtensions
    {
        public static void NavigateToPage<TPage>(this NavigationManager navigationManager, params object[] parameters) where TPage : ComponentBase
            => navigationManager.NavigateTo(navigationManager.GetPageLink<TPage>(parameters));

        public static string GetPageLink<TPage>(this NavigationManager navigationManager, params object[] parameters) where TPage : ComponentBase
        {
            var routeParameterPattern = new Regex("{.*}");

            var routes = typeof(TPage)
                .GetCustomAttributes(typeof(RouteAttribute), false)
                .Cast<RouteAttribute>()
                .Select(x => new
                {
                    x.Template,
                    ParameterCount = routeParameterPattern.Matches(x.Template).Count
                })
                .ToList();

            if (!routes.Any())
                throw new InvalidOperationException($"The component of type '{typeof(TPage).Name}' has no routes defined.");

            var routeTemplate = routes.FirstOrDefault(x => x.ParameterCount == parameters.Length)?.Template;
            if (routeTemplate == null)
                throw new InvalidOperationException($"No route template for '{typeof(TPage).Name}' found matching the given parameters count.");

            foreach (var parameter in parameters)
                routeTemplate = routeParameterPattern.Replace(routeTemplate, ParameterToString(parameter), 1);

            var route = navigationManager.BaseUri + routeTemplate.Substring(1);
            return route;
        }

        private static string ParameterToString(object parameter)
        {
            if (parameter is DateTime dateTime)
                return dateTime.ToString("o");
            return parameter?.ToString() ?? string.Empty;
        }
    }
}
