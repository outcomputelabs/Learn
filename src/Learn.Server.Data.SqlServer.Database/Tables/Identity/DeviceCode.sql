CREATE TABLE [Identity].[DeviceCode]
(
    [UserCode] NVARCHAR(255) NOT NULL,
    [DeviceCode] NVARCHAR(255) NOT NULL,
    [SubjectId] NVARCHAR(255) NOT NULL,
    [ClientId] NVARCHAR(255) NOT NULL,
    [CreationTime] DATETIMEOFFSET NOT NULL,
    [Expiration] DATETIMEOFFSET NOT NULL,
    [Data] NVARCHAR(MAX) NOT NULL,

    CONSTRAINT [PK_DeviceCode] PRIMARY KEY CLUSTERED
    (
        [UserCode]
    )
)
GO
