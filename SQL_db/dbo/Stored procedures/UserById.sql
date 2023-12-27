CREATE PROCEDURE [dbo].[UserById]
	@id int
AS
	SELECT * FROM Users WHERE Id = @id