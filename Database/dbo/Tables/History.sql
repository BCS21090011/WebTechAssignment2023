CREATE TABLE [dbo].[History] (
    [History_ID]    INT      IDENTITY (1, 1) NOT NULL,
    [GeneratedTime] DATETIME NOT NULL,
    CONSTRAINT [PK_History] PRIMARY KEY CLUSTERED ([History_ID] ASC),
);

