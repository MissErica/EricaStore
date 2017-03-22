CREATE TABLE [dbo].[Address] 
(

	[ID]       INT            IDENTITY (1, 1) NOT NULL,
	[Address1] NVARCHAR (100) NOT NULL,
	[Address2] NVARCHAR (100) NULL,
	[City]     NVARCHAR (100) NOT NULL,
	[State]    NVARCHAR (100) NOT NULL,
	[Zipcode]  INT            NOT NULL,
	[Created] DATETIME NULL Default GetUtcDate(),
	[Modified] DATETIME NULL Default GetUtcDate(),

	[AspNetUserID] NVARCHAR(128) NULL,
	CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_Address_AspNetUsers] FOREIGN KEY (AspNetUserID) REFERENCES [AspNetUsers]([ID]) ON DELETE SET NULL
);

