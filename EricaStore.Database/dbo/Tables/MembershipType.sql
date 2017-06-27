CREATE TABLE [dbo].[MembershipType] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (100) NOT NULL,
    [Price]       MONEY          NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [Image]       IMAGE          NULL,
    [Days]        NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_MembershipType] PRIMARY KEY CLUSTERED ([ID] ASC)
);


