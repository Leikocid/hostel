
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/18/2019 20:54:57
-- Generated from EDMX file: D:\work\hostel\HostelApp\HostelApp\Model\HostelModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [hostel];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PersonUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSet] DROP CONSTRAINT [FK_PersonUser];
GO
IF OBJECT_ID(N'[dbo].[FK_HostelRoom]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoomSet] DROP CONSTRAINT [FK_HostelRoom];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonStudent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StudentSet] DROP CONSTRAINT [FK_PersonStudent];
GO
IF OBJECT_ID(N'[dbo].[FK_FacultyGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupSet] DROP CONSTRAINT [FK_FacultyGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupStudent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StudentSet] DROP CONSTRAINT [FK_GroupStudent];
GO
IF OBJECT_ID(N'[dbo].[FK_Mother]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSet] DROP CONSTRAINT [FK_Mother];
GO
IF OBJECT_ID(N'[dbo].[FK_Father]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSet] DROP CONSTRAINT [FK_Father];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomOcupation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OcupationSet] DROP CONSTRAINT [FK_RoomOcupation];
GO
IF OBJECT_ID(N'[dbo].[FK_StudentOcupation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OcupationSet] DROP CONSTRAINT [FK_StudentOcupation];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[HostelSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HostelSet];
GO
IF OBJECT_ID(N'[dbo].[RoomSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoomSet];
GO
IF OBJECT_ID(N'[dbo].[StudentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StudentSet];
GO
IF OBJECT_ID(N'[dbo].[PersonSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonSet];
GO
IF OBJECT_ID(N'[dbo].[FacultySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FacultySet];
GO
IF OBJECT_ID(N'[dbo].[GroupSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GroupSet];
GO
IF OBJECT_ID(N'[dbo].[OcupationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OcupationSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] varchar(200)  NOT NULL,
    [Pasword] varchar(200)  NOT NULL,
    [Active] bit  NOT NULL,
    [Person_Id] int  NULL
);
GO

-- Creating table 'HostelSet'
CREATE TABLE [dbo].[HostelSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(200)  NOT NULL,
    [Address] nvarchar(1000)  NULL
);
GO

-- Creating table 'RoomSet'
CREATE TABLE [dbo].[RoomSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Floor] int  NOT NULL,
    [Number] int  NOT NULL,
    [Capacity] smallint  NOT NULL,
    [Hostel_Id] int  NOT NULL
);
GO

-- Creating table 'StudentSet'
CREATE TABLE [dbo].[StudentSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Active] bit  NOT NULL,
    [Person_Id] int  NOT NULL,
    [Group_Id] int  NOT NULL
);
GO

-- Creating table 'PersonSet'
CREATE TABLE [dbo].[PersonSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(200)  NOT NULL,
    [SecondName] nvarchar(200)  NOT NULL,
    [MiddleName] nvarchar(200)  NULL,
    [Passport] varchar(15)  NOT NULL,
    [RegistrationAddress] nvarchar(1000)  NULL,
    [Mother_Id] int  NULL,
    [Father_Id] int  NULL
);
GO

-- Creating table 'FacultySet'
CREATE TABLE [dbo].[FacultySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(1000)  NOT NULL
);
GO

-- Creating table 'GroupSet'
CREATE TABLE [dbo].[GroupSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StudyYear] tinyint  NOT NULL,
    [Number] int  NOT NULL,
    [Faculty_Id] int  NOT NULL
);
GO

-- Creating table 'OcupationSet'
CREATE TABLE [dbo].[OcupationSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [From] datetime  NOT NULL,
    [To] datetime  NULL,
    [Room_Id] int  NOT NULL,
    [Student_Id] int  NOT NULL
);
GO

-- Creating table 'OrderSet'
CREATE TABLE [dbo].[OrderSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Number] nvarchar(200)  NOT NULL,
    [Price] float  NOT NULL,
    [Ocupation_Id] int  NOT NULL
);
GO

-- Creating table 'PaymentSet'
CREATE TABLE [dbo].[PaymentSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Number] nvarchar(200)  NOT NULL,
    [Amount] float  NOT NULL,
    [Payer_Id] int  NULL,
    [Order_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HostelSet'
ALTER TABLE [dbo].[HostelSet]
ADD CONSTRAINT [PK_HostelSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RoomSet'
ALTER TABLE [dbo].[RoomSet]
ADD CONSTRAINT [PK_RoomSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StudentSet'
ALTER TABLE [dbo].[StudentSet]
ADD CONSTRAINT [PK_StudentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonSet'
ALTER TABLE [dbo].[PersonSet]
ADD CONSTRAINT [PK_PersonSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FacultySet'
ALTER TABLE [dbo].[FacultySet]
ADD CONSTRAINT [PK_FacultySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GroupSet'
ALTER TABLE [dbo].[GroupSet]
ADD CONSTRAINT [PK_GroupSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OcupationSet'
ALTER TABLE [dbo].[OcupationSet]
ADD CONSTRAINT [PK_OcupationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [PK_OrderSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PaymentSet'
ALTER TABLE [dbo].[PaymentSet]
ADD CONSTRAINT [PK_PaymentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Person_Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [FK_PersonUser]
    FOREIGN KEY ([Person_Id])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonUser'
CREATE INDEX [IX_FK_PersonUser]
ON [dbo].[UserSet]
    ([Person_Id]);
GO

-- Creating foreign key on [Hostel_Id] in table 'RoomSet'
ALTER TABLE [dbo].[RoomSet]
ADD CONSTRAINT [FK_HostelRoom]
    FOREIGN KEY ([Hostel_Id])
    REFERENCES [dbo].[HostelSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HostelRoom'
CREATE INDEX [IX_FK_HostelRoom]
ON [dbo].[RoomSet]
    ([Hostel_Id]);
GO

-- Creating foreign key on [Person_Id] in table 'StudentSet'
ALTER TABLE [dbo].[StudentSet]
ADD CONSTRAINT [FK_PersonStudent]
    FOREIGN KEY ([Person_Id])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonStudent'
CREATE INDEX [IX_FK_PersonStudent]
ON [dbo].[StudentSet]
    ([Person_Id]);
GO

-- Creating foreign key on [Faculty_Id] in table 'GroupSet'
ALTER TABLE [dbo].[GroupSet]
ADD CONSTRAINT [FK_FacultyGroup]
    FOREIGN KEY ([Faculty_Id])
    REFERENCES [dbo].[FacultySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FacultyGroup'
CREATE INDEX [IX_FK_FacultyGroup]
ON [dbo].[GroupSet]
    ([Faculty_Id]);
GO

-- Creating foreign key on [Group_Id] in table 'StudentSet'
ALTER TABLE [dbo].[StudentSet]
ADD CONSTRAINT [FK_GroupStudent]
    FOREIGN KEY ([Group_Id])
    REFERENCES [dbo].[GroupSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupStudent'
CREATE INDEX [IX_FK_GroupStudent]
ON [dbo].[StudentSet]
    ([Group_Id]);
GO

-- Creating foreign key on [Mother_Id] in table 'PersonSet'
ALTER TABLE [dbo].[PersonSet]
ADD CONSTRAINT [FK_Mother]
    FOREIGN KEY ([Mother_Id])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Mother'
CREATE INDEX [IX_FK_Mother]
ON [dbo].[PersonSet]
    ([Mother_Id]);
GO

-- Creating foreign key on [Father_Id] in table 'PersonSet'
ALTER TABLE [dbo].[PersonSet]
ADD CONSTRAINT [FK_Father]
    FOREIGN KEY ([Father_Id])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Father'
CREATE INDEX [IX_FK_Father]
ON [dbo].[PersonSet]
    ([Father_Id]);
GO

-- Creating foreign key on [Room_Id] in table 'OcupationSet'
ALTER TABLE [dbo].[OcupationSet]
ADD CONSTRAINT [FK_RoomOcupation]
    FOREIGN KEY ([Room_Id])
    REFERENCES [dbo].[RoomSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomOcupation'
CREATE INDEX [IX_FK_RoomOcupation]
ON [dbo].[OcupationSet]
    ([Room_Id]);
GO

-- Creating foreign key on [Student_Id] in table 'OcupationSet'
ALTER TABLE [dbo].[OcupationSet]
ADD CONSTRAINT [FK_StudentOcupation]
    FOREIGN KEY ([Student_Id])
    REFERENCES [dbo].[StudentSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentOcupation'
CREATE INDEX [IX_FK_StudentOcupation]
ON [dbo].[OcupationSet]
    ([Student_Id]);
GO

-- Creating foreign key on [Ocupation_Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_OcupationOrder]
    FOREIGN KEY ([Ocupation_Id])
    REFERENCES [dbo].[OcupationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OcupationOrder'
CREATE INDEX [IX_FK_OcupationOrder]
ON [dbo].[OrderSet]
    ([Ocupation_Id]);
GO

-- Creating foreign key on [Payer_Id] in table 'PaymentSet'
ALTER TABLE [dbo].[PaymentSet]
ADD CONSTRAINT [FK_PaymentPerson]
    FOREIGN KEY ([Payer_Id])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PaymentPerson'
CREATE INDEX [IX_FK_PaymentPerson]
ON [dbo].[PaymentSet]
    ([Payer_Id]);
GO

-- Creating foreign key on [Order_Id] in table 'PaymentSet'
ALTER TABLE [dbo].[PaymentSet]
ADD CONSTRAINT [FK_PaymentOrder]
    FOREIGN KEY ([Order_Id])
    REFERENCES [dbo].[OrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PaymentOrder'
CREATE INDEX [IX_FK_PaymentOrder]
ON [dbo].[PaymentSet]
    ([Order_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------