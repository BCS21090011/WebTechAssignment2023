CREATE TABLE [dbo].[Questions] (
    [Questions_ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Question]              VARCHAR (255) NOT NULL,
    [QuestionMark]          INT           NOT NULL,
    [QuestionDifficulty]    INT           NOT NULL,
    [Subjects_ID]           INT           NOT NULL,
    [QuestionImageFileName] VARCHAR (255) DEFAULT (NULL) NULL,
    [Answer]                VARCHAR (255) NOT NULL,
    [AnswerImageFileName]   VARCHAR (255) DEFAULT (NULL) NULL,
    CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED ([Questions_ID] ASC),
    CONSTRAINT [FK_Questions_Subjects] FOREIGN KEY ([Subjects_ID]) REFERENCES [dbo].[Subjects] ([Subjects_ID])
);

