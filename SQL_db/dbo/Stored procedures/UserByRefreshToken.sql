CREATE PROCEDURE [dbo].[UserByRefreshToken]
	@refreshToken nvarchar(450)
AS
	SELECT  TOP 1 * FROM Users WHERE RefreshToken = @refreshToken AND RefreshTokenDate > DATEADD(hh, -1, getdate())