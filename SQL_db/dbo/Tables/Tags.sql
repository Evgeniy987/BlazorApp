CREATE TABLE [dbo].[Tags]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ObjId] INT NOT NULL, 
    [ObjName] NVARCHAR(50) NOT NULL, 
    [CreatedAt] DATETIME NOT NULL DEFAULT getdate(), 
    [CreatedBy] NVARCHAR(50) NULL, 
    [Title] NVARCHAR(50) NULL, 
    [BadgeColor] INT NULL, 
    [IsRemovable] BIT NULL 
)