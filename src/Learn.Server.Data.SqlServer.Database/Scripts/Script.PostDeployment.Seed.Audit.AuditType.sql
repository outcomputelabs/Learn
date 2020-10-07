
WITH [Source] AS
(
    SELECT
        [Id],
        [Name]
    FROM
    (
        VALUES
            (1, 'INSERTED'),
            (2, 'UPDATED'),
            (3, 'DELETED')

    ) AS [X] ([Id], [Name])
)
MERGE [Audit].[AuditType] AS [Target]
USING [Source]
ON [Source].[Id] = [Target].[Id]
WHEN MATCHED AND [Target].[Name] != [Source].[Name] THEN
UPDATE SET
    [Name] = [Source].[Name]
WHEN NOT MATCHED BY TARGET THEN
INSERT
(
    [Id],
    [Name]
)
VALUES
(
    [Id],
    [Name]
)
WHEN NOT MATCHED BY SOURCE THEN
DELETE;

GO