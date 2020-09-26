using System;
using static System.String;

namespace Learn.WebApp.Shared.Conflict
{
    public class SlugConflictApiResponseModel
    {
        public Guid Key { get; set; }

        public string Slug { get; set; } = Empty;
    }
}