using Learn.Server.Data.Repositories;
using Learn.Server.Data.SqlServer;
using Learn.Server.Data.SqlServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SqlServerRepositoryDependencyInjectionExtensions
    {
        public static IServiceCollection AddSqlServerRepository(this IServiceCollection services, Action<SqlServerRepositoryOptions> configure)
        {
            return services
                .AddDbContext<ApplicationDbContext>((provider, options) =>
                {
                    options.UseSqlServer(provider.GetRequiredService<IOptions<SqlServerRepositoryOptions>>().Value.ConnectionString);
                })
                .AddSingleton<ICoursePathRepository, CoursePathRepository>()
                .AddOptions<SqlServerRepositoryOptions>()
                .Configure(configure)
                .ValidateDataAnnotations()
                .Services;
        }
    }
}