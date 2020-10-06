CREATE TABLE [Audit].[AuditType]
(
	[Id] INT NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,

	CONSTRAINT [PK_AuditType] PRIMARY KEY CLUSTERED
	(
		[Id]
	),

	CONSTRAINT [UK_AuditType_Name] UNIQUE
	(
		[Name]
	)
)
