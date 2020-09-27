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

        public async Task<IEnumerable<CoursePathResponseModel>> GetCoursePathsAsync()
        {
            try
            {
                return await _client
                    .GetFromJsonAsync<IEnumerable<CoursePathResponseModel>>("CoursePath")
                    .ConfigureAwait(false);
            }
            catch (NotSupportedException ex)
            {
                /* todo */
                throw;
            }
        }
    }
}