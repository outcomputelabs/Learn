using System;
using System.ComponentModel.DataAnnotations;

namespace Learn.WebApp.Shared
{
    public class CoursePathApiModel
    {
        public Guid? Key { get; set; }

        [Required, MaxLength(1000)]
        public string? Name { get; set; }

        [Required, MaxLength(1000)]
        public string? Slug { get; set; }

        public Guid? Version { get; set; }
    }
}