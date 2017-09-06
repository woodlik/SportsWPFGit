
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/01/2017 14:39:46
-- Generated from EDMX file: D:\C# лабы\Sports\DataBase\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Sports];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ContractClient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Contracts] DROP CONSTRAINT [FK_ContractClient];
GO
IF OBJECT_ID(N'[dbo].[FK_ContractTrainer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Contracts] DROP CONSTRAINT [FK_ContractTrainer];
GO
IF OBJECT_ID(N'[dbo].[FK_ServiceContract]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Contracts] DROP CONSTRAINT [FK_ServiceContract];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Clients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clients];
GO
IF OBJECT_ID(N'[dbo].[Contracts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contracts];
GO
IF OBJECT_ID(N'[dbo].[Services]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Services];
GO
IF OBJECT_ID(N'[dbo].[Trainers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trainers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Clients'
CREATE TABLE [dbo].[Clients] (
    [ClientId] int IDENTITY(1,1) NOT NULL,
    [Family] varchar(50)  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [ThirdName] varchar(50)  NOT NULL,
    [Adress] varchar(50)  NULL,
    [Phone] varchar(20)  NOT NULL
);
GO

-- Creating table 'Contracts'
CREATE TABLE [dbo].[Contracts] (
    [ContractId] int IDENTITY(1,1) NOT NULL,
    [DateStart] datetime  NOT NULL,
    [Quantity] int  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Payment] varchar(50)  NOT NULL,
    [ClientId] int  NOT NULL,
    [TrainerId] int  NOT NULL,
    [ServiceId] int  NOT NULL,
    [DateEnd] datetime  NOT NULL,
    [Sum] float  NOT NULL
);
GO

-- Creating table 'Services'
CREATE TABLE [dbo].[Services] (
    [ServiceId] int IDENTITY(1,1) NOT NULL,
    [Title] varchar(50)  NOT NULL,
    [Price] float  NOT NULL
);
GO

-- Creating table 'Trainers'
CREATE TABLE [dbo].[Trainers] (
    [TrainerId] int IDENTITY(1,1) NOT NULL,
    [Family] varchar(50)  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [ThirdName] varchar(50)  NOT NULL,
    [Post] varchar(50)  NOT NULL,
    [Phone] varchar(20)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ClientId] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [PK_Clients]
    PRIMARY KEY CLUSTERED ([ClientId] ASC);
GO

-- Creating primary key on [ContractId] in table 'Contracts'
ALTER TABLE [dbo].[Contracts]
ADD CONSTRAINT [PK_Contracts]
    PRIMARY KEY CLUSTERED ([ContractId] ASC);
GO

-- Creating primary key on [ServiceId] in table 'Services'
ALTER TABLE [dbo].[Services]
ADD CONSTRAINT [PK_Services]
    PRIMARY KEY CLUSTERED ([ServiceId] ASC);
GO

-- Creating primary key on [TrainerId] in table 'Trainers'
ALTER TABLE [dbo].[Trainers]
ADD CONSTRAINT [PK_Trainers]
    PRIMARY KEY CLUSTERED ([TrainerId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ClientId] in table 'Contracts'
ALTER TABLE [dbo].[Contracts]
ADD CONSTRAINT [FK_ContractClient]
    FOREIGN KEY ([ClientId])
    REFERENCES [dbo].[Clients]
        ([ClientId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContractClient'
CREATE INDEX [IX_FK_ContractClient]
ON [dbo].[Contracts]
    ([ClientId]);
GO

-- Creating foreign key on [TrainerId] in table 'Contracts'
ALTER TABLE [dbo].[Contracts]
ADD CONSTRAINT [FK_ContractTrainer]
    FOREIGN KEY ([TrainerId])
    REFERENCES [dbo].[Trainers]
        ([TrainerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContractTrainer'
CREATE INDEX [IX_FK_ContractTrainer]
ON [dbo].[Contracts]
    ([TrainerId]);
GO

-- Creating foreign key on [ServiceId] in table 'Contracts'
ALTER TABLE [dbo].[Contracts]
ADD CONSTRAINT [FK_ServiceContract]
    FOREIGN KEY ([ServiceId])
    REFERENCES [dbo].[Services]
        ([ServiceId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ServiceContract'
CREATE INDEX [IX_FK_ServiceContract]
ON [dbo].[Contracts]
    ([ServiceId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------