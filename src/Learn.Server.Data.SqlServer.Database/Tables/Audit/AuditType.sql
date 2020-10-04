CREATE TABLE [Audit].[AuditType]
(
	[Key] INT NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,

	CONSTRAINT [PK_AuditType] PRIMARY KEY CLUSTERED
	(
		[Key]
	),

	CONSTRAINT [UK_AuditType_Name] UNIQUE
	(
		[Name]
	)
)
