CREATE PROCEDURE [dbo].[TagsByObj]
	@objId int,
	@objName varchar(50)
AS
	SELECT * FROM Tags WHERE [ObjId] = @objId AND ObjName = @objName

