CREATE TABLE [Identity].[AspNetUsers]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
    [UserName] NVARCHAR(256) NOT NULL,
    [NormalizedUserName] NVARCHAR(256) NOT NULL,
    [Email] NVARCHAR(256) NOT NULL,
    [NormalizedEmail] NVARCHAR(256) NOT NULL,
    [EmailConfirmed] BIT NOT NULL,
    [PasswordHash] NVARCHAR(256) NOT NULL,
    [SecurityStamp] NVARCHAR(256) NOT NULL,
    [ConcurrencyStamp] UNIQUEIDENTIFIER NOT NULL,
    [PhoneNumber] NVARCHAR(256) NULL,
    [PhoneNumberConfirmed] BIT NOT NULL,
    [TwoFactorEnabled] BIT NOT NULL,
    [LockoutEnd] DATETIMEOFFSET NULL,
    [LockoutEnabled] BIT NOT NULL,
    [AccessFailedCount] INT NOT NULL,

    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED
    (
        [Id]
    ),

    CONSTRAINT [UK_User_NormalizedUserName] UNIQUE
    (
        [NormalizedUserName]
    ),

    CONSTRAINT [UK_User_NormalizedEmail] UNIQUE
    (
        [NormalizedEmail]
    )
)
GO
