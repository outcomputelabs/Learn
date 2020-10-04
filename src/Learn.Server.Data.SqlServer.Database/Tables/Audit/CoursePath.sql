CREATE TABLE [Audit].[CoursePath]
(
	[AuditKey] INT NOT NULL,
	[AuditTypeKey] INT NOT NULL,
	[AuditDate] DATETIMEOFFSET NOT NULL,

	[Key] UNIQUEIDENTIFIER NOT NULL,
	[Name] NVARCHAR(1000) NOT NULL,
	[Slug] NVARCHAR(1000) NOT NULL,
	[Version] UNIQUEIDENTIFIER NOT NULL,

	CONSTRAINT [PK_Audit_CoursePath] PRIMARY KEY CLUSTERED
	(
		[AuditKey]
	),

	CONSTRAINT [FK_AuditType] FOREIGN KEY
	(
		[AuditTypeKey]
	)
	REFERENCES [Audit].[AuditType]
	(
		[Key]
	)
)
GO

CREATE UNIQUE NONCLUSTERED INDEX [NCI_Audit_CoursePath]
ON [Audit].[CoursePath]
(
	[AuditKey]
)
GO

CREATE SEQUENCE [Audit].[CoursePathAuditSequence]
AS INT
START WITH 1
INCREMENT BY 1
GO
