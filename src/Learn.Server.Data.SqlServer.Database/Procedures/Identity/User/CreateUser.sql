CREATE PROCEDURE [Identity].[CreateUser]
    @Id UNIQUEIDENTIFIER,
	@UserName NVARCHAR(255),
    @NormalizedUserName NVARCHAR(255),
    @Email NVARCHAR(255),
    @NormalizedEmail NVARCHAR(255),
    @EmailConfirmed BIT,
    @PasswordHash NVARCHAR(255),
    @SecurityStamp UNIQUEIDENTIFIER,
    @ConcurrencyStamp UNIQUEIDENTIFIER,
    @PhoneNumber NVARCHAR(255),
    @PhoneNumberConfirmed BIT,
    @TwoFactorEnabled BIT,
    @LockoutEnd DATETIMEOFFSET,
    @LockoutEnabled BIT,
    @AccessFailedCount INT
AS

SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @AuditId INT = NEXT VALUE FOR [Audit].[UserAuditSequence];
DECLARE @AuditTypeId INT = (SELECT [Id] FROM [Audit].[AuditType] WHERE [Name] = 'INSERTED');
DECLARE @AuditTimestamp DATETIMEOFFSET = SYSDATETIMEOFFSET();

INSERT [Identity].[AspNetUsers]
(
    [Id],
    [UserName],
    [NormalizedUserName],
    [Email],
    [NormalizedEmail],
    [EmailConfirmed],
    [PasswordHash],
    [SecurityStamp],
    [ConcurrencyStamp],
    [PhoneNumber],
    [PhoneNumberConfirmed],
    [TwoFactorEnabled],
    [LockoutEnd],
    [LockoutEnabled],
    [AccessFailedCount]
)

OUTPUT
    
    @AuditId,
    @AuditTypeId,
    @AuditTimestamp,

    [inserted].[Id],
    [inserted].[UserName],
    [inserted].[NormalizedUserName],
    [inserted].[Email],
    [inserted].[NormalizedEmail],
    [inserted].[EmailConfirmed],
    [inserted].[PasswordHash],
    [inserted].[SecurityStamp],
    [inserted].[ConcurrencyStamp],
    [inserted].[PhoneNumber],
    [inserted].[PhoneNumberConfirmed],
    [inserted].[TwoFactorEnabled],
    [inserted].[LockoutEnd],
    [inserted].[LockoutEnabled],
    [inserted].[AccessFailedCount]

INTO [Audit].[UserAudit]
(
    [AuditId],
    [AuditTypeId],
    [AuditTimestamp],

	[Id],
    [UserName],
    [NormalizedUserName],
    [Email],
    [NormalizedEmail],
    [EmailConfirmed],
    [PasswordHash],
    [SecurityStamp],
    [ConcurrencyStamp],
    [PhoneNumber],
    [PhoneNumberConfirmed],
    [TwoFactorEnabled],
    [LockoutEnd],
    [LockoutEnabled],
    [AccessFailedCount]
)

OUTPUT
    
    [inserted].[Id],
    [inserted].[UserName],
    [inserted].[NormalizedUserName],
    [inserted].[Email],
    [inserted].[NormalizedEmail],
    [inserted].[EmailConfirmed],
    [inserted].[PasswordHash],
    [inserted].[SecurityStamp],
    [inserted].[ConcurrencyStamp],
    [inserted].[PhoneNumber],
    [inserted].[PhoneNumberConfirmed],
    [inserted].[TwoFactorEnabled],
    [inserted].[LockoutEnd],
    [inserted].[LockoutEnabled],
    [inserted].[AccessFailedCount]

VALUES
(
    @Id,
    @UserName,
    @NormalizedUserName,
    @Email,
    @NormalizedEmail,
    @EmailConfirmed,
    @PasswordHash,
    @SecurityStamp,
    @ConcurrencyStamp,
    @PhoneNumber,
    @PhoneNumberConfirmed,
    @TwoFactorEnabled,
    @LockoutEnd,
    @LockoutEnabled,
    @AccessFailedCount
)

RETURN 0;
GO
