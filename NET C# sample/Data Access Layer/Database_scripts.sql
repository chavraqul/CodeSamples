----------------------------------------
----------------------------------------
--Name: Release Management database scripts
--Last Updated: Aug. 2, 2016
----------------------------------------

----------------------------------------
----LOOK UP / REFERENCE TABLES----------
----------------------------------------
--Users Table
CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1, 1), 
    [UserID] NVARCHAR(100) NOT NULL,
    [UserType] NVARCHAR(30) NOT NULL,
	[UserFirstName] NVARCHAR(100) NOT NULL,
	[UserLastName] NVARCHAR(100) NOT NULL,
	[UserEmail] NVARCHAR(255) NULL,
	[ActiveInd] NCHAR(1) NOT NULL,
    [LastAccessed] DATETIME NULL

)

----------------------------------------
--Application Table

CREATE TABLE [dbo].[Application]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1, 1), 
    [EPRId] NVARCHAR(50) NOT NULL, 
    [ApplicationName] NVARCHAR(255) NOT NULL, 
    [ApplicationAlias] NVARCHAR(255) NULL,
	[CreatedBy] INT NOT NULL,
	[CreatedDate] DATETIME NOT NULL,
	[UpdatedBy] INT NOT NULL,
	[UpdatedDate] DATETIME NOT NULL,
	
	CONSTRAINT [FK_Application_UsersCreate] FOREIGN KEY ([CreatedBy]) REFERENCES [Users]([Id]), 
    CONSTRAINT [FK_Application_UsersUpdate] FOREIGN KEY ([UpdatedBy]) REFERENCES [Users]([Id]) 

)