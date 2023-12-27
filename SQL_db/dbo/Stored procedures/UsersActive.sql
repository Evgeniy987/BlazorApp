CREATE PROCEDURE [dbo].[UsersActive]
	
AS
	SELECT * FROM Users WHERE IsDisabled = 0