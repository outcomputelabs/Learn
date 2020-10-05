CREATE TABLE [Identity].[UserToken]
(
    [UserId] NVARCHAR(255) NOT NULL,
    [LoginProvider] NVARCHAR(255) NOT NULL,
    [Name] NVARCHAR(255) NOT NULL,
    [Value] NVARCHAR(255) NOT NULL,

    CONSTRAINT [PK_UserToken] PRIMARY KEY CLUSTERED
    (
        [UserId],
        [LoginProvider],
        [Name]
    ),

    CONSTRAINT [FK_UserToken_User] FOREIGN KEY
    (
        [UserId]
    )
    REFERENCES [Identity].[User]
    (
        [Id]
    )
)
GO