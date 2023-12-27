CREATE PROCEDURE [dbo].[UserByEmail]
	@email varchar(255)
AS
	SELECT TOP 1 * FROM Users WHERE EmailAddress = @email