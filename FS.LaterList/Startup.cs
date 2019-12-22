using System;
using System.Net.Http;
using System.Threading.Tasks;
using FS.LaterList.Api.REST.Controllers;
using FS.LaterList.Api.REST.Swagger;
using FS.LaterList.Common.Extensions;
using FS.LaterList.IoC.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
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
                .AddApplicationPart(typeof(TodoListController).Assembly);

            services.RegisterSwaggerGenerator();
            services.RegisterAppServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            DatabaseMigration.MigrateToLatest(app);

#if BLAZOR_SERVER
            app.Map("/blazorserver", appBuilder => ConfigureBlazor(appBuilder, env, false));
#endif
#if BLAZOR_CLIENT
            app.Map("/blazorclient", appBuilder => ConfigureBlazor(appBuilder, env, true));
#endif

            app.RegisterSwaggerMiddleware();
            app.RegisterSwaggerUI();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            app.UseHttpsRedirection();
            UseLaterListStaticFiles(app, env);
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapGet("/index.html", r =>
                {
                    r.Response.Redirect("/");
                    return Task.CompletedTask;
                });
                endpoints.MapFallbackToPage("/Project");
                endpoints.MapControllers();
            });
        }

        private static void ConfigureBlazor(IApplicationBuilder app, IWebHostEnvironment env, bool clientSideBlazor)
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

            app.UseHttpsRedirection();
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
                    endpoints.MapFallbackToPage("/Index");
                }
            });
        }

        private static void UseLaterListStaticFiles(IApplicationBuilder app, IHostEnvironment env)
        {
            var webRootPath = env.GetWebRootPath();
            var fileProvider = webRootPath != null
                ? (IFileProvider)new PhysicalFileProvider(webRootPath)
                : new NullFileProvider();

            var staticFileOptions = new StaticFileOptions { FileProvider = fileProvider };
            app.UseStaticFiles(staticFileOptions);
        }
    }
}
