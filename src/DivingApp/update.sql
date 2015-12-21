IF OBJECT_ID(N'__MigrationHistory') IS NULL
    CREATE TABLE [__MigrationHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK_HistoryRow] PRIMARY KEY ([MigrationId])
    );

GO

CREATE TABLE [DicCert] (
    [CertID] decimal(18, 2) NOT NULL,
    [CertName] nvarchar(max),
    [Description] nvarchar(max),
    [IsGeneral] bit NOT NULL,
    [Level] tinyint NOT NULL,
    CONSTRAINT [PK_DicCert] PRIMARY KEY ([CertID])
);
CREATE TABLE [DicCountry] (
    [CountryKod] int NOT NULL,
    [Description] nvarchar(max),
    [Flag] varbinary(max),
    [FullName] nvarchar(max),
    [PhoneCode] nvarchar(max),
    [ShortName] nvarchar(max),
    [ShortName2] nvarchar(max),
    [Tmp1] int,
    [Tmp2] int,
    [Tmp3] int,
    [ValueEU] nvarchar(max),
    [ValueRU] nvarchar(max),
    CONSTRAINT [PK_DicCountry] PRIMARY KEY ([CountryKod])
);
CREATE TABLE [DicSuit] (
    [SuitID] tinyint NOT NULL,
    [SuitValue] nvarchar(max),
    CONSTRAINT [PK_DicSuit] PRIMARY KEY ([SuitID])
);
CREATE TABLE [DicTank] (
    [TankId] tinyint NOT NULL,
    [TankValue] nvarchar(max),
    CONSTRAINT [PK_DicTank] PRIMARY KEY ([TankId])
);
CREATE TABLE [DicWeightOk] (
    [WeightOkID] tinyint NOT NULL,
    [WeightOkValue] nvarchar(max),
    CONSTRAINT [PK_DicWeightOk] PRIMARY KEY ([WeightOkID])
);
CREATE TABLE [PhotoImg] (
    [PhotoID] decimal(18, 2) NOT NULL,
    [PhotoVal] varbinary(max),
    CONSTRAINT [PK_PhotoImg] PRIMARY KEY ([PhotoID])
);
CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [ConcurrencyStamp] nvarchar(max),
    [Name] nvarchar(256),
    [NormalizedName] nvarchar(256),
    CONSTRAINT [PK_IdentityRole] PRIMARY KEY ([Id])
);
CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [Adress] nvarchar(max),
    [Birth] datetime2,
    [City] nvarchar(max),
    [ConcurrencyStamp] nvarchar(max),
    [Country] int,
    [DicCountryCountryKod] int,
    [Email] nvarchar(256),
    [EmailConfirmed] bit NOT NULL,
    [FirstName] nvarchar(max),
    [LastName] nvarchar(max),
    [LockoutEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset,
    [NormalizedEmail] nvarchar(256),
    [NormalizedUserName] nvarchar(256),
    [PasswordHash] nvarchar(max),
    [PhoneNumber] nvarchar(max),
    [PhoneNumberConfirmed] bit NOT NULL,
    [Photo] varbinary(max),
    [SecurityStamp] nvarchar(max),
    [Status] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [UserName] nvarchar(256),
    CONSTRAINT [PK_User] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_User_DicCountry_DicCountryCountryKod] FOREIGN KEY ([DicCountryCountryKod]) REFERENCES [DicCountry] ([CountryKod])
);
CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max),
    [ClaimValue] nvarchar(max),
    [RoleId] nvarchar(450),
    CONSTRAINT [PK_IdentityRoleClaim<string>] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_IdentityRoleClaim<string>_IdentityRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id])
);
CREATE TABLE [Cert] (
    [CertNumber] nvarchar(450) NOT NULL,
    [DateArchieve] datetime2,
    [DicCertCertID] decimal(18, 2),
    [Issuer] nvarchar(max),
    [Photo] varbinary(max),
    [UserId] nvarchar(450),
    CONSTRAINT [PK_Cert] PRIMARY KEY ([CertNumber]),
    CONSTRAINT [FK_Cert_DicCert_DicCertCertID] FOREIGN KEY ([DicCertCertID]) REFERENCES [DicCert] ([CertID]),
    CONSTRAINT [FK_Cert_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id])
);
CREATE TABLE [Dive] (
    [DiveID] decimal(18, 2) NOT NULL,
    [AirTemperature] float,
    [Comments] nvarchar(max),
    [CountriesCountryKod] int,
    [Country] decimal(18, 2),
    [DiveDate] datetime2 NOT NULL,
    [DiveType] tinyint,
    [DiveX] float,
    [DiveY] float,
    [FiveMetersMinutes] tinyint,
    [Location] nvarchar(max),
    [MaxDepth] float,
    [Status] bit NOT NULL,
    [SuitSuitID] tinyint,
    [SuitType] tinyint,
    [Tank] tinyint,
    [TankEnd] tinyint,
    [TankNameTankId] tinyint,
    [TankStart] tinyint,
    [TotalMinutes] tinyint,
    [UpdDate] datetime2,
    [UserId] nvarchar(450),
    [Visibility] tinyint,
    [WaterTemperature] float,
    [Weight] float,
    [WeightIsOk] tinyint,
    [WeightOkWeightOkID] tinyint,
    CONSTRAINT [PK_Dive] PRIMARY KEY ([DiveID]),
    CONSTRAINT [FK_Dive_DicCountry_CountriesCountryKod] FOREIGN KEY ([CountriesCountryKod]) REFERENCES [DicCountry] ([CountryKod]),
    CONSTRAINT [FK_Dive_DicSuit_SuitSuitID] FOREIGN KEY ([SuitSuitID]) REFERENCES [DicSuit] ([SuitID]),
    CONSTRAINT [FK_Dive_DicTank_TankNameTankId] FOREIGN KEY ([TankNameTankId]) REFERENCES [DicTank] ([TankId]),
    CONSTRAINT [FK_Dive_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_Dive_DicWeightOk_WeightOkWeightOkID] FOREIGN KEY ([WeightOkWeightOkID]) REFERENCES [DicWeightOk] ([WeightOkID])
);
CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max),
    [ClaimValue] nvarchar(max),
    [UserId] nvarchar(450),
    CONSTRAINT [PK_IdentityUserClaim<string>] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_IdentityUserClaim<string>_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id])
);
CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max),
    [UserId] nvarchar(450),
    CONSTRAINT [PK_IdentityUserLogin<string>] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_IdentityUserLogin<string>_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id])
);
CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_IdentityUserRole<string>] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_IdentityUserRole<string>_IdentityRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]),
    CONSTRAINT [FK_IdentityUserRole<string>_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id])
);
CREATE TABLE [Photos] (
    [PhotoID] decimal(18, 2) NOT NULL,
    [DiveID] decimal(18, 2) NOT NULL,
    [PhotoComment] nvarchar(max),
    [PhotoDate] datetime2 NOT NULL,
    [PhotoName] nvarchar(max),
    [PhotoThumb] varbinary(max),
    [Status] bit NOT NULL,
    CONSTRAINT [PK_Photos] PRIMARY KEY ([PhotoID]),
    CONSTRAINT [FK_Photos_Dive_DiveID] FOREIGN KEY ([DiveID]) REFERENCES [Dive] ([DiveID])
);
CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
CREATE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]);
CREATE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]);
INSERT INTO [__MigrationHistory] ([MigrationId], [ProductVersion])
VALUES (N'20151216143643_InitMigration', N'7.0.0-beta7-15540');;
GO

ALTER TABLE [AspNetUsers] DROP CONSTRAINT [FK_User_DicCountry_DicCountryCountryKod];
DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'AspNetUsers') AND [c].[name] = N'Country');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var0 + ']');
ALTER TABLE [AspNetUsers] DROP COLUMN [Country];
DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'AspNetUsers') AND [c].[name] = N'DicCountryCountryKod');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var1 + ']');
ALTER TABLE [AspNetUsers] DROP COLUMN [DicCountryCountryKod];
ALTER TABLE [AspNetUsers] ADD [DicCountryId] int;
ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_User_DicCountry_DicCountryId] FOREIGN KEY ([DicCountryId]) REFERENCES [DicCountry] ([CountryKod]);
INSERT INTO [__MigrationHistory] ([MigrationId], [ProductVersion])
VALUES (N'20151221143656_DicCountryMigration', N'7.0.0-beta7-15540');;
GO

