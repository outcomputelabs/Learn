using Learn.Server.Grains;
using Learn.Server.Shared;
using Orleans;
using System;
using System.Threading.Tasks;

namespace Learn.Server.Grains
{
    public interface ICoursePathGrain : IGrainWithGuidKey
    {
        Task<CoursePath?> GetAsync();

        Task<CoursePath> SetAsync(CoursePath entity);

        Task ClearAsync(Guid version);
    }
}

namespace Orleans
{
    public static class CoursePathGrainFactoryExtensions
    {
        public static ICoursePathGrain GetCoursePathGrain(this IGrainFactory factory, Guid key)
        {
            if (factory is null) throw new ArgumentNullException(nameof(factory));

            return factory.GetGrain<ICoursePathGrain>(key);
        }
    }
}