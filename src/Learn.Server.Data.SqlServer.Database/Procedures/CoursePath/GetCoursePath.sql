CREATE PROCEDURE [dbo].[GetCoursePath]
	@Key UNIQUEIDENTIFIER
AS

SET NOCOUNT ON
SET XACT_ABORT ON

SELECT
	[Key],
	[Name],
	[Slug],
	[Version]
FROM
	[dbo].[CoursePath]
WHERE
	[Key] = @Key

RETURN 0
GO
