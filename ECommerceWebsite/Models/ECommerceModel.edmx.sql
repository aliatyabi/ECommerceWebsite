
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/22/2017 07:47:10
-- Generated from EDMX file: D:\Ali\My Projects\ECommerceWebsite\ECommerceWebsite\Models\ECommerceModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ECommerceWebsite];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Cities_States]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cities] DROP CONSTRAINT [FK_Cities_States];
GO
IF OBJECT_ID(N'[dbo].[FK_Feature_Values_Features]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Feature_Values] DROP CONSTRAINT [FK_Feature_Values_Features];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Categories_Product_Categories1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product_Categories] DROP CONSTRAINT [FK_Product_Categories_Product_Categories1];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Gallery_Products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product_Gallery] DROP CONSTRAINT [FK_Product_Gallery_Products];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Reviews_Product_Reviews1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product_Reviews] DROP CONSTRAINT [FK_Product_Reviews_Product_Reviews1];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Reviews_Products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product_Reviews] DROP CONSTRAINT [FK_Product_Reviews_Products];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Tags_Products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product_Tags] DROP CONSTRAINT [FK_Product_Tags_Products];
GO
IF OBJECT_ID(N'[dbo].[FK_Products_Features_Feature_Values]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products_Features] DROP CONSTRAINT [FK_Products_Features_Feature_Values];
GO
IF OBJECT_ID(N'[dbo].[FK_Products_Features_Products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products_Features] DROP CONSTRAINT [FK_Products_Features_Products];
GO
IF OBJECT_ID(N'[dbo].[FK_Products_Product_Categories]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_Products_Product_Categories];
GO
IF OBJECT_ID(N'[dbo].[FK_Products_Product_Categories1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_Products_Product_Categories1];
GO
IF OBJECT_ID(N'[dbo].[FK_Products_Product_Categories2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_Products_Product_Categories2];
GO
IF OBJECT_ID(N'[dbo].[FK_Products_Product_Categories3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_Products_Product_Categories3];
GO
IF OBJECT_ID(N'[dbo].[FK_Users_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_Roles];
GO
IF OBJECT_ID(N'[dbo].[FK_UsersProfile_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsersProfile] DROP CONSTRAINT [FK_UsersProfile_Users];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Cities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cities];
GO
IF OBJECT_ID(N'[dbo].[Feature_Values]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Feature_Values];
GO
IF OBJECT_ID(N'[dbo].[Features]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Features];
GO
IF OBJECT_ID(N'[dbo].[News]', 'U') IS NOT NULL
    DROP TABLE [dbo].[News];
GO
IF OBJECT_ID(N'[dbo].[Product_Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product_Categories];
GO
IF OBJECT_ID(N'[dbo].[Product_Gallery]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product_Gallery];
GO
IF OBJECT_ID(N'[dbo].[Product_Reviews]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product_Reviews];
GO
IF OBJECT_ID(N'[dbo].[Product_Tags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product_Tags];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[Products_Features]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products_Features];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Sliders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sliders];
GO
IF OBJECT_ID(N'[dbo].[States]', 'U') IS NOT NULL
    DROP TABLE [dbo].[States];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[UsersProfile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsersProfile];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [RoleId] int  NOT NULL,
    [RoleTitle] varchar(150)  NOT NULL,
    [RoleName] nvarchar(150)  NOT NULL
);
GO

-- Creating table 'UsersProfile'
CREATE TABLE [dbo].[UsersProfile] (
    [UserId] int  NOT NULL,
    [FirstName] nvarchar(50)  NOT NULL,
    [LastName] nvarchar(50)  NOT NULL,
    [NationalCode] nvarchar(10)  NOT NULL,
    [Tel] nvarchar(8)  NOT NULL,
    [Mobile] nvarchar(11)  NOT NULL,
    [Birthday] datetime  NULL,
    [Gender] bit  NOT NULL,
    [State] nvarchar(50)  NOT NULL,
    [City] nvarchar(50)  NOT NULL,
    [Address] nvarchar(500)  NOT NULL,
    [Avatar] varchar(50)  NULL,
    [NewsletterMembership] bit  NOT NULL,
    [Email] nvarchar(150)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [RoleId] int  NOT NULL,
    [Email] nvarchar(150)  NOT NULL,
    [Password] nvarchar(150)  NOT NULL,
    [ActivationCode] varchar(100)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [RegisterationDate] datetime  NOT NULL
);
GO

-- Creating table 'Cities'
CREATE TABLE [dbo].[Cities] (
    [CityId] int  NOT NULL,
    [CityName] nvarchar(50)  NOT NULL,
    [StateId] int  NOT NULL
);
GO

-- Creating table 'States'
CREATE TABLE [dbo].[States] (
    [StateId] int  NOT NULL,
    [StateName] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Sliders'
CREATE TABLE [dbo].[Sliders] (
    [SlideId] int IDENTITY(1,1) NOT NULL,
    [SlideTitle] nvarchar(50)  NOT NULL,
    [Link] varchar(500)  NULL,
    [Image] nvarchar(250)  NOT NULL,
    [ClickCount] int  NOT NULL,
    [NewTab] bit  NOT NULL
);
GO

-- Creating table 'News'
CREATE TABLE [dbo].[News] (
    [NewsId] int  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [ShortDescription] nvarchar(300)  NOT NULL,
    [Text] nvarchar(max)  NULL,
    [NumberOfVisits] int  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [ImageName] nvarchar(250)  NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'Product_Categories'
CREATE TABLE [dbo].[Product_Categories] (
    [CategoryId] int IDENTITY(1,1) NOT NULL,
    [CategoryTitle] nvarchar(150)  NOT NULL,
    [ParentId] int  NULL
);
GO

-- Creating table 'Product_Gallery'
CREATE TABLE [dbo].[Product_Gallery] (
    [GalleryId] int IDENTITY(1,1) NOT NULL,
    [ProductId] int  NOT NULL,
    [ImageName] varchar(50)  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [ProductId] int IDENTITY(1,1) NOT NULL,
    [CategoryId] int  NOT NULL,
    [SubCategoryId] int  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [ShortDescription] nvarchar(300)  NOT NULL,
    [Text] nvarchar(max)  NULL,
    [ImageName] varchar(50)  NOT NULL,
    [Fee] int  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [FurtherSubCategoryId] int  NOT NULL,
    [FurthestSubCategoryId] int  NOT NULL,
    [Status] tinyint  NOT NULL,
    [Comment] nvarchar(max)  NULL
);
GO

-- Creating table 'Product_Tags'
CREATE TABLE [dbo].[Product_Tags] (
    [TagId] int IDENTITY(1,1) NOT NULL,
    [ProductId] int  NOT NULL,
    [MetaTitle] nvarchar(150)  NOT NULL,
    [MetaDescription] nvarchar(160)  NOT NULL,
    [MetaKeyword] nvarchar(150)  NULL
);
GO

-- Creating table 'Feature_Values'
CREATE TABLE [dbo].[Feature_Values] (
    [FeatureValueId] int IDENTITY(1,1) NOT NULL,
    [FeatureId] int  NOT NULL,
    [FeatureValue] nvarchar(150)  NOT NULL,
    [FeatureSimpleValue] nvarchar(50)  NULL
);
GO

-- Creating table 'Features'
CREATE TABLE [dbo].[Features] (
    [FeatureId] int IDENTITY(1,1) NOT NULL,
    [FeatureTitle] nvarchar(150)  NOT NULL
);
GO

-- Creating table 'Products_Features'
CREATE TABLE [dbo].[Products_Features] (
    [ProductFeatureId] int IDENTITY(1,1) NOT NULL,
    [ProductId] int  NOT NULL,
    [FeatureValueId] int  NOT NULL
);
GO

-- Creating table 'Product_Reviews'
CREATE TABLE [dbo].[Product_Reviews] (
    [ReviewId] int IDENTITY(1,1) NOT NULL,
    [ProductId] int  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Email] varchar(150)  NULL,
    [Rating] smallint  NULL,
    [ReviewText] nvarchar(500)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [ParentId] int  NULL,
    [CreatedDate] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [RoleId] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([RoleId] ASC);
GO

-- Creating primary key on [UserId] in table 'UsersProfile'
ALTER TABLE [dbo].[UsersProfile]
ADD CONSTRAINT [PK_UsersProfile]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [CityId] in table 'Cities'
ALTER TABLE [dbo].[Cities]
ADD CONSTRAINT [PK_Cities]
    PRIMARY KEY CLUSTERED ([CityId] ASC);
GO

-- Creating primary key on [StateId] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [PK_States]
    PRIMARY KEY CLUSTERED ([StateId] ASC);
GO

-- Creating primary key on [SlideId] in table 'Sliders'
ALTER TABLE [dbo].[Sliders]
ADD CONSTRAINT [PK_Sliders]
    PRIMARY KEY CLUSTERED ([SlideId] ASC);
GO

-- Creating primary key on [NewsId] in table 'News'
ALTER TABLE [dbo].[News]
ADD CONSTRAINT [PK_News]
    PRIMARY KEY CLUSTERED ([NewsId] ASC);
GO

-- Creating primary key on [CategoryId] in table 'Product_Categories'
ALTER TABLE [dbo].[Product_Categories]
ADD CONSTRAINT [PK_Product_Categories]
    PRIMARY KEY CLUSTERED ([CategoryId] ASC);
GO

-- Creating primary key on [GalleryId] in table 'Product_Gallery'
ALTER TABLE [dbo].[Product_Gallery]
ADD CONSTRAINT [PK_Product_Gallery]
    PRIMARY KEY CLUSTERED ([GalleryId] ASC);
GO

-- Creating primary key on [ProductId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([ProductId] ASC);
GO

-- Creating primary key on [TagId] in table 'Product_Tags'
ALTER TABLE [dbo].[Product_Tags]
ADD CONSTRAINT [PK_Product_Tags]
    PRIMARY KEY CLUSTERED ([TagId] ASC);
GO

-- Creating primary key on [FeatureValueId] in table 'Feature_Values'
ALTER TABLE [dbo].[Feature_Values]
ADD CONSTRAINT [PK_Feature_Values]
    PRIMARY KEY CLUSTERED ([FeatureValueId] ASC);
GO

-- Creating primary key on [FeatureId] in table 'Features'
ALTER TABLE [dbo].[Features]
ADD CONSTRAINT [PK_Features]
    PRIMARY KEY CLUSTERED ([FeatureId] ASC);
GO

-- Creating primary key on [ProductFeatureId] in table 'Products_Features'
ALTER TABLE [dbo].[Products_Features]
ADD CONSTRAINT [PK_Products_Features]
    PRIMARY KEY CLUSTERED ([ProductFeatureId] ASC);
GO

-- Creating primary key on [ReviewId] in table 'Product_Reviews'
ALTER TABLE [dbo].[Product_Reviews]
ADD CONSTRAINT [PK_Product_Reviews]
    PRIMARY KEY CLUSTERED ([ReviewId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [RoleId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_Users_Roles]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]
        ([RoleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Users_Roles'
CREATE INDEX [IX_FK_Users_Roles]
ON [dbo].[Users]
    ([RoleId]);
GO

-- Creating foreign key on [UserId] in table 'UsersProfile'
ALTER TABLE [dbo].[UsersProfile]
ADD CONSTRAINT [FK_UsersProfile_Users]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [StateId] in table 'Cities'
ALTER TABLE [dbo].[Cities]
ADD CONSTRAINT [FK_Cities_States]
    FOREIGN KEY ([StateId])
    REFERENCES [dbo].[States]
        ([StateId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Cities_States'
CREATE INDEX [IX_FK_Cities_States]
ON [dbo].[Cities]
    ([StateId]);
GO

-- Creating foreign key on [ParentId] in table 'Product_Categories'
ALTER TABLE [dbo].[Product_Categories]
ADD CONSTRAINT [FK_Product_Categories_Product_Categories1]
    FOREIGN KEY ([ParentId])
    REFERENCES [dbo].[Product_Categories]
        ([CategoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Categories_Product_Categories1'
CREATE INDEX [IX_FK_Product_Categories_Product_Categories1]
ON [dbo].[Product_Categories]
    ([ParentId]);
GO

-- Creating foreign key on [CategoryId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Products_Product_Categories]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Product_Categories]
        ([CategoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Products_Product_Categories'
CREATE INDEX [IX_FK_Products_Product_Categories]
ON [dbo].[Products]
    ([CategoryId]);
GO

-- Creating foreign key on [SubCategoryId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Products_Product_Categories1]
    FOREIGN KEY ([SubCategoryId])
    REFERENCES [dbo].[Product_Categories]
        ([CategoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Products_Product_Categories1'
CREATE INDEX [IX_FK_Products_Product_Categories1]
ON [dbo].[Products]
    ([SubCategoryId]);
GO

-- Creating foreign key on [ProductId] in table 'Product_Gallery'
ALTER TABLE [dbo].[Product_Gallery]
ADD CONSTRAINT [FK_Product_Gallery_Products]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Gallery_Products'
CREATE INDEX [IX_FK_Product_Gallery_Products]
ON [dbo].[Product_Gallery]
    ([ProductId]);
GO

-- Creating foreign key on [FurtherSubCategoryId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Products_Product_Categories2]
    FOREIGN KEY ([FurtherSubCategoryId])
    REFERENCES [dbo].[Product_Categories]
        ([CategoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Products_Product_Categories2'
CREATE INDEX [IX_FK_Products_Product_Categories2]
ON [dbo].[Products]
    ([FurtherSubCategoryId]);
GO

-- Creating foreign key on [FurthestSubCategoryId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Products_Product_Categories3]
    FOREIGN KEY ([FurthestSubCategoryId])
    REFERENCES [dbo].[Product_Categories]
        ([CategoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Products_Product_Categories3'
CREATE INDEX [IX_FK_Products_Product_Categories3]
ON [dbo].[Products]
    ([FurthestSubCategoryId]);
GO

-- Creating foreign key on [ProductId] in table 'Product_Tags'
ALTER TABLE [dbo].[Product_Tags]
ADD CONSTRAINT [FK_Product_Tags_Products]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Tags_Products'
CREATE INDEX [IX_FK_Product_Tags_Products]
ON [dbo].[Product_Tags]
    ([ProductId]);
GO

-- Creating foreign key on [FeatureId] in table 'Feature_Values'
ALTER TABLE [dbo].[Feature_Values]
ADD CONSTRAINT [FK_Feature_Values_Features]
    FOREIGN KEY ([FeatureId])
    REFERENCES [dbo].[Features]
        ([FeatureId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Feature_Values_Features'
CREATE INDEX [IX_FK_Feature_Values_Features]
ON [dbo].[Feature_Values]
    ([FeatureId]);
GO

-- Creating foreign key on [FeatureValueId] in table 'Products_Features'
ALTER TABLE [dbo].[Products_Features]
ADD CONSTRAINT [FK_Products_Features_Feature_Values]
    FOREIGN KEY ([FeatureValueId])
    REFERENCES [dbo].[Feature_Values]
        ([FeatureValueId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Products_Features_Feature_Values'
CREATE INDEX [IX_FK_Products_Features_Feature_Values]
ON [dbo].[Products_Features]
    ([FeatureValueId]);
GO

-- Creating foreign key on [ProductId] in table 'Products_Features'
ALTER TABLE [dbo].[Products_Features]
ADD CONSTRAINT [FK_Products_Features_Products]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Products_Features_Products'
CREATE INDEX [IX_FK_Products_Features_Products]
ON [dbo].[Products_Features]
    ([ProductId]);
GO

-- Creating foreign key on [ParentId] in table 'Product_Reviews'
ALTER TABLE [dbo].[Product_Reviews]
ADD CONSTRAINT [FK_Product_Reviews_Product_Reviews1]
    FOREIGN KEY ([ParentId])
    REFERENCES [dbo].[Product_Reviews]
        ([ReviewId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Reviews_Product_Reviews1'
CREATE INDEX [IX_FK_Product_Reviews_Product_Reviews1]
ON [dbo].[Product_Reviews]
    ([ParentId]);
GO

-- Creating foreign key on [ProductId] in table 'Product_Reviews'
ALTER TABLE [dbo].[Product_Reviews]
ADD CONSTRAINT [FK_Product_Reviews_Products]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Reviews_Products'
CREATE INDEX [IX_FK_Product_Reviews_Products]
ON [dbo].[Product_Reviews]
    ([ProductId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------