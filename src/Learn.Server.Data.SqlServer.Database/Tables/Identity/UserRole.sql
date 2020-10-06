CREATE TABLE [Identity].[UserRole]
(
	[UserId] INT NOT NULL,
	[RoleId] INT NOT NULL,

	CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED
	(
		[UserId],
		[RoleId]
	),

	CONSTRAINT [FK_UserRole_Role] FOREIGN KEY
	(
		[RoleId]
	)
	REFERENCES [Identity].[Role]
	(
		[Id]
	),

	CONSTRAINT [FK_UserRole_User] FOREIGN KEY
	(
		[UserId]
	)
	REFERENCES [Identity].[User]
	(
		[Id]
	)
)
GO

