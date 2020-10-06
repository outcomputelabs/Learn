CREATE PROCEDURE [dbo].[ClearCoursePath]
	@Key UNIQUEIDENTIFIER,
	@Version UNIQUEIDENTIFIER
AS

SET NOCOUNT ON
SET XACT_ABORT ON

DECLARE @AuditId INT = NEXT VALUE FOR [Audit].[CoursePathAuditSequence];
DECLARE @AuditTypeId INT = (SELECT [Id] FROM [Audit].[AuditType] WHERE [Name] = 'DELETED');
DECLARE @AuditTimestamp DATETIMEOFFSET = SYSDATETIMEOFFSET();

DELETE [dbo].[CoursePath]
OUTPUT
	@AuditId,
	@AuditTypeId,
	@AuditTimestamp,

	[deleted].[Key],
	[deleted].[Name],
	[deleted].[Slug],
	[deleted].[Version]
INTO
	[Audit].[CoursePath]
	(
		[AuditId],
		[AuditTypeId],
		[AuditTimestamp],

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