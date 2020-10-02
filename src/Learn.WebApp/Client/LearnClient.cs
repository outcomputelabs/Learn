using Learn.WebApp.Shared.CoursePath;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Learn.WebApp.Client
{
    public class LearnClient
    {
        private readonly HttpClient _client;

        public LearnClient(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public Task<IEnumerable<CoursePathModel>> GetCoursePathsAsync() => _client.GetFromJsonAsync<IEnumerable<CoursePathModel>>("CoursePath");

        public Task<CoursePathModel?> GetCoursePathAsync(Guid key) => _client.GetFromJsonAsync<CoursePathModel?>($"CoursePath/{key:N}");
    }
}