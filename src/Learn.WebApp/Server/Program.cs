using AutoMapper;
using Learn.Server.Data;
using Learn.Server.Data.SqlServer;
using Learn.WebApp.Server.Middleware;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System.Threading.Tasks;

namespace Learn.WebApp.Server
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();

            await host.RunAsync().ConfigureAwait(false);
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureLogging((context, logging) =>
                    {
                        var logger = new LoggerConfiguration()
                            .WriteTo.Console(context.HostingEnvironment.IsDevelopment() ? LogEventLevel.Information : LogEventLevel.Warning)
                            .CreateLogger();

                        logging.ClearProviders();
                        logging.AddSerilog(logger, true);
                    });

                    webBuilder.ConfigureServices((context, services) =>
                    {
                        // add web api services
                        services.AddControllersWithViews();
                        services.AddRazorPages();
                        services.AddSingleton<ActivityMiddleware>();
                        services.Configure<JsonOptions>(options =>
                        {
                            options.JsonSerializerOptions.IgnoreNullValues = true;
                        });

                        // add swagger services
                        services.AddSwaggerGen();

                        // add orleans cluster services
                        services.AddLearnClusterClient(options => context.Configuration.Bind("LearnClusterClient", options));

                        // add auto mapper services
                        services.AddAutoMapper(options =>
                        {
                            options.AddProfile<AutoMapperProfile>();
                        });

                        // add repositories
                        services.AddSqlServerRepository(options =>
                        {
                            options.ConnectionString = context.Configuration.GetConnectionString("Learn");
                        });

                        // add entity framework services
                        services.AddDatabaseDeveloperPageExceptionFilter();

                        // add identity services
                        /*
                        services
                            .AddDefaultIdentity<ApplicationUser>(options =>
                            {
                                options.SignIn.RequireConfirmedAccount = true;
                            })
                            .AddEntityFrameworkStores<ApplicationDbContext>();
                        */

                        services
                            .AddIdentityServer()
                            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

                        services
                            .AddAuthentication()
                            .AddIdentityServerJwt();
                    });

                    webBuilder.Configure((context, app) =>
                    {
                        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

                        if (env.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
                            app.UseMigrationsEndPoint();
                            app.UseWebAssemblyDebugging();
                        }
                        else
                        {
                            app.UseExceptionHandler("/Error");

                            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                            app.UseHsts();
                        }

                        app.UseHttpsRedirection();
                        app.UseBlazorFrameworkFiles();
                        app.UseStaticFiles();

                        app.UseRouting();

                        app.UseIdentityServer();
                        app.UseAuthentication();
                        app.UseAuthorization();

                        app.UseSwagger();
                        app.UseSwaggerUI(options =>
                        {
                            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Learn V1");
                        });

                        app.UseMiddleware<ActivityMiddleware>();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapRazorPages();
                            endpoints.MapControllers();
                            endpoints.MapFallbackToFile("index.html");
                        });
                    });

                    webBuilder.SuppressStatusMessages(true);
                });
        }
    }
}