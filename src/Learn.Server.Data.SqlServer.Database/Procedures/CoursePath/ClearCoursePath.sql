CREATE PROCEDURE [dbo].[ClearCoursePath]
	@Key UNIQUEIDENTIFIER,
	@Version UNIQUEIDENTIFIER
AS

SET NOCOUNT ON
SET XACT_ABORT ON

DELETE [dbo].[CoursePath]
OUTPUT
	[deleted].[Key],
	[deleted].[Name],
	[deleted].[Slug],
	[deleted].[Version]
WHERE
	[Key] = @Key
	AND [Version] = @Version

RETURN 0
GO