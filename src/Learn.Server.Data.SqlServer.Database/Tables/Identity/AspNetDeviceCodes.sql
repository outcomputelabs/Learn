CREATE TABLE [Identity].[AspNetDeviceCodes]
(
    [UserCode] NVARCHAR(256) NOT NULL,
    [DeviceCode] NVARCHAR(256) NOT NULL,
    [SubjectId] NVARCHAR(256) NOT NULL,
    [ClientId] NVARCHAR(256) NOT NULL,
    [CreationTime] DATETIMEOFFSET NOT NULL,
    [Expiration] DATETIMEOFFSET NOT NULL,
    [Data] NVARCHAR(MAX) NOT NULL,

    CONSTRAINT [PK_AspNetDeviceCodes] PRIMARY KEY CLUSTERED
    (
        [UserCode]
    )
)
GO
