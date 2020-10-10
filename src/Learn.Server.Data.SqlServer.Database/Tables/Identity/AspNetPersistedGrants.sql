CREATE TABLE [Identity].[AspNetPersistedGrants]
(
	[Key] NVARCHAR(256) NOT NULL,
    [Type] NVARCHAR(256) NOT NULL,
    [SubjectId] NVARCHAR(256) NOT NULL,
    [ClientId] NVARCHAR(256) NOT NULL,
    [CreationTime] DATETIMEOFFSET NOT NULL,
    [Expiration] DATETIMEOFFSET NOT NULL,
    [Data] NVARCHAR(MAX) NOT NULL,

    CONSTRAINT [PK_AspNetPersistedGrants] PRIMARY KEY
    (
        [Key]
    )
)
GO
