using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Learn.WebApp.Server.Areas.Identity.IdentityHostingStartup))]

namespace Learn.WebApp.Server.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}