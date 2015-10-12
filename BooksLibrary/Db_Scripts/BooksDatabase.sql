/*
Deployment script for BookLibrary database
*/

USE [master];
GO

SET NOEXEC OFF
GO

IF (DB_ID('BookLibrary') IS NOT NULL) 
BEGIN
    ALTER DATABASE [BookLibrary]
		SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [BookLibrary];
END
GO

PRINT N'Creating BookLibrary database...'
GO

CREATE DATABASE [BookLibrary] COLLATE SQL_Latin1_General_CP1_CI_AS
GO

USE [BookLibrary];
GO

PRINT N'Creating [dbo].[Category]...';
GO

CREATE TABLE [dbo].[Category] (
    [Category_Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Category_Name]        VARCHAR (50)  NULL,
    [Category_Description] VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([Category_Id] ASC)
);
GO

PRINT N'Creating [dbo].[Author]...';
GO

CREATE TABLE [dbo].[Author] (
    [Author_Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Last_Name]   VARCHAR (50) NOT NULL,
    [First_Name]  VARCHAR (50) NOT NULL,
    [Middle_Name] VARCHAR (50) NULL,
    CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED ([Author_Id] ASC)
);
GO

PRINT N'Creating [dbo].[Book]...';
GO

CREATE TABLE [dbo].[Book] (
    [Book_Id]          INT           IDENTITY (1, 1) NOT NULL,
    [ISBN]             VARCHAR (13)  NULL,
    [Book_Name]        VARCHAR (255) NOT NULL,
    [Author_Id]        INT           NOT NULL,
    [Publication_Date] DATE          NULL,
    [Category_Id]      INT           NOT NULL,
    [Publisher]        VARCHAR (50)  NULL,
    CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED ([Book_Id] ASC)
);
GO

PRINT N'Creating [dbo].[Book].[IX_Book_Author_Id]...';
GO

CREATE NONCLUSTERED INDEX [IX_Book_Author_Id]
    ON [dbo].[Book]([Author_Id] ASC);
GO

PRINT N'Creating [dbo].[Book].[IX_Book_Category_Id]...';
GO

CREATE NONCLUSTERED INDEX [IX_Book_Category_Id]
    ON [dbo].[Book]([Category_Id] ASC);
GO

PRINT N'Creating [dbo].[FK_Book_Author]...';
GO

ALTER TABLE [dbo].[Book]
    ADD CONSTRAINT [FK_Book_Author] FOREIGN KEY ([Author_Id]) REFERENCES [dbo].[Author] ([Author_Id]);
GO

PRINT N'Creating [dbo].[FK_Book_Category]...';
GO

ALTER TABLE [dbo].[Book]
    ADD CONSTRAINT [FK_Book_Category] FOREIGN KEY ([Category_Id]) REFERENCES [dbo].[Category] ([Category_Id]);
GO

PRINT N'Database and Tables creation is complete.';
GO

PRINT N'Adding sample data.';
GO

SET IDENTITY_INSERT [dbo].[Category] ON;
GO

INSERT INTO [dbo].[Category] ([Category_Id], [Category_Name],[Category_Description]) VALUES(1, 'Textbook', 'Books that usually contain information related to scholar curriculum');
INSERT INTO [dbo].[Category] ([Category_Id], [Category_Name],[Category_Description]) VALUES(2, 'Politics', 'Books about politics subject');
INSERT INTO [dbo].[Category] ([Category_Id], [Category_Name],[Category_Description]) VALUES(3, 'Science', 'Like Chemestry, Physics, Math, etc.');
INSERT INTO [dbo].[Category] ([Category_Id], [Category_Name],[Category_Description]) VALUES(4, 'Self-Help', 'Motivational books');
INSERT INTO [dbo].[Category] ([Category_Id], [Category_Name],[Category_Description]) VALUES(5, 'General', 'All other type of books');
INSERT INTO [dbo].[Category] ([Category_Id], [Category_Name],[Category_Description]) VALUES(6, 'Engineering', 'Engineering books');
INSERT INTO [dbo].[Category] ([Category_Id], [Category_Name],[Category_Description]) VALUES(7, 'Biography', 'Biography');
INSERT INTO [dbo].[Category] ([Category_Id], [Category_Name],[Category_Description]) VALUES(8, 'Business', 'Business');
INSERT INTO [dbo].[Category] ([Category_Id], [Category_Name],[Category_Description]) VALUES(9, 'Cookbooks', 'Cookbooks');
INSERT INTO [dbo].[Category] ([Category_Id], [Category_Name],[Category_Description]) VALUES(10, 'Health', 'Health');
INSERT INTO [dbo].[Category] ([Category_Id], [Category_Name],[Category_Description]) VALUES(11, 'Science Fiction', 'Science Fiction');
INSERT INTO [dbo].[Category] ([Category_Id], [Category_Name],[Category_Description]) VALUES(12, 'Novels', 'Novels');
INSERT INTO [dbo].[Category] ([Category_Id], [Category_Name],[Category_Description]) VALUES(13, 'History', 'History');
INSERT INTO [dbo].[Category] ([Category_Id], [Category_Name],[Category_Description]) VALUES(14, 'Mystery & Crime', 'Mystery & Crime');
INSERT INTO [dbo].[Category] ([Category_Id], [Category_Name],[Category_Description]) VALUES(15, 'Religion', 'Religion');
INSERT INTO [dbo].[Category] ([Category_Id], [Category_Name],[Category_Description]) VALUES(16, 'Romance', 'Romance');
INSERT INTO [dbo].[Category] ([Category_Id], [Category_Name],[Category_Description]) VALUES(17, 'Comics', 'Comics');
GO

SET IDENTITY_INSERT [dbo].[Category] OFF;
GO

SET IDENTITY_INSERT [dbo].[Author] ON;
GO

INSERT INTO [dbo].[Author] ([Author_Id], [Last_Name],[First_Name]) VALUES(1, 'Dyer', 'Wayne');
INSERT INTO [dbo].[Author] ([Author_Id], [Last_Name],[First_Name]) VALUES(2, 'Tyson', 'Eric');
INSERT INTO [dbo].[Author] ([Author_Id], [Last_Name],[First_Name]) VALUES(3, 'Brown', 'Brene ');
INSERT INTO [dbo].[Author] ([Author_Id], [Last_Name],[First_Name]) VALUES(4, 'Larson', 'Ron');
INSERT INTO [dbo].[Author] ([Author_Id], [Last_Name],[First_Name]) VALUES(5, 'D''Antonio', 'Michael');
INSERT INTO [dbo].[Author] ([Author_Id], [Last_Name],[First_Name]) VALUES(6, 'Kiyosaki', 'Robert');

SET IDENTITY_INSERT [dbo].[Author] OFF;
GO

INSERT INTO [dbo].[Book] ([ISBN],[Book_Name], [Author_Id], [Publication_Date], [Category_Id], [Publisher]) VALUES('308102282', 'Erroneous Zones', 1 , '07/01/1976', 4, 'Barnes & Noble ');
INSERT INTO [dbo].[Book] ([ISBN],[Book_Name], [Author_Id], [Publication_Date], [Category_Id], [Publisher]) VALUES('1209873465', 'The Gifts of Imperfection', 3 , '08/27/2015', 1, ' Hazelden   ');
INSERT INTO [dbo].[Book] ([ISBN],[Book_Name], [Author_Id], [Publication_Date], [Category_Id], [Publisher]) VALUES('9781285057095', 'Calculus', 4 , '01/01/2013', 1, 'Cengage Learning');
INSERT INTO [dbo].[Book] ([ISBN],[Book_Name], [Author_Id], [Publication_Date], [Category_Id], [Publisher]) VALUES('9781466840423', 'Never Enough: Donald Trump and the Pursuit of Success', 5 , '10/06/2015', 7, 'St Martins Press');
INSERT INTO [dbo].[Book] ([ISBN],[Book_Name], [Author_Id], [Publication_Date], [Category_Id], [Publisher]) VALUES('9781612680200', 'Rich Dad''s Guide to Investing', 6 , '04/03/2012', 4, 'Plata Publishing, LLC.');
INSERT INTO [dbo].[Book] ([ISBN],[Book_Name], [Author_Id], [Publication_Date], [Category_Id], [Publisher]) VALUES('9781612680002', 'Rich Dad Poor Dad', 6 , '06/14/2011', 4, 'Plata Publishing, LLC.');
GO

PRINT N'Process completed successfully.';
GO