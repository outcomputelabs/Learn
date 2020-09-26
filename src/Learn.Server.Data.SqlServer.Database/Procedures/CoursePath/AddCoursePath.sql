CREATE PROCEDURE [dbo].[AddCoursePath]
	@Key UNIQUEIDENTIFIER,
	@Name NVARCHAR(1000),
	@Slug NVARCHAR(1000)
AS

SET NOCOUNT ON
SET XACT_ABORT ON

DECLARE @Version UNIQUEIDENTIFIER = NEWID()

INSERT [dbo].[CoursePath]
(
	[Key],
	[Name],
	[Slug],
	[Version]
)
OUTPUT
	[inserted].[Key],
	[inserted].[Name],
	[inserted].[Slug],
	[inserted].[Version]
VALUES
(
	@Key,
	@Name,
	@Slug,
	@Version
)

RETURN 0
GO
