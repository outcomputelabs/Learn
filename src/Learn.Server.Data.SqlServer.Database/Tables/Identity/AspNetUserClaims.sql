CREATE TABLE [Identity].[AspNetUserClaims]
(
    [Id] INT NOT NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [ClaimType] NVARCHAR(256) NOT NULL,
    [ClaimValue] NVARCHAR(256) NOT NULL,

    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED
    (
        [Id]
    ),

    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers] FOREIGN KEY
    (
        [UserId]
    )
    REFERENCES [Identity].[AspNetUsers]
    (
        [Id]
    )
)
GO

CREATE SEQUENCE [Identity].[AspNetUserClaimsSequence]
AS INT
START WITH 1
INCREMENT BY 1
GO