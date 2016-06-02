
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/16/2016 10:44:48
-- Generated from EDMX file: C:\Users\Dezmor\Documents\Visual Studio 2015\Projects\CinemaTicketsSeller\CinemaTicketsSeller\CinemaModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CinemaDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_MovieSession]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SessionSet] DROP CONSTRAINT [FK_MovieSession];
GO
IF OBJECT_ID(N'[dbo].[FK_MovieGenreMovie]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MovieSet] DROP CONSTRAINT [FK_MovieGenreMovie];
GO
IF OBJECT_ID(N'[dbo].[FK_SessionTicket]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TicketSet] DROP CONSTRAINT [FK_SessionTicket];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[MovieSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MovieSet];
GO
IF OBJECT_ID(N'[dbo].[MovieGenreSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MovieGenreSet];
GO
IF OBJECT_ID(N'[dbo].[TicketSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TicketSet];
GO
IF OBJECT_ID(N'[dbo].[SessionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SessionSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'MovieSet'
CREATE TABLE [dbo].[MovieSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Duration] time  NOT NULL,
    [StudioName] nvarchar(max)  NOT NULL,
    [Country] nvarchar(max)  NOT NULL,
    [AgeRestrictions] int  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [MovieGenreId] int  NULL
);
GO

-- Creating table 'MovieGenreSet'
CREATE TABLE [dbo].[MovieGenreSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'TicketSet'
CREATE TABLE [dbo].[TicketSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Status] int  NOT NULL,
    [Seat_Row] int  NOT NULL,
    [Seat_Column] int  NOT NULL,
    [SessionId] int  NOT NULL
);
GO

-- Creating table 'SessionSet'
CREATE TABLE [dbo].[SessionSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BeginTime] datetime  NOT NULL,
    [EndTime] datetime  NOT NULL,
    [TicketPrice] float  NOT NULL,
    [MovieId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'MovieSet'
ALTER TABLE [dbo].[MovieSet]
ADD CONSTRAINT [PK_MovieSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MovieGenreSet'
ALTER TABLE [dbo].[MovieGenreSet]
ADD CONSTRAINT [PK_MovieGenreSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TicketSet'
ALTER TABLE [dbo].[TicketSet]
ADD CONSTRAINT [PK_TicketSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SessionSet'
ALTER TABLE [dbo].[SessionSet]
ADD CONSTRAINT [PK_SessionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [MovieId] in table 'SessionSet'
ALTER TABLE [dbo].[SessionSet]
ADD CONSTRAINT [FK_MovieSession]
    FOREIGN KEY ([MovieId])
    REFERENCES [dbo].[MovieSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MovieSession'
CREATE INDEX [IX_FK_MovieSession]
ON [dbo].[SessionSet]
    ([MovieId]);
GO

-- Creating foreign key on [MovieGenreId] in table 'MovieSet'
ALTER TABLE [dbo].[MovieSet]
ADD CONSTRAINT [FK_MovieGenreMovie]
    FOREIGN KEY ([MovieGenreId])
    REFERENCES [dbo].[MovieGenreSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MovieGenreMovie'
CREATE INDEX [IX_FK_MovieGenreMovie]
ON [dbo].[MovieSet]
    ([MovieGenreId]);
GO

-- Creating foreign key on [SessionId] in table 'TicketSet'
ALTER TABLE [dbo].[TicketSet]
ADD CONSTRAINT [FK_SessionTicket]
    FOREIGN KEY ([SessionId])
    REFERENCES [dbo].[SessionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SessionTicket'
CREATE INDEX [IX_FK_SessionTicket]
ON [dbo].[TicketSet]
    ([SessionId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------