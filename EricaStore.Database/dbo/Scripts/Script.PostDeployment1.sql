/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
INSERT INTO Product(Name, Price, Description) VALUES
('Rump Shaker', 9.99, 'amazing yummy great cool good' ),
('Simply the Best', 9.99, 'Simply the Best'),
('Welcome to the Jungle', 9.99, ''),
('Moondance', 9.99, ''),
('Yellow Submarine', 9.99, ''),
('Uptown Girl', 9.99, '')

INSERT INTO ProductImage(ProductID, Path) VALUES
((SELECT TOP 1 ID FROM Product WHERE Name = 'Rump Shaker'), '/Content/Images/rhubarbpear.jpeg'),
((SELECT TOP 1 ID FROM Product WHERE Name = 'Simply the Best'), '/Content/Images/healthy-people-woman-girl.jpg'),
((SELECT TOP 1 ID FROM Product WHERE Name = 'Welcome to the Jungle'), '/Content/Images/rhubarbpear.jpeg'),
((SELECT TOP 1 ID FROM Product WHERE Name = 'Moondance'), '/Content/Images/rhubarbpear.jpeg'),
((SELECT TOP 1 ID FROM Product WHERE Name = 'Yellow Submarine'), '/Content/Images/rhubarbpear.jpeg'),
((SELECT TOP 1 ID FROM Product WHERE Name = 'Uptown Girl'), '/Content/Images/rhubarbpear.jpeg')

INSERT INTO MembershipType( Name, Price) VALUES
('Ulitmate Juice Fix', 150),
('Triple Booster Package', 115),
('Weekend Warrior', 55)
            