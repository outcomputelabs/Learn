using Learn.Server.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learn.Server.Data.Repositories
{
    public interface ICoursePathRepository
    {
        Task<IEnumerable<CoursePath>> GetAllAsync();

        Task<CoursePath?> GetAsync(Guid key);

        Task<CoursePath> SetAsync(CoursePath entity);

        Task ClearAsync(Guid key, Guid version);
    }
}