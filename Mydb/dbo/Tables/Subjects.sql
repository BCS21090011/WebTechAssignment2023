CREATE TABLE [dbo].[Subjects] (
    [Subjects_ID] INT           IDENTITY (1, 1) NOT NULL,
    [SubjectName] VARCHAR (255) NOT NULL,
    CONSTRAINT [PK_Subjects] PRIMARY KEY CLUSTERED ([Subjects_ID] ASC)
);

