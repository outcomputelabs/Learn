using System;

namespace Learn.WebApp.Shared.Conflict
{
    public class VersionConflictApiResponseModel
    {
        public Guid? StoredVersion { get; set; }

        public Guid? CurrentVersion { get; set; }
    }
}