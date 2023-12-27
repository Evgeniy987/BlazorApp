CREATE PROCEDURE [dbo].[UserByAccessToken]
	@accessToken nvarchar(450)
AS
	SELECT TOP 1 * FROM Users WHERE AccessToken = @accessToken
