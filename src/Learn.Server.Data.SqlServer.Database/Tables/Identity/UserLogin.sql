CREATE TABLE [Identity].[UserLogin]
(
    [LoginProvider] NVARCHAR(255) NOT NULL,
    [ProviderKey] NVARCHAR(255) NOT NULL,
    [ProviderDisplayName] NVARCHAR(255) NOT NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL,

    CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED
    (
        [LoginProvider],
        [ProviderKey]
    ),

    CONSTRAINT [FK_UserLogin_User] FOREIGN KEY
    (
        [UserId]
    )
    REFERENCES [Identity].[AspNetUsers]
    (
        [Id]
    )
)
GO
