CREATE PROCEDURE [Identity].[FindUserByNormalizedUserName]
    @NormalizedUserName NVARCHAR(255)
AS

SET NOCOUNT ON;
SET XACT_ABORT ON;

SELECT

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

FROM
    [Identity].[User]

WHERE
    [NormalizedUserName] = @NormalizedUserName

RETURN 0;
GO
