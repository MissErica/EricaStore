CREATE TABLE [dbo].[MembershipTypeProduct]
(
MembershipID INT NOT NULL ,
ProductID INT NOT NULL, 
    CONSTRAINT [PK_MembershipTypeProduct] PRIMARY KEY ([ProductID], [MembershipID]), 
    CONSTRAINT [FK_MembershipTypeProduct_MembershipType] FOREIGN KEY ([MembershipID]) REFERENCES [MembershipType]([ID]), 
    CONSTRAINT [FK_MembershipTypeProduct_Product] FOREIGN KEY ([ProductID]) REFERENCES [Product]([ID]),

)
