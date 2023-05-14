CREATE TABLE [dbo].[History] (
    [History_ID]    INT      IDENTITY (1, 1) NOT NULL,
    [GeneratedTime] DATETIME NOT NULL,
    [Users_ID]      INT      NOT NULL,
    CONSTRAINT [PK_History] PRIMARY KEY CLUSTERED ([History_ID] ASC),
    CONSTRAINT [FK_History_Users] FOREIGN KEY ([Users_ID]) REFERENCES [dbo].[Users] ([Users_ID])
);

