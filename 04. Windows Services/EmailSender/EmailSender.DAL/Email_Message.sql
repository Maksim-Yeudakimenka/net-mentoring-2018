CREATE TABLE [dbo].[Email_Message] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Subject]   VARCHAR (500) NOT NULL,
    [Body]      VARCHAR (MAX) NOT NULL,
    [Recipient] VARCHAR (MAX) NOT NULL,
    [Completed] BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

