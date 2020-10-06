CREATE TABLE [Audit].[CoursePath]
(
	[AuditId] INT NOT NULL,
	[AuditTypeId] INT NOT NULL,
	[AuditTimestamp] DATETIMEOFFSET NOT NULL,

	[Key] UNIQUEIDENTIFIER NOT NULL,
	[Name] NVARCHAR(1000) NOT NULL,
	[Slug] NVARCHAR(1000) NOT NULL,
	[Version] UNIQUEIDENTIFIER NOT NULL,

	CONSTRAINT [PK_Audit_CoursePath] PRIMARY KEY CLUSTERED
	(
		[AuditId]
	),

	CONSTRAINT [FK_AuditType] FOREIGN KEY
	(
		[AuditTypeId]
	)
	REFERENCES [Audit].[AuditType]
	(
		[Id]
	)
)
GO

CREATE UNIQUE NONCLUSTERED INDEX [NCI_Audit_CoursePath]
ON [Audit].[CoursePath]
(
	[AuditId]
)
GO

CREATE SEQUENCE [Audit].[CoursePathAuditSequence]
AS INT
START WITH 1
INCREMENT BY 1
GO
