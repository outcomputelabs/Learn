CREATE TABLE [Identity].[UserClaim]
(
    [Id] INT NOT NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [ClaimType] NVARCHAR(255) NOT NULL,
    [ClaimValue] NVARCHAR(255) NOT NULL,

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
