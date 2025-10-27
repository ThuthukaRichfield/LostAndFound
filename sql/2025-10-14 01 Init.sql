IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Users] (
    [UserId] int NOT NULL IDENTITY,
    [Email] nvarchar(max) NOT NULL,
    [Pasword] nvarchar(30) NOT NULL,
    [Role] int NOT NULL,
    [CreatedBy] nvarchar(150) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(150) NULL,
    [LastModifiedDate] datetime2 NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Name] nvarchar(200) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
);
GO

CREATE TABLE [Items] (
    [ItemId] int NOT NULL IDENTITY,
    [Title] nvarchar(150) NOT NULL,
    [Category] nvarchar(150) NOT NULL,
    [Status] int NOT NULL,
    [UserId] int NOT NULL,
    [CreatedBy] nvarchar(150) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(150) NULL,
    [LastModifiedDate] datetime2 NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    CONSTRAINT [PK_Items] PRIMARY KEY ([ItemId]),
    CONSTRAINT [FK_Items_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Claims] (
    [ClaimId] int NOT NULL IDENTITY,
    [ItemId] int NOT NULL,
    [UserId] int NOT NULL,
    [CreatedBy] nvarchar(150) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(150) NULL,
    [LastModifiedDate] datetime2 NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    CONSTRAINT [PK_Claims] PRIMARY KEY ([ClaimId]),
    CONSTRAINT [FK_Claims_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([ItemId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Claims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Disputes] (
    [DisputeId] int NOT NULL IDENTITY,
    [Reason] nvarchar(max) NOT NULL,
    [Status] int NOT NULL,
    [ClaimId] int NOT NULL,
    [CreatedBy] nvarchar(150) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(150) NULL,
    [LastModifiedDate] datetime2 NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    CONSTRAINT [PK_Disputes] PRIMARY KEY ([DisputeId]),
    CONSTRAINT [FK_Disputes_Claims_ClaimId] FOREIGN KEY ([ClaimId]) REFERENCES [Claims] ([ClaimId]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Claims_ItemId] ON [Claims] ([ItemId]);
GO

CREATE INDEX [IX_Claims_UserId] ON [Claims] ([UserId]);
GO

CREATE INDEX [IX_Disputes_ClaimId] ON [Disputes] ([ClaimId]);
GO

CREATE INDEX [IX_Items_UserId] ON [Items] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251014052854_Init', N'8.0.19');
GO

COMMIT;
GO

