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
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdateBy] nvarchar(max) NULL,
    [UpdatedAt] datetime2 NULL,
    [DeleteBy] nvarchar(max) NULL,
    [DeletedAt] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220811034327_Create Users table', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdateBy] nvarchar(max) NULL,
    [UpdatedAt] datetime2 NULL,
    [DeleteBy] nvarchar(max) NULL,
    [DeletedAt] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Courses] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [ShortDescription] nvarchar(280) NOT NULL,
    [Level] int NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdateBy] nvarchar(max) NULL,
    [UpdatedAt] datetime2 NULL,
    [DeleteBy] nvarchar(max) NULL,
    [DeletedAt] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Students] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Dob] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdateBy] nvarchar(max) NULL,
    [UpdatedAt] datetime2 NULL,
    [DeleteBy] nvarchar(max) NULL,
    [DeletedAt] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CategoryCourse] (
    [CategoriesId] int NOT NULL,
    [CoursesId] int NOT NULL,
    CONSTRAINT [PK_CategoryCourse] PRIMARY KEY ([CategoriesId], [CoursesId]),
    CONSTRAINT [FK_CategoryCourse_Categories_CategoriesId] FOREIGN KEY ([CategoriesId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CategoryCourse_Courses_CoursesId] FOREIGN KEY ([CoursesId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Chapters] (
    [Id] int NOT NULL IDENTITY,
    [CourseId] int NOT NULL,
    [List] nvarchar(max) NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdateBy] nvarchar(max) NULL,
    [UpdatedAt] datetime2 NULL,
    [DeleteBy] nvarchar(max) NULL,
    [DeletedAt] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Chapters] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Chapters_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [CourseStudent] (
    [CoursesId] int NOT NULL,
    [StudentsId] int NOT NULL,
    CONSTRAINT [PK_CourseStudent] PRIMARY KEY ([CoursesId], [StudentsId]),
    CONSTRAINT [FK_CourseStudent_Courses_CoursesId] FOREIGN KEY ([CoursesId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CourseStudent_Students_StudentsId] FOREIGN KEY ([StudentsId]) REFERENCES [Students] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_CategoryCourse_CoursesId] ON [CategoryCourse] ([CoursesId]);
GO

CREATE UNIQUE INDEX [IX_Chapters_CourseId] ON [Chapters] ([CourseId]);
GO

CREATE INDEX [IX_CourseStudent_StudentsId] ON [CourseStudent] ([StudentsId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220823014142_AddJwtConfigrations', N'6.0.8');
GO

COMMIT;
GO

