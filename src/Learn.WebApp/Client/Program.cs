using AutoMapper;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Learn.WebApp.Client
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            // add spa authentication support
            builder.Services.AddApiAuthorization();

            // add the named http client that adds access token to requests
            builder.Services
                .AddHttpClient<LearnClient>(client =>
                {
                    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
                })
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // add auto mapper services
            builder.Services.AddAutoMapper(options =>
            {
                options.AddProfile<AutoMapperProfile>();
            });

            await builder.Build().RunAsync().ConfigureAwait(false);
        }
    }
}