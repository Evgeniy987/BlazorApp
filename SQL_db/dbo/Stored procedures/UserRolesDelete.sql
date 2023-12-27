CREATE PROCEDURE [dbo].[UserRolesDelete]
	@userId int
AS
	DELETE Tags WHERE [ObjId] = @userId AND ObjName = 'User'