using System;
using System.ComponentModel.DataAnnotations;
using static System.String;

namespace Learn.WebApp.Shared.CoursePath
{
    public class CoursePathModel
    {
        [Required]
        public Guid Key { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(1000)]
        public string Name { get; set; } = Empty;

        [Required]
        [MinLength(1)]
        [MaxLength(1000)]
        public string Slug { get; set; } = Empty;

        public Guid Version { get; set; }
    }
}