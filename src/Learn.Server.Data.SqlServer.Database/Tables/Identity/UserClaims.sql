CREATE TABLE [Identity].[AspNetUserClaims]
(
    [Id] INT NOT NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [ClaimType] NVARCHAR(256) NOT NULL,
    [ClaimValue] NVARCHAR(256) NOT NULL,

    CONSTRAINT [PK_UserClaim] PRIMARY KEY CLUSTERED
    (
        [Id]
    ),

    CONSTRAINT [FK_UserClaim_User] FOREIGN KEY
    (
        [UserId]
    )
    REFERENCES [Identity].[AspNetUsers]
    (
        [Id]
    )
)
GO
