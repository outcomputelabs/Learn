CREATE TABLE [Identity].[User]
(
	[Id] NVARCHAR(255) NOT NULL,
    [UserName] NVARCHAR(255) NOT NULL,
    [NormalizedUserName] NVARCHAR(255) NOT NULL,
    [Email] NVARCHAR(255) NOT NULL,
    [NormalizedEmail] NVARCHAR(255) NOT NULL,
    [EmailConfirmed] BIT NOT NULL,
    [PasswordHash] NVARCHAR(255) NOT NULL,
    [SecurityStamp] NVARCHAR(255) NOT NULL,
    [ConcurrencyStamp] NVARCHAR(255) NOT NULL,
    [PhoneNumber] NVARCHAR(255) NOT NULL,
    [PhoneNumberConfirmed] BIT NOT NULL,
    [TwoFactorEnabled] BIT NOT NULL,
    [LockoutEnd] DATETIMEOFFSET NOT NULL,
    [LockoutEnabled] BIT NOT NULL,
    [AccessFailedCount] INT NOT NULL,

    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED
    (
        [Id]
    )
)
GO