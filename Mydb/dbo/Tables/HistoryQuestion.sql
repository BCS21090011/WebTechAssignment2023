CREATE TABLE [dbo].[HistoryQuestion] (
    [HistoryQuestion_ID] INT IDENTITY (1, 1) NOT NULL,
    [History_ID]         INT NOT NULL,
    [Questions_ID]       INT NOT NULL,
    [Question_Number]    INT NOT NULL,
    PRIMARY KEY CLUSTERED ([HistoryQuestion_ID] ASC),
    FOREIGN KEY ([History_ID]) REFERENCES [dbo].[History] ([History_ID]),
    FOREIGN KEY ([Questions_ID]) REFERENCES [dbo].[Questions] ([Questions_ID])
);

