CREATE TABLE [Identity].[UserClaim]
(
    [Id] INT NOT NULL,
    [UserId] NVARCHAR(255) NOT NULL,
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
    REFERENCES [Identity].[User]
    (
        [Id]
    )
)
GO

CREATE SEQUENCE [Identity].[UserClaimSequence]
AS INT
START WITH 1
INCREMENT BY 1
GO