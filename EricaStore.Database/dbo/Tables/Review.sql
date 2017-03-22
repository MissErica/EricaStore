CREATE TABLE [dbo].[Review]
(
	[ID] INT IDENTITY (1,1) NOT NULL,
	[ProductID] INT NOT NULL, 
    [Rating] INT NOT NULL, 
    [Email] NVARCHAR(100) NULL, 
    [Body] NTEXT NULL, 
    [Created] DATETIME NULL DEFAULT GetUtcDate(), 
    [Modied] DATETIME NULL DEFAULT GetUtcDate(),
	CONSTRAINT [PK_Review] PRIMARY KEY ([ID]),
	CONSTRAINT [FK_Review_Product] FOREIGN KEY (ProductID) REFERENCES Product([ID]) ON DELETE CASCADE,
)
