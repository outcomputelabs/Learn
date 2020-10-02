CREATE PROCEDURE [dbo].[SetCoursePath]
	@Key UNIQUEIDENTIFIER,
	@Name NVARCHAR(1000),
	@Slug NVARCHAR(1000),
	@Version UNIQUEIDENTIFIER
AS

SET NOCOUNT ON
SET XACT_ABORT ON

DECLARE @NewVersion UNIQUEIDENTIFIER = NEWID();

WITH [Source] AS
(
	SELECT
		@Key AS [Key],
		@Name AS [Name],
		@Slug AS [Slug],
		@Version AS [CurrentVersion],
		@NewVersion AS [NewVersion]
)
MERGE [dbo].[CoursePath] AS [Target]
USING [Source]
ON [Target].[Key] = [Source].[Key]
WHEN NOT MATCHED BY TARGET THEN
INSERT
(
	[Key],
	[Name],
	[Slug],
	[Version]
)
VALUES
(
	[Source].[Key],
	[Source].[Name],
	[Source].[Slug],
	[Source].[NewVersion]
)
WHEN MATCHED AND [Target].[Version] = [Source].[CurrentVersion] THEN
UPDATE SET
	[Name] = [Source].[Name],
	[Slug] = [Source].[Slug],
	[Version] = [Source].[NewVersion]
OUTPUT
	[inserted].[Key],
	[inserted].[Name],
	[inserted].[Slug],
	[inserted].[Version]
;

RETURN 0
GO