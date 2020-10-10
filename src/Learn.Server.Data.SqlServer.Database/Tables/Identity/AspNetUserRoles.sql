CREATE TABLE [Identity].[AspNetUserRoles]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL,
	[RoleId] NVARCHAR(256) NOT NULL,

	CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED
	(
		[UserId],
		[RoleId]
	),

	CONSTRAINT [FK_AspNetUserRoles_AspNetRoles] FOREIGN KEY
	(
		[RoleId]
	)
	REFERENCES [Identity].[AspNetRoles]
	(
		[Id]
	),

	CONSTRAINT [FK_UserRole_User] FOREIGN KEY
	(
		[UserId]
	)
	REFERENCES [Identity].[AspNetUsers]
	(
		[Id]
	)
)
GO

