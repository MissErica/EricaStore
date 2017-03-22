INSERT INTO Product (ID, Name, Price, [Description], MembershipType)
VALUES (1, "Entry", 100.00, "Entry point for delivery service", "Auto");






CREATE PROCEDURE [dbo].[sp_CompleteOrder]
	@orderID int = 0
AS

	BEGIN TRANSACTION COMPLETEORDER
		BEGIN TRY
			UPDATE
				[Order]
			SET
				Completed = GETUTCDATE() 
			WHERE 
				ID = @orderID 

			UPDATE 
				Product
			SET 
				Product.Inventory = Product.Inventory - OrderProduct.Quantity
			FROM
				Product inner join OrderProduct on OrderProduct.ProductID = Product.ID 
			WHERE
				OrderProduct.OrderID = @orderId


			COMMIT TRANSACTION COMPLETEORDER
			RETURN 0
		END TRY

		BEGIN CATCH
			ROLLBACK TRANSACTION COMPLETEORDER
			RETURN 1
		END CATCH
