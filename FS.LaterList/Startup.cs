using FS.LaterList.Api.REST.Controllers;
using FS.LaterList.Api.REST.Swagger;
using FS.LaterList.IoC.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

#if BLAZOR_SERVER
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
#endif

#if BLAZOR_CLIENT
using System.Linq;
using Microsoft.AspNetCore.ResponseCompression;
#endif

namespace FS.LaterList
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
#if BLAZOR_CLIENT
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
#endif

#if BLAZOR_SERVER
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped(serviceProvider => new HttpClient { BaseAddress = new Uri(serviceProvider.GetRequiredService<NavigationManager>().BaseUri) });
#endif

            services
                .AddControllers()
                .AddApplicationPart(typeof(WeatherForecastController).Assembly);

            services.RegisterSwaggerGenerator();
            services.RegisterAppServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

#if BLAZOR_CLIENT
            app.UseResponseCompression();
#endif

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
#if BLAZOR_CLIENT
                app.UseBlazorDebugging();
#endif
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.RegisterSwaggerMiddleware();
            app.RegisterSwaggerUI();

            app.UseStaticFiles();
            app.UseClientSideBlazorFiles<UI.Blazor.Startup>();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

#if BLAZOR_CLIENT
                endpoints.MapFallbackToClientSideBlazor<UI.Blazor.Startup>("index.html");
#else
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
#endif
            });
        }
    }
}
