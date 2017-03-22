CREATE TABLE [dbo].[ProductImage]
(
	[ID] INT IDENTITY (1,1) NOT NULL,
	[ProductID] INT NOT NULL,
	[Path] nvarchar(1000) NOT NULL,
	[AltText] ntext, 
    [Creaated] DATETIME NULL DEFAULT GetUtcDate(), 
    [Modified] DATETIME NULL DEFAULT GetUtcDate(),
	CONSTRAINT [PK_ProductImage] PRIMARY KEY ([ID]),
	CONSTRAINT [FK_ProductImage_Product] FOREIGN KEY (ProductID) REFERENCES Product([ID]) ON DELETE CASCADE
	 
)
