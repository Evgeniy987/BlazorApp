CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [LongName] NVARCHAR(500) NULL, 
    [Notes] NVARCHAR(MAX) NULL, 
    [CreatedAt] DATETIME NOT NULL DEFAULT getdate(), 
    [CreatedBy] NVARCHAR(255) NULL, 
    [PhoneNumber] NVARCHAR(255) NULL, 
    [EmailAddress] NVARCHAR(255) NULL, 
    [BadgeColor] INT NULL, 
    [AccessToken] NVARCHAR(450) NULL, 
    [RefreshToken] NVARCHAR(450) NULL,
    [IsDisabled] BIT NOT NULL DEFAULT 0, 
    [DisabledBy] NVARCHAR(255) NULL, 
    [BadgeText] NVARCHAR(10) NULL, 
    [RefreshTokenDate] DATETIME NULL
)