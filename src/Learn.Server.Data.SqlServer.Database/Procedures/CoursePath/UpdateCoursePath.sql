CREATE PROCEDURE [dbo].[UpdateCoursePath]
	@Key UNIQUEIDENTIFIER,
	@Name NVARCHAR(1000),
	@Slug NVARCHAR(1000),
	@Version UNIQUEIDENTIFIER
AS

SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRANSACTION

DECLARE @NewVersion UNIQUEIDENTIFIER = NEWID()

UPDATE [dbo].[CoursePath]
SET
	[Name] = @Name,
	[Slug] = @Slug,
	[Version] = @NewVersion
OUTPUT
	[inserted].[Key],
	[inserted].[Name],
	[inserted].[Slug],
	[inserted].[Version]
FROM
	[dbo].[CoursePath]
WHERE
	[Key] = @Key
	AND [Version] = @Version

IF @@ROWCOUNT = 0
BEGIN
	SELECT
		[Version]
	FROM
		[dbo].[CoursePath]
	WHERE
		[Key] = @Key
END

COMMIT TRANSACTION

RETURN 0
GO
