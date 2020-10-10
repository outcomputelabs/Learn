CREATE TABLE [Identity].[AspNetUserLogins]
(
    [LoginProvider] NVARCHAR(256) NOT NULL,
    [ProviderKey] NVARCHAR(256) NOT NULL,
    [ProviderDisplayName] NVARCHAR(256) NOT NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL,

    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED
    (
        [LoginProvider],
        [ProviderKey]
    ),

    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers] FOREIGN KEY
    (
        [UserId]
    )
    REFERENCES [Identity].[AspNetUsers]
    (
        [Id]
    )
)
GO
