using Learn.Server.Data.Repositories;
using Learn.Server.Data.SqlServer;
using Learn.Server.Data.SqlServer.Repositories;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SqlServerRepositoryDependencyInjectionExtensions
    {
        public static IServiceCollection AddSqlServerRepository(this IServiceCollection services, Action<SqlServerRepositoryOptions> configure)
        {
            return services
                .AddSingleton<ICoursePathRepository, CoursePathRepository>()
                .AddOptions<SqlServerRepositoryOptions>()
                .Configure(configure)
                .ValidateDataAnnotations()
                .Services;
        }
    }
}