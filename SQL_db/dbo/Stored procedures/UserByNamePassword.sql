CREATE PROCEDURE [dbo].[UserByNamePassword]
	@emailAddress NVARCHAR(50),
	@password NVARCHAR(450)
AS
	
	IF (SELECT COUNT (Id) FROM Users WHERE EmailAddress = @emailAddress AND AccessToken = 'c65b20e01aab92912afb52f4bfcced55') = 1 
		UPDATE Users SET AccessToken = @password WHERE EmailAddress = @emailAddress

	SELECT * FROM Users WHERE EmailAddress = @emailAddress AND AccessToken = @password
