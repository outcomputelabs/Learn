CREATE TABLE [Identity].[User]
(
	[Id] INT NOT NULL,
    [UserName] NVARCHAR(255) NOT NULL,
    [NormalizedUserName] NVARCHAR(255) NOT NULL,
    [Email] NVARCHAR(255) NOT NULL,
    [NormalizedEmail] NVARCHAR(255) NOT NULL,
    [EmailConfirmed] BIT NOT NULL,
    [PasswordHash] NVARCHAR(255) NOT NULL,
    [SecurityStamp] UNIQUEIDENTIFIER NOT NULL,
    [ConcurrencyStamp] UNIQUEIDENTIFIER NOT NULL,
    [PhoneNumber] NVARCHAR(255) NOT NULL,
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

CREATE SEQUENCE [Identity].[UserSequence]
AS INT
START WITH 1
INCREMENT BY 1
GO
