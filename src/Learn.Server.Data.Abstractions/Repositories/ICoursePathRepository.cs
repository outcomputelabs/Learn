using Learn.Server.Shared;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Learn.Server.Data.Repositories
{
    public interface ICoursePathRepository
    {
        Task<IEnumerable<CoursePath>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<CoursePath?> GetAsync(Guid key, CancellationToken cancellationToken = default);

        Task<CoursePath> AddAsync(CoursePath entity, CancellationToken cancellationToken = default);

        Task<CoursePath> UpdateAsync(CoursePath entity, CancellationToken cancellationToken = default);

        Task RemoveAsync(Guid key, Guid version, CancellationToken cancellationToken = default);
    }
}