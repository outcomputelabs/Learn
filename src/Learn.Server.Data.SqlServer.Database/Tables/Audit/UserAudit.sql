CREATE TABLE [Audit].[UserAudit]
(
	[AuditId] INT NOT NULL,
    [AuditTypeId] INT NOT NULL,
    [AuditTimestamp] DATETIMEOFFSET NOT NULL,

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
    [LockoutEnd] DATETIMEOFFSET NOT NULL,
    [LockoutEnabled] BIT NOT NULL,
    [AccessFailedCount] INT NOT NULL,

    CONSTRAINT [PK_UserAudit] PRIMARY KEY CLUSTERED
    (
        [AuditId]
    ),

    CONSTRAINT [FK_UserAudit_AuditType] FOREIGN KEY
    (
        [AuditTypeId]
    )
    REFERENCES [Audit].[AuditType]
    (
        [Id]
    )
)
GO

CREATE SEQUENCE [Audit].[UserAuditSequence]
AS INT
START WITH 1
INCREMENT BY 1
GO
