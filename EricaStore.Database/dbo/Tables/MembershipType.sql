CREATE TABLE [dbo].[MembershipType]
(
	[ID] INT IDENTITY (1,1) NOT NULL, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Price] MONEY NOT NULL,
	CONSTRAINT [PK_MembershipType] PRIMARY KEY ([ID]),

)
