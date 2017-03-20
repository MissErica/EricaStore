CREATE PROCEDURE GetUserAddress
	@UserId int
	
AS
	SELECT ID,Address1, Address2,City, State, Zipcode FROM Addresses WHERE @UserId=ID