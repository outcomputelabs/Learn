CREATE TABLE [Identity].[PersistedGrant]
(
	[Key] NVARCHAR(255) NOT NULL,
    [Type] NVARCHAR(50) NOT NULL,
    [SubjectId] NVARCHAR(255) NOT NULL,
    [ClientId] NVARCHAR(255) NOT NULL,
    [CreationTime] DATETIMEOFFSET NOT NULL,
    [Expiration] DATETIMEOFFSET NOT NULL,
    [Data] NVARCHAR(MAX) NOT NULL,

    CONSTRAINT [PK_PersistedGrant] PRIMARY KEY
    (
        [Key]
    )
)
GO
