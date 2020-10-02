using AutoMapper;
using Learn.WebApp.Shared.CoursePath;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Learn.WebApp.Client.Pages.Admin
{
    public partial class EditCoursePath
    {
        #region Dependencies

        [Inject]
        public LearnClient Client { get; set; } = null!;

        [Inject]
        public IMapper Mapper { get; set; } = null!;

        [Inject]
        public ILogger<EditCoursePath> Logger { get; set; } = null!;

        #endregion Dependencies

        #region Parameters

        [Parameter]
        public Guid? Key { get; set; }

        #endregion Parameters

        #region View Model

        public CoursePathModel? Model { get; set; }

        public bool IsValid { get; set; } = true;

        #endregion View Model

        protected override async Task OnInitializedAsync()
        {
            if (Key.HasValue)
            {
                var result = await Client.GetCoursePathAsync(Key.Value).ConfigureAwait(true);

                if (result is null)
                {
                    IsValid = false;
                }
                else
                {
                    Model = Mapper.Map<CoursePathModel>(result);
                }
            }
            else
            {
                Model = new CoursePathModel
                {
                    Key = Guid.NewGuid()
                };
            }
        }

        public void HandleValidSubmit()
        {
            Logger.LogInformation("Submitted");
        }
    }
}