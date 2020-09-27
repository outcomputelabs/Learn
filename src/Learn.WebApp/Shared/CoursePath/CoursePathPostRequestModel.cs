using System;
using System.ComponentModel.DataAnnotations;

namespace Learn.WebApp.Shared.CoursePath
{
    public class CoursePathPostRequestModel
    {
        public Guid? Key { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Slug { get; set; }

        public Guid? Version { get; set; }
    }
}