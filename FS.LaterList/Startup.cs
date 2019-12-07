using System;
using System.Net.Http;
using FS.LaterList.Api.REST.Controllers;
using FS.LaterList.Api.REST.Swagger;
using FS.LaterList.IoC.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
#if BLAZOR_SERVER
            app.Map("/server", appBuilder => Configure(appBuilder, env, false));
#endif
#if BLAZOR_CLIENT
            app.Map("/client", appBuilder => Configure(appBuilder, env, true));
#endif

            app.RegisterSwaggerMiddleware();
            app.RegisterSwaggerUI();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(@"
                    <!DOCTYPE html>
                    <body style=""text-align: center; padding-top: 5rem;"">
                        <a href=""swagger/"" style=""display: block; color: rgb(64,64,64); font-family: sans-serif; margin-bottom: 0.5rem;"">Swagger UI</a>
                        <a href=""client/"" style=""display: block; color: rgb(64,64,64); font-family: sans-serif; margin-bottom: 0.5rem;"">Client side rendered Blazor</a>
                        <a href=""server/"" style=""display: block; color: rgb(64,64,64); font-family: sans-serif; margin-bottom: 0.5rem;"">Server side rendered Blazor</a>
                    </body>
                    </html>
                ");
            });
        }

        private void Configure(IApplicationBuilder app, IWebHostEnvironment env, bool clientSideBlazor)
        {
            if (clientSideBlazor)
                app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                if (clientSideBlazor)
                    app.UseBlazorDebugging();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseClientSideBlazorFiles<UI.Blazor.Startup>();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                if (clientSideBlazor)
                    endpoints.MapFallbackToClientSideBlazor<UI.Blazor.Startup>("index.html");
                else
                {
                    endpoints.MapBlazorHub();
                    endpoints.MapFallbackToPage("/_Host");
                }
            });
        }
    }
}
