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

CREATE TABLE [Customers] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(20) NOT NULL,
    [Forename] nvarchar(50) NOT NULL,
    [Surname] nvarchar(50) NOT NULL,
    [EmailAddress] nvarchar(75) NOT NULL,
    [MobileNumber] nvarchar(15) NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Addresses] (
    [Id] int NOT NULL IDENTITY,
    [AddressLineOne] nvarchar(max) NOT NULL,
    [AddressLineTwo] nvarchar(max) NULL,
    [Town] nvarchar(max) NOT NULL,
    [County] nvarchar(max) NULL,
    [PostCode] nvarchar(max) NOT NULL,
    [Country] nvarchar(max) NULL,
    [CustomerId] int NOT NULL,
    [IsPrimary] bit NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Addresses_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Addresses_CustomerId] ON [Addresses] ([CustomerId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230728090347_InitialMigration', N'7.0.9');
GO

COMMIT;
GO

