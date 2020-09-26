using Dapper;
using Learn.Server.Data.Exceptions;
using Learn.Server.Data.Repositories;
using Learn.Server.Data.SqlServer.Models;
using Learn.Server.Shared;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Learn.Server.Data.SqlServer.Repositories
{
    internal class CoursePathRepository : ICoursePathRepository
    {
        private readonly SqlServerRepositoryOptions _options;

        public CoursePathRepository(IOptions<SqlServerRepositoryOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<IEnumerable<CoursePath>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            await using var connection = new SqlConnection(_options.ConnectionString);

            return await connection
                .QueryAsync<CoursePath>(new CommandDefinition("[dbo].[GetCoursePaths]", default, default, default, CommandType.StoredProcedure, CommandFlags.Buffered, cancellationToken))
                .ConfigureAwait(false);
        }

        public async Task<CoursePath?> GetAsync(Guid key, CancellationToken cancellationToken = default)
        {
            await using var connection = new SqlConnection(_options.ConnectionString);

            var parameters = new
            {
                key
            };

            return await connection
                .QuerySingleOrDefaultAsync<CoursePath>(new CommandDefinition("[dbo].[GetCoursePath]", parameters, default, default, CommandType.StoredProcedure, CommandFlags.Buffered, cancellationToken))
                .ConfigureAwait(false);
        }

        public Task<CoursePath> AddAsync(CoursePath entity, CancellationToken cancellationToken = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            return InnerAddAsync(entity, cancellationToken);
        }

        private async Task<CoursePath> InnerAddAsync(CoursePath entity, CancellationToken cancellationToken)
        {
            await using var connection = new SqlConnection(_options.ConnectionString);

            var parameters = new
            {
                entity.Key,
                entity.Name,
                entity.Slug
            };

            try
            {
                return await connection
                    .QuerySingleAsync<CoursePath>(new CommandDefinition("[dbo].[AddCoursePath]", parameters, default, default, CommandType.StoredProcedure, CommandFlags.Buffered, cancellationToken))
                    .ConfigureAwait(false);
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

        public async Task RemoveAsync(Guid key, Guid version, CancellationToken cancellationToken = default)
        {
            await using var connection = new SqlConnection(_options.ConnectionString);

            var parameters = new
            {
                key,
                version
            };

            var conflict = await connection
                .QuerySingleOrDefaultAsync<Conflict>(new CommandDefinition("[dbo].[RemoveCoursePath]", parameters, default, default, CommandType.StoredProcedure, CommandFlags.Buffered, cancellationToken))
                .ConfigureAwait(false);

            if (conflict != null)
            {
                throw new ConcurrencyException(conflict.Version, version);
            }
        }

        public Task<CoursePath> UpdateAsync(CoursePath entity, CancellationToken cancellationToken = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            return InnerUpdateAsync(entity, cancellationToken);
        }

        private async Task<CoursePath> InnerUpdateAsync(CoursePath entity, CancellationToken cancellationToken)
        {
            await using var connection = new SqlConnection(_options.ConnectionString);

            try
            {
                var grid = await connection
                    .QueryMultipleAsync(new CommandDefinition("[dbo].[UpdateCoursePath]", entity, default, default, CommandType.StoredProcedure, CommandFlags.Buffered, cancellationToken))
                    .ConfigureAwait(false);

                var result = await grid.ReadSingleOrDefaultAsync<CoursePath>().ConfigureAwait(false);
                if (result != null)
                {
                    return result;
                }

                var conflict = await grid.ReadSingleOrDefaultAsync<Conflict>().ConfigureAwait(false);
                throw new ConcurrencyException(conflict?.Version, entity.Version);
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
    }
}