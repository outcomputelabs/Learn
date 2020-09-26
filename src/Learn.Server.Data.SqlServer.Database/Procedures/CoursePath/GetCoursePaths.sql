CREATE PROCEDURE [dbo].[GetCoursePaths]
AS

SET NOCOUNT ON;
SET XACT_ABORT ON;

SELECT
	[Key],
	[Name],
	[Slug],
	[Version]
FROM
	[dbo].[CoursePath]
;

RETURN 0;
GO