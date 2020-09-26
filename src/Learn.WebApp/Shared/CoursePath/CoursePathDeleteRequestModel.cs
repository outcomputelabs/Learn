using System;
using System.ComponentModel.DataAnnotations;

namespace Learn.WebApp.Shared.CoursePath
{
    public class CoursePathDeleteRequestModel
    {
        [Required]
        public Guid Key { get; set; }

        [Required]
        public Guid Version { get; set; }
    }
}