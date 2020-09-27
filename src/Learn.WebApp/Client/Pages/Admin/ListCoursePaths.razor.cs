using Learn.WebApp.Shared.CoursePath;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learn.WebApp.Client.Pages.Admin
{
    public partial class ListCoursePaths
    {
        private IEnumerable<CoursePathResponseModel> _models = null!;

        protected override async Task OnInitializedAsync()
        {
            _models = await Client.GetCoursePathsAsync().ConfigureAwait(true);

            await base.OnInitializedAsync().ConfigureAwait(true);
        }
    }
}