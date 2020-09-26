namespace Learn.WebApp.Shared.Conflict
{
    public class ConflictApiResponseModel
    {
        public VersionConflictApiResponseModel? VersionConflict { get; set; }

        public NameConflictApiResponseModel? NameConflict { get; set; }

        public SlugConflictApiResponseModel? SlugConflict { get; set; }
    }
}