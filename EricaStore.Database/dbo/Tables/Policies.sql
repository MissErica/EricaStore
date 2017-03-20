CREATE TABLE [dbo].[Policies] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [Number]        NVARCHAR (100) NOT NULL,
    [EffectiveDate] DATETIME       NULL,
    [AddressId]     INT            NULL,
    CONSTRAINT [PK_Policies] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Policies_Addresses] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Addresses] ([ID])
);

