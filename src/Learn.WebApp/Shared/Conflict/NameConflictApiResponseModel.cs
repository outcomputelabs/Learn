using System;
using static System.String;

namespace Learn.WebApp.Shared.Conflict
{
    public class NameConflictApiResponseModel
    {
        public Guid Key { get; set; }

        public string Name { get; set; } = Empty;
    }
}