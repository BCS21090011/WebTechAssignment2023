CREATE TABLE [dbo].[Users] (
    [Users_ID]        INT           IDENTITY (1, 1) NOT NULL,
    [UserName]        VARCHAR (255) NULL,
    [UserPassword]    VARCHAR (255) NOT NULL,
    [UserGender]      VARCHAR (255) NULL,
    [UserCountry]     VARCHAR (255) NULL,
    [UserEmail]       VARCHAR (255) NOT NULL,
    [UserPhoneNumber] INT           NULL,
    [UserSchoolName]  VARCHAR (255) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Users_ID] ASC)
);

