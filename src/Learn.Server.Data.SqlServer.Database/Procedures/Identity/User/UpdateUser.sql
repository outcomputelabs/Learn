CREATE PROCEDURE [Identity].[UpdateUser]
    @Id INT,
	@UserName NVARCHAR(255),
    @NormalizedUserName NVARCHAR(255),
    @Email NVARCHAR(255),
    @NormalizedEmail NVARCHAR(255),
    @EmailConfirmed BIT,
    @PasswordHash NVARCHAR(255),
    @PhoneNumber NVARCHAR(255),
    @PhoneNumberConfirmed BIT,
    @TwoFactorEnabled BIT,
    @LockoutEnd DATETIMEOFFSET,
    @LockoutEnabled BIT,
    @AccessFailedCount INT,
    @SecurityStamp UNIQUEIDENTIFIER,
    @ConcurrencyStamp UNIQUEIDENTIFIER
AS

SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @NewConcurrencyStamp UNIQUEIDENTIFIER = NEWID();

DECLARE @AuditId INT = NEXT VALUE FOR [Audit].[UserAuditSequence];
DECLARE @AuditTypeId INT = (SELECT [Id] FROM [Audit].[AuditType] WHERE [Name] = 'UPDATED');
DECLARE @AuditTimestamp DATETIMEOFFSET = SYSDATETIMEOFFSET();

UPDATE [Identity].[User]
SET
    [UserName] = @UserName,
    [NormalizedUserName] = @NormalizedUserName,
    [Email] = @Email,
    [NormalizedEmail] = @NormalizedEmail,
    [EmailConfirmed] = @EmailConfirmed,
    [PasswordHash] = @PasswordHash,
    [PhoneNumber] = @PhoneNumber,
    [PhoneNumberConfirmed] = @PhoneNumberConfirmed,
    [TwoFactorEnabled] = @TwoFactorEnabled,
    [LockoutEnd] = @LockoutEnd,
    [LockoutEnabled] = @LockoutEnabled,
    [AccessFailedCount] = @AccessFailedCount,
    [SecurityStamp] = @SecurityStamp,
    [ConcurrencyStamp] = @NewConcurrencyStamp

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

WHERE
    [Id] = @Id
    AND [ConcurrencyStamp] = @ConcurrencyStamp

RETURN 0;
GO
