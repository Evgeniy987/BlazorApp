CREATE PROCEDURE [dbo].[TagsByObjs]
	@objIdMin int = 0,
	@objIdMax int = 0,
	@objName varchar(50)
AS
	SELECT * FROM Tags WHERE [ObjId] BETWEEN @objIdMin AND @objIdMax AND ObjName = @objName