CREATE TABLE [Identity].[RoleClaim]
(
    [Id] INT NOT NULL,
    [RoleId] INT NOT NULL,
    [ClaimType] NVARCHAR(255) NOT NULL,
    [ClaimValue] NVARCHAR(255) NOT NULL,

    CONSTRAINT [PK_RoleClaim] PRIMARY KEY CLUSTERED
    (
        [Id]
    ),

    CONSTRAINT [FK_RoleClaim_Role] FOREIGN KEY
    (
        [RoleId]
    )
    REFERENCES [Identity].[Role]
    (
        [Id]
    )
)
GO

CREATE SEQUENCE [Identity].[RoleClaimSequence]
AS INT
START WITH 1
INCREMENT BY 1
GO