CREATE PROCEDURE [Identity].[FindUserById]
    @Id UNIQUEIDENTIFIER
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
    [Identity].[AspNetUsers]

WHERE
    [Id] = @Id

RETURN 0;
GO
