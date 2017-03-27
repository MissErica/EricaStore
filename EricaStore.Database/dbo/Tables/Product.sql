CREATE TABLE [dbo].[Product]
(
	[ID] INT IDENTITY(1,1) NOT NULL, 
	[Name] NVARCHAR(100) NOT NULL, 
	[Price] MONEY NOT NULL, 
	[Description] NTEXT NULL,
	[Created] DATETIME NULL DEFAULT GetUtcDate(), 
	[Modified] DATETIME NULL DEFAULT GetUtcDate(), 
	[Category] NVARCHAR(100) NULL, 
    [Ingredients] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_Product] PRIMARY KEY ([ID]),
	


   
)
