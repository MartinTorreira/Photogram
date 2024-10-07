/* 
 * SQL Server Script
 * 
 * In a local environment (for example, with the SQLServerExpress instance 
 * included in the VStudio installation) it will be necessary to create the 
 * database and the user required by the connection string. So, the following
 * steps are needed:
 *
 *     Configure the @Default_DB_Path variable with the path where 
 *     database and log files will be created  
 *
 * This script can be executed from MS Sql Server Management Studio Express,
 * but also it is possible to use a command Line syntax:
 *
 *    > sqlcmd.exe -U [user] -P [password] -I -i SqlServerCreateTables.sql
 *
 */


 /******************************************************************************/
 /*** PATH to store the db files. This path must exists in the local system. ***/
 /******************************************************************************/
 DECLARE @Default_DB_Path as VARCHAR(64)  
 SET @Default_DB_Path = N'C:\source\'
 
USE [master]


/***** Drop database if already exists  ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = 'photogram_test')
DROP DATABASE [photogram_test]


USE [master]

/* DataBase Creation */

	                              
DECLARE @sql nvarchar(500)

SET @sql = 
  N'CREATE DATABASE [photogram_test] 
    ON PRIMARY ( NAME = photogram_test, FILENAME = "' + @Default_DB_Path + N'photogram_test.mdf")
    LOG ON ( NAME = photogram_test_log, FILENAME = "' + @Default_DB_Path + N'photogram_test_log.ldf")'

EXEC(@sql)
PRINT N'Database [Photogram_test] created.'
GO

SET QUOTED_IDENTIFIER OFF;
GO
USE [photogram_test];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO



-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/24/2023 15:29:26
-- Generated from EDMX file: C:\Users\regis\source\repos\practica-mad-mad18\Model\photogram.edmx
-- --------------------------------------------------

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------
IF OBJECT_ID(N'[dbo].[FK__Comment__imageId__607251E5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comment] DROP CONSTRAINT [FK__Comment__imageId__607251E5];
GO
IF OBJECT_ID(N'[dbo].[FK__Comment__userId__5F7E2DAC]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comment] DROP CONSTRAINT [FK__Comment__userId__5F7E2DAC];
GO
IF OBJECT_ID(N'[dbo].[FK__Follow__creatorI__1FEDB87C]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Follow] DROP CONSTRAINT [FK__Follow__creatorI__1FEDB87C];
GO
IF OBJECT_ID(N'[dbo].[FK__Follow__follower__1EF99443]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Follow] DROP CONSTRAINT [FK__Follow__follower__1EF99443];
GO
IF OBJECT_ID(N'[dbo].[FK__Image__categoryI__5BAD9CC8]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Image] DROP CONSTRAINT [FK__Image__categoryI__5BAD9CC8];
GO
IF OBJECT_ID(N'[dbo].[FK__Image__userId__5CA1C101]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Image] DROP CONSTRAINT [FK__Image__userId__5CA1C101];
GO
IF OBJECT_ID(N'[dbo].[FK_Like_Image]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Like] DROP CONSTRAINT [FK_Like_Image];
GO
IF OBJECT_ID(N'[dbo].[FK_Like_UserProfile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Like] DROP CONSTRAINT [FK_Like_UserProfile];
GO
IF OBJECT_ID(N'[dbo].[FK_TagImage_Image]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TagImage] DROP CONSTRAINT [FK_TagImage_Image];
GO
IF OBJECT_ID(N'[dbo].[FK_TagImage_Tag]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TagImage] DROP CONSTRAINT [FK_TagImage_Tag];
GO
IF OBJECT_ID(N'[dbo].[FK_UserProfileUserProfile_UserProfile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserProfileUserProfile] DROP CONSTRAINT [FK_UserProfileUserProfile_UserProfile];
GO
IF OBJECT_ID(N'[dbo].[FK_UserProfileUserProfile_UserProfile1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserProfileUserProfile] DROP CONSTRAINT [FK_UserProfileUserProfile_UserProfile1];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Category];
GO
IF OBJECT_ID(N'[dbo].[Comment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comment];
GO
IF OBJECT_ID(N'[dbo].[Follow]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Follow];
GO
IF OBJECT_ID(N'[dbo].[Image]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Image];
GO
IF OBJECT_ID(N'[dbo].[Like]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Like];
GO
IF OBJECT_ID(N'[dbo].[Tag]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tag];
GO
IF OBJECT_ID(N'[dbo].[TagImage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TagImage];
GO
IF OBJECT_ID(N'[dbo].[UserProfile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserProfile];
GO
IF OBJECT_ID(N'[dbo].[UserProfileUserProfile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserProfileUserProfile];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Category'
CREATE TABLE [dbo].[Category] (
    [categoryId] bigint IDENTITY(1,1) NOT NULL,
    [name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Comment'
CREATE TABLE [dbo].[Comment] (
    [commentId] int IDENTITY(1,1) NOT NULL,
    [content] nvarchar(300)  NOT NULL,
    [userId] bigint  NOT NULL,
    [imageId] bigint  NOT NULL,
    [releaseDate] datetime  NOT NULL
);
GO

-- Creating table 'Follow'
CREATE TABLE [dbo].[Follow] (
    [followId] int IDENTITY(1,1) NOT NULL,
    [followerId] bigint  NOT NULL,
    [creatorId] bigint  NOT NULL
);
GO

-- Creating table 'Image'
CREATE TABLE [dbo].[Image] (
    [imageId] bigint IDENTITY(1,1) NOT NULL,
    [path] varchar(255)  NULL,
    [title] nvarchar(100)  NOT NULL,
    [description] nvarchar(100)  NULL,
    [releaseDate] datetime  NOT NULL,
    [apertureSize] decimal(4,2)  NULL,
    [exposureTime] int  NULL,
    [whiteBalance] nvarchar(50)  NULL,
    [categoryId] bigint  NULL,
    [userId] bigint  NULL
);
GO

-- Creating table 'Tag'
CREATE TABLE [dbo].[Tag] (
    [tagId] bigint IDENTITY(1,1) NOT NULL,
    [title] varchar(64)  NOT NULL
);
GO

-- Creating table 'UserProfile'
CREATE TABLE [dbo].[UserProfile] (
    [userId] bigint IDENTITY(1,1) NOT NULL,
    [loginName] varchar(30)  NOT NULL,
    [enPassword] varchar(50)  NOT NULL,
    [firstName] varchar(30)  NOT NULL,
    [lastName] varchar(40)  NOT NULL,
    [email] varchar(60)  NOT NULL,
    [language] varchar(2)  NOT NULL,
    [country] varchar(2)  NOT NULL
);
GO

-- Creating table 'Like'
CREATE TABLE [dbo].[Like] (
    [Image1_imageId] bigint  NOT NULL,
    [UserProfile1_userId] bigint  NOT NULL
);
GO

-- Creating table 'TagImage'
CREATE TABLE [dbo].[TagImage] (
    [Image_imageId] bigint  NOT NULL,
    [Tag_tagId] bigint  NOT NULL
);
GO

-- Creating table 'UserProfileUserProfile'
CREATE TABLE [dbo].[UserProfileUserProfile] (
    [UserProfile2_userId] bigint  NOT NULL,
    [UserProfile1_userId] bigint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [categoryId] in table 'Category'
ALTER TABLE [dbo].[Category]
ADD CONSTRAINT [PK_Category]
    PRIMARY KEY CLUSTERED ([categoryId] ASC);
GO

-- Creating primary key on [commentId] in table 'Comment'
ALTER TABLE [dbo].[Comment]
ADD CONSTRAINT [PK_Comment]
    PRIMARY KEY CLUSTERED ([commentId] ASC);
GO

-- Creating primary key on [followId] in table 'Follow'
ALTER TABLE [dbo].[Follow]
ADD CONSTRAINT [PK_Follow]
    PRIMARY KEY CLUSTERED ([followId] ASC);
GO

-- Creating primary key on [imageId] in table 'Image'
ALTER TABLE [dbo].[Image]
ADD CONSTRAINT [PK_Image]
    PRIMARY KEY CLUSTERED ([imageId] ASC);
GO

-- Creating primary key on [tagId] in table 'Tag'
ALTER TABLE [dbo].[Tag]
ADD CONSTRAINT [PK_Tag]
    PRIMARY KEY CLUSTERED ([tagId] ASC);
GO

-- Creating primary key on [userId] in table 'UserProfile'
ALTER TABLE [dbo].[UserProfile]
ADD CONSTRAINT [PK_UserProfile]
    PRIMARY KEY CLUSTERED ([userId] ASC);
GO

-- Creating primary key on [Image1_imageId], [UserProfile1_userId] in table 'Like'
ALTER TABLE [dbo].[Like]
ADD CONSTRAINT [PK_Like]
    PRIMARY KEY CLUSTERED ([Image1_imageId], [UserProfile1_userId] ASC);
GO

-- Creating primary key on [Image_imageId], [Tag_tagId] in table 'TagImage'
ALTER TABLE [dbo].[TagImage]
ADD CONSTRAINT [PK_TagImage]
    PRIMARY KEY CLUSTERED ([Image_imageId], [Tag_tagId] ASC);
GO

-- Creating primary key on [UserProfile2_userId], [UserProfile1_userId] in table 'UserProfileUserProfile'
ALTER TABLE [dbo].[UserProfileUserProfile]
ADD CONSTRAINT [PK_UserProfileUserProfile]
    PRIMARY KEY CLUSTERED ([UserProfile2_userId], [UserProfile1_userId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [categoryId] in table 'Image'
ALTER TABLE [dbo].[Image]
ADD CONSTRAINT [FK__Image__categoryI__5BAD9CC8]
    FOREIGN KEY ([categoryId])
    REFERENCES [dbo].[Category]
        ([categoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Image__categoryI__5BAD9CC8'
CREATE INDEX [IX_FK__Image__categoryI__5BAD9CC8]
ON [dbo].[Image]
    ([categoryId]);
GO

-- Creating foreign key on [imageId] in table 'Comment'
ALTER TABLE [dbo].[Comment]
ADD CONSTRAINT [FK__Comment__imageId__607251E5]
    FOREIGN KEY ([imageId])
    REFERENCES [dbo].[Image]
        ([imageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Comment__imageId__607251E5'
CREATE INDEX [IX_FK__Comment__imageId__607251E5]
ON [dbo].[Comment]
    ([imageId]);
GO

-- Creating foreign key on [userId] in table 'Comment'
ALTER TABLE [dbo].[Comment]
ADD CONSTRAINT [FK__Comment__userId__5F7E2DAC]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[UserProfile]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Comment__userId__5F7E2DAC'
CREATE INDEX [IX_FK__Comment__userId__5F7E2DAC]
ON [dbo].[Comment]
    ([userId]);
GO

-- Creating foreign key on [creatorId] in table 'Follow'
ALTER TABLE [dbo].[Follow]
ADD CONSTRAINT [FK__Follow__creatorI__1FEDB87C]
    FOREIGN KEY ([creatorId])
    REFERENCES [dbo].[UserProfile]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Follow__creatorI__1FEDB87C'
CREATE INDEX [IX_FK__Follow__creatorI__1FEDB87C]
ON [dbo].[Follow]
    ([creatorId]);
GO

-- Creating foreign key on [followerId] in table 'Follow'
ALTER TABLE [dbo].[Follow]
ADD CONSTRAINT [FK__Follow__follower__1EF99443]
    FOREIGN KEY ([followerId])
    REFERENCES [dbo].[UserProfile]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Follow__follower__1EF99443'
CREATE INDEX [IX_FK__Follow__follower__1EF99443]
ON [dbo].[Follow]
    ([followerId]);
GO

-- Creating foreign key on [userId] in table 'Image'
ALTER TABLE [dbo].[Image]
ADD CONSTRAINT [FK__Image__userId__5CA1C101]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[UserProfile]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Image__userId__5CA1C101'
CREATE INDEX [IX_FK__Image__userId__5CA1C101]
ON [dbo].[Image]
    ([userId]);
GO

-- Creating foreign key on [Image1_imageId] in table 'Like'
ALTER TABLE [dbo].[Like]
ADD CONSTRAINT [FK_Like_Image]
    FOREIGN KEY ([Image1_imageId])
    REFERENCES [dbo].[Image]
        ([imageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserProfile1_userId] in table 'Like'
ALTER TABLE [dbo].[Like]
ADD CONSTRAINT [FK_Like_UserProfile]
    FOREIGN KEY ([UserProfile1_userId])
    REFERENCES [dbo].[UserProfile]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Like_UserProfile'
CREATE INDEX [IX_FK_Like_UserProfile]
ON [dbo].[Like]
    ([UserProfile1_userId]);
GO

-- Creating foreign key on [Image_imageId] in table 'TagImage'
ALTER TABLE [dbo].[TagImage]
ADD CONSTRAINT [FK_TagImage_Image]
    FOREIGN KEY ([Image_imageId])
    REFERENCES [dbo].[Image]
        ([imageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Tag_tagId] in table 'TagImage'
ALTER TABLE [dbo].[TagImage]
ADD CONSTRAINT [FK_TagImage_Tag]
    FOREIGN KEY ([Tag_tagId])
    REFERENCES [dbo].[Tag]
        ([tagId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TagImage_Tag'
CREATE INDEX [IX_FK_TagImage_Tag]
ON [dbo].[TagImage]
    ([Tag_tagId]);
GO

-- Creating foreign key on [UserProfile2_userId] in table 'UserProfileUserProfile'
ALTER TABLE [dbo].[UserProfileUserProfile]
ADD CONSTRAINT [FK_UserProfileUserProfile_UserProfile]
    FOREIGN KEY ([UserProfile2_userId])
    REFERENCES [dbo].[UserProfile]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserProfile1_userId] in table 'UserProfileUserProfile'
ALTER TABLE [dbo].[UserProfileUserProfile]
ADD CONSTRAINT [FK_UserProfileUserProfile_UserProfile1]
    FOREIGN KEY ([UserProfile1_userId])
    REFERENCES [dbo].[UserProfile]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserProfileUserProfile_UserProfile1'
CREATE INDEX [IX_FK_UserProfileUserProfile_UserProfile1]
ON [dbo].[UserProfileUserProfile]
    ([UserProfile1_userId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------