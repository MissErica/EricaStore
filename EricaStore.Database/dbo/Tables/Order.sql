CREATE TABLE [dbo].[Order]
(
	[ID] INT IDENTITY(1,1) NOT NULL, 
    [OrderDate] TIMESTAMP NOT NULL , 
    [DeliveryAddressID] INT NULL, 
    [BillingAddressID] INT NULL, 
	[PurchaseEmailAddress] NVARCHAR(100) NULL,
    [ConfirmationNumber] UNIQUEIDENTIFIER NOT NULL DEFAULT NewID(),
	[Completed] DATETIME NULL, 
    [Created] DATETIME NULL DEFAULT GetUtcDate(), 
    [Modified] DATETIME NULL DEFAULT GetUtcDate(), 

	[AspNetUserID] NVARCHAR(128) NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY ([ID]),
	CONSTRAINT [FK_Order_BillingAddress] FOREIGN KEY (BillingAddressID) REFERENCES [Address]([ID]),
	CONSTRAINT [FK_Order_DeliveryAddress] FOREIGN KEY (DeliveryAddressID) REFERENCES [Address]([ID]),
	CONSTRAINT [FK_Order_AspNetUsers] FOREIGN KEY (AspNetUserID) REFERENCES [AspNetUsers]([ID]) ON DELETE SET NULL
)
