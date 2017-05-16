﻿DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'Tests') AND [c].[name] = N'Dummy2');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Tests] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Tests] DROP COLUMN [Dummy2];

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20170516105557_test1';

GO

