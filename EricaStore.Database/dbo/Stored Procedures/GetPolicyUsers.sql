CREATE PROCEDURE GetPolicyUsers
	@PolicyId int 
	
AS
	SELECT PolicyID,FirstName,LastName FROM Users WHERE PolicyID=@PolicyId