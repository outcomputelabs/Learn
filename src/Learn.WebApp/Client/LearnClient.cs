using Learn.WebApp.Shared.CoursePath;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<CoursePathModel>> GetCoursePathsAsync()
        {
            try
            {
                return await _client.GetFromJsonAsync<IEnumerable<CoursePathModel>>("CoursePath").ConfigureAwait(false);
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
                return Enumerable.Empty<CoursePathModel>();
            }
        }

        public Task<CoursePathModel?> GetCoursePathAsync(Guid key) => _client.GetFromJsonAsync<CoursePathModel?>($"CoursePath/{key:N}");
    }
}