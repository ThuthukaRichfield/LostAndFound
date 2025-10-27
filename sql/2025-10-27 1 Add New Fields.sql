BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Email');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Users] ALTER COLUMN [Email] nvarchar(150) NOT NULL;
GO

ALTER TABLE [Items] ADD [DateLost] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Items] ADD [Location] nvarchar(250) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Items] ADD [LostDescription] nvarchar(250) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Items] ADD [LostImage] nvarchar(max) NULL;
GO

ALTER TABLE [Claims] ADD [FoundDescription] nvarchar(250) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Claims] ADD [FoundImage] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251027200145_AddNewFields', N'8.0.19');
GO

COMMIT;
GO

