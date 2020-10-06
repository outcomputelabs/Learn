using Dapper;
using Learn.Server.Data.SqlServer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using IdentityUser = Learn.Server.Data.SqlServer.Identity.Models.IdentityUser;

namespace Learn.Server.Data.SqlServer.Identity
{
    public class UserStore : IUserStore<IdentityUser>
    {
        private readonly IdentityOptions _options;
        private readonly IdentityErrorDescriber _describer;

        public UserStore(IOptions<IdentityOptions> options, IdentityErrorDescriber describer)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _describer = describer ?? throw new ArgumentNullException(nameof(describer));
        }

        private SqlConnection CreateConnection() => new SqlConnection(_options.ConnectionString);

        /// <summary>
        /// Creates the specified <paramref name="user"/> in the user store.
        /// </summary>
        public Task<IdentityResult> CreateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            // early break if cancelled
            cancellationToken.ThrowIfCancellationRequested();

            // validate arguments
            if (user is null) throw new ArgumentNullException(nameof(user));

            return InnerCreateAsync(user, cancellationToken);
        }

        /// <summary>
        /// Inner async implementation of for <see cref="CreateAsync(Models.IdentityUser, CancellationToken)"/>.
        /// </summary>
        private async Task<IdentityResult> InnerCreateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            // attempt to create the user
            await using var connection = CreateConnection();
            try
            {
                var result = await connection
                    .QuerySingleOrDefaultAsync<IdentityUser>(new CommandDefinition("[Identity].[CreateUser]",
                        new
                        {
                            user.UserName,
                            user.NormalizedUserName,
                            user.Email,
                            user.NormalizedEmail,
                            user.EmailConfirmed,
                            user.PasswordHash,
                            user.PhoneNumber,
                            user.PhoneNumberConfirmed,
                            user.TwoFactorEnabled,
                            user.LockoutEnd,
                            user.LockoutEnabled,
                            user.AccessFailedCount
                        },
                        default, default, CommandType.StoredProcedure, CommandFlags.Buffered, cancellationToken))
                    .ConfigureAwait(false);

                // pick up the generated columns
                user.Id = result.Id;
                user.ConcurrencyStamp = result.ConcurrencyStamp;
                user.SecurityStamp = result.SecurityStamp;
                return IdentityResult.Success;
            }
            catch (SqlException ex) when (ex.Number == SqlExceptionNumbers.UniqueKeyViolation && ex.Message.Contains("PK_User", StringComparison.OrdinalIgnoreCase))
            {
                // the system attempted to insert a duplicate primary key
                return IdentityResult.Failed(_describer.ConcurrencyFailure());
            }
            catch (SqlException ex) when (ex.Number == SqlExceptionNumbers.UniqueKeyViolation && ex.Message.Contains("UK_User_NormalizedUserName", StringComparison.OrdinalIgnoreCase))
            {
                // the system attempted to insert a duplicate primary key
                return IdentityResult.Failed(_describer.DuplicateUserName(user.UserName));
            }
            catch (SqlException ex) when (ex.Number == SqlExceptionNumbers.UniqueKeyViolation && ex.Message.Contains("UK_User_NormalizedEmail", StringComparison.OrdinalIgnoreCase))
            {
                // the system attempted to insert a duplicate primary key
                return IdentityResult.Failed(_describer.DuplicateEmail(user.Email));
            }
        }

        public Task<IdentityResult> DeleteAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetUserIdAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(IdentityUser user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetUserNameAsync(IdentityUser user, string userName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Updates the specified <paramref name="user"/> in the user store.
        /// </summary>
        public Task<IdentityResult> UpdateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            // early break if cancelled
            cancellationToken.ThrowIfCancellationRequested();

            // validate arguments
            if (user is null) throw new ArgumentNullException(nameof(user));

            return InnerUpdateAsync(user, cancellationToken);
        }

        private async Task<IdentityResult> InnerUpdateAsync(IdentityUser user, CancellationToken cancellationToken)
        {

        }
    }
}