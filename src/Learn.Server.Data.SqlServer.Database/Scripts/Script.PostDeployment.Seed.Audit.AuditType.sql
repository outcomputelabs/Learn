
WITH [Source] AS
(
    SELECT
        [Key],
        [Name]
    FROM
    (
        VALUES
            (1, 'INSERTED'),
            (2, 'UPDATED'),
            (3, 'DELETED')

    ) AS [X] ([Key], [Name])
)
MERGE [Audit].[AuditType] AS [Target]
USING [Source]
ON [Source].[Key] = [Target].[Key]
WHEN MATCHED AND [Target].[Name] != [Source].[Name] THEN
UPDATE SET
    [Name] = [Source].[Name]
WHEN NOT MATCHED BY TARGET THEN
INSERT
(
    [Key],
    [Name]
)
VALUES
(
    [Key],
    [Name]
)
WHEN NOT MATCHED BY SOURCE THEN
DELETE;

GO