CREATE PROCEDURE [dbo].[ClearCoursePath]
	@Key UNIQUEIDENTIFIER,
	@Version UNIQUEIDENTIFIER
AS

SET NOCOUNT ON
SET XACT_ABORT ON

DECLARE @AuditKey INT = NEXT VALUE FOR [Audit].[CoursePathAuditSequence];
DECLARE @AuditTypeKey INT = (SELECT [Key] FROM [Audit].[AuditType] WHERE [Name] = 'DELETED');
DECLARE @AuditDate DATETIMEOFFSET = SYSDATETIMEOFFSET();

DELETE [dbo].[CoursePath]
OUTPUT
	@AuditKey,
	@AuditTypeKey,
	@AuditDate,
	[deleted].[Key],
	[deleted].[Name],
	[deleted].[Slug],
	[deleted].[Version]
INTO
	[Audit].[CoursePath]
	(
		[AuditKey],
		[AuditTypeKey],
		[AuditDate],
		[Key],
		[Name],
		[Slug],
		[Version]
	)
WHERE
	[Key] = @Key
	AND [Version] = @Version

SELECT
	[Version]
FROM
	[dbo].[CoursePath]
WHERE
	[Key] = @Key

RETURN 0
GO