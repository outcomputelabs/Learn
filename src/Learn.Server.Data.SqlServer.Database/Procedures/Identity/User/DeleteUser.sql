CREATE PROCEDURE [Identity].[DeleteUser]
    @Id INT,
    @ConcurrencyStamp UNIQUEIDENTIFIER
AS

SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @AuditId INT = NEXT VALUE FOR [Audit].[UserAuditSequence];
DECLARE @AuditTypeId INT = (SELECT [Id] FROM [Audit].[AuditType] WHERE [Name] = 'DELETED');
DECLARE @AuditTimestamp DATETIMEOFFSET = SYSDATETIMEOFFSET();

DELETE [Identity].[User]

OUTPUT
    
    @AuditId,
    @AuditTypeId,
    @AuditTimestamp,

    [deleted].[Id],
    [deleted].[UserName],
    [deleted].[NormalizedUserName],
    [deleted].[Email],
    [deleted].[NormalizedEmail],
    [deleted].[EmailConfirmed],
    [deleted].[PasswordHash],
    [deleted].[SecurityStamp],
    [deleted].[ConcurrencyStamp],
    [deleted].[PhoneNumber],
    [deleted].[PhoneNumberConfirmed],
    [deleted].[TwoFactorEnabled],
    [deleted].[LockoutEnd],
    [deleted].[LockoutEnabled],
    [deleted].[AccessFailedCount]

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
    
    [deleted].[Id],
    [deleted].[UserName],
    [deleted].[NormalizedUserName],
    [deleted].[Email],
    [deleted].[NormalizedEmail],
    [deleted].[EmailConfirmed],
    [deleted].[PasswordHash],
    [deleted].[SecurityStamp],
    [deleted].[ConcurrencyStamp],
    [deleted].[PhoneNumber],
    [deleted].[PhoneNumberConfirmed],
    [deleted].[TwoFactorEnabled],
    [deleted].[LockoutEnd],
    [deleted].[LockoutEnabled],
    [deleted].[AccessFailedCount]

WHERE
    [Id] = @Id
    AND [ConcurrencyStamp] = @ConcurrencyStamp

RETURN 0;
GO
