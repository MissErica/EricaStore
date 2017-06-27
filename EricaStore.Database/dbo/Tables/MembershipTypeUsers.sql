CREATE TABLE [dbo].[MembershipTypeUsers]
(
	[ID] INT IDENTITY(1,1) NOT NULL,
	[MembershipID] INT NOT NULL,
	[UserID] NVARCHAR(128) NOT NULL,
	StartDate DATETIME NOT NULL,
	EndDate DATETIME NULL, 
    CONSTRAINT [PK_MembershipTypeUsers] PRIMARY KEY ([ID]), 
    CONSTRAINT [FK_MembershipTypeUsers_MembershipType] FOREIGN KEY (MembershipID) REFERENCES MembershipType(ID),
	 CONSTRAINT [FK_MembershipTypeUsers_AspNetUsers] FOREIGN KEY (UserID) REFERENCES AspNetUsers(ID)

)
