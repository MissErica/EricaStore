CREATE TABLE [dbo].[OrderProduct] (
    [OrderID]   INT      NOT NULL,
    [ProductID] INT      NOT NULL,
    [Quantity]  INT      DEFAULT ((1)) NOT NULL,
    [Created]   DATETIME DEFAULT (getutcdate()) NULL,
    [Modified]  DATETIME DEFAULT (getutcdate()) NULL,
    CONSTRAINT [PK_OrderProduct] PRIMARY KEY CLUSTERED ([ProductID] ASC, [OrderID] ASC),
    CONSTRAINT [CK_OrderProduct_Quantity] CHECK ([Quantity]>(0)),
    CONSTRAINT [FK_OrderProduct_Order] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Order] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderProduct_Product] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[MembershipType] ([ID]) ON DELETE CASCADE
);


