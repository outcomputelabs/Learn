using Dapper;
using Learn.Server.Data.Exceptions;
using Learn.Server.Data.Repositories;
using Learn.Server.Shared;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Learn.Server.Data.SqlServer.Repositories
{
    /// <summary>
    /// Manages the <see cref="CoursePath"/> entity.
    /// </summary>
    internal class CoursePathRepository : ICoursePathRepository
    {
        private readonly SqlServerRepositoryOptions _options;

        public CoursePathRepository(IOptions<SqlServerRepositoryOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        private SqlConnection CreateConnection() => new SqlConnection(_options.ConnectionString);

        public async Task<IEnumerable<CoursePath>> GetAllAsync()
        {
            await using var connection = CreateConnection();

            return await connection
                .QueryAsync<CoursePath>("[dbo].[GetCoursePaths]", default, default, default, CommandType.StoredProcedure)
                .ConfigureAwait(false);
        }

        public async Task<CoursePath?> GetAsync(Guid key)
        {
            await using var connection = CreateConnection();

            return await connection
                .QuerySingleOrDefaultAsync<CoursePath>("[dbo].[GetCoursePath]", new { key }, default, default, CommandType.StoredProcedure)
                .ConfigureAwait(false);
        }

        public Task<CoursePath> SetAsync(CoursePath entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            return InnerSetAsync(entity);
        }

        private async Task<CoursePath> InnerSetAsync(CoursePath entity)
        {
            await using var connection = CreateConnection();

            try
            {
                var result = await connection
                    .QuerySingleAsync<CoursePath>("[dbo].[SetCoursePath]", entity, default, default, CommandType.StoredProcedure)
                    .ConfigureAwait(false);

                if (result is null)
                {
                    var stored = await GetAsync(entity.Key).ConfigureAwait(false);

                    throw new ConcurrencyException(stored?.Version, entity.Version);
                }

                return result;
            }
            catch (SqlException ex) when (ex.Number == SqlExceptionNumbers.UniqueKeyViolation && ex.Message.Contains("PK_CoursePath", StringComparison.Ordinal))
            {
                throw new KeyAlreadyExistsException(entity.Key);
            }
            catch (SqlException ex) when (ex.Number == SqlExceptionNumbers.UniqueKeyViolation && ex.Message.Contains("CHK_Name_Unique", StringComparison.Ordinal))
            {
                throw new NameAlreadyExistsException(entity.Name);
            }
            catch (SqlException ex) when (ex.Number == SqlExceptionNumbers.UniqueKeyViolation && ex.Message.Contains("CHK_Slug_Unique", StringComparison.Ordinal))
            {
                throw new SlugAlreadyExistsException(entity.Slug);
            }
        }

        public async Task ClearAsync(Guid key, Guid version)
        {
            await using var connection = CreateConnection();

            var result = await connection
                .QuerySingleOrDefaultAsync<CoursePath>("[dbo].[ClearCoursePath]", new { key, version }, default, default, CommandType.StoredProcedure)
                .ConfigureAwait(false);

            if (result is null)
            {
                var stored = await GetAsync(key).ConfigureAwait(false);

                throw new ConcurrencyException(stored?.Version, version);
            }
        }
    }
}