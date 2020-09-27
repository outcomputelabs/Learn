using System;

namespace Learn.WebApp.Shared.CoursePath
{
    public class CoursePathResponseModel
    {
        public Guid? Key { get; set; }

        public string? Name { get; set; }

        public string? Slug { get; set; }

        public Guid? Version { get; set; }
    }
}