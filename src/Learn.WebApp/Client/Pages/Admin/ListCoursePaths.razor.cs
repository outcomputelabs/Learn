using Learn.WebApp.Shared.CoursePath;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learn.WebApp.Client.Pages.Admin
{
    public partial class ListCoursePaths
    {
        private IEnumerable<CoursePathModel> _models = null!;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                _models = await Client.GetCoursePathsAsync().ConfigureAwait(true);
            }
            catch (NotSupportedException)
            {
                // todo: show friendly error or toast
            }

            await base.OnInitializedAsync().ConfigureAwait(true);
        }
    }
}