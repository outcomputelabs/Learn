CREATE TABLE [Identity].[AspNetRoleClaims]
(
    [Id] INT NOT NULL,
    [RoleId] NVARCHAR(256) NOT NULL,
    [ClaimType] NVARCHAR(256) NOT NULL,
    [ClaimValue] NVARCHAR(256) NOT NULL,

    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED
    (
        [Id]
    ),

    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles] FOREIGN KEY
    (
        [RoleId]
    )
    REFERENCES [Identity].[AspNetRoles]
    (
        [Id]
    )
)
GO

CREATE SEQUENCE [Identity].[AspNetRoleClaimsSequence]
AS INT
START WITH 1
INCREMENT BY 1
GO