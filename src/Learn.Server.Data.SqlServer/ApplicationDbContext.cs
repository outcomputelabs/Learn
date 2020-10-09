using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace Learn.Server.Data.SqlServer
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            if (builder is null) throw new ArgumentNullException(nameof(builder));

            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().Property(x => x.Id).HasConversion(x => new Guid(x), x => x.ToString());
            builder.Entity<ApplicationUser>().Property(x => x.ConcurrencyStamp).HasConversion(x => new Guid(x), x => x.ToString());
        }
    }
}