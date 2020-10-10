CREATE TABLE [Identity].[AspNetUserTokens]
(
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [LoginProvider] NVARCHAR(256) NOT NULL,
    [Name] NVARCHAR(256) NOT NULL,
    [Value] NVARCHAR(256) NOT NULL,

    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED
    (
        [UserId],
        [LoginProvider],
        [Name]
    ),

    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers] FOREIGN KEY
    (
        [UserId]
    )
    REFERENCES [Identity].[AspNetUsers]
    (
        [Id]
    )
)
GO