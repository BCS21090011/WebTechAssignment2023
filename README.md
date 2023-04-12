# **WebTechAssignment2023**

## **Description**

This is for Web Technology assignment.

---

## **Links**

* [GitHub](https://github.com/BCS21090011/WebTechAssignment2023.git)
* [Figma](https://www.figma.com/file/0grXPVhB7sIiSKGHO9Q1H9/Untitled?node-id=15%3A3&t=pVewqGUSb8sBxukY-1)

---

## **TODO**

* [ ] Create all webpages.
* [ ] Webpages need to be "beautiful" enough.
* [ ] Create admin panel.
* [ ] Modify the sidebar.
* [ ] Make the pages more presentable.
* [ ] Add the function to generate the pdf files (both question and answer).
* [ ] Link to the database (online).
> Currently link offline, need to create database locally. More information in [**Local database**](#Local-database) section.
* [ ] Push the project online.
* [ ] Submit the project.

## **Pages needed**

* [ ] Login page
> Need to make it more presentable.
* [ ] Register page
> Need to make it more presentable.
* [ ] Main page
* [ ] User profile page
* [ ] Insert question page
* [ ] Choose topic page
* [ ] Create question paper page
> * Need to make it more presentable.
> * Need to add in the function to generate pdf files.
  
## **Local database**

Need to install SSMS first, [guide for installation](https://blog.csdn.net/weixin_43074474/article/details/105106894).

Codes to create database:

```MSSQL
CREATE DATABASE WebTechAssignmentDB;
Go
USE WebTechAssignmentDB;
GO

IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL
    DROP TABLE dbo.Users;
CREATE TABLE dbo.Users(
    U_ID INT IDENTITY(1,1) NOT NULL,
    UserName VARCHAR(255) NOT NULL,
    UserPassword VARCHAR(255) NOT NULL,
    UserEmail VARCHAR(255) DEFAULT NULL,
    CONSTRAINT PK_Users PRIMARY KEY CLUSTERED (U_ID)
);

IF OBJECT_ID('dbo.Subjects', 'U') IS NOT NULL
    DROP TABLE dbo.Subjects;
CREATE TABLE dbo.Subjects(
    S_ID INT IDENTITY(1,1) NOT NULL,
    SubjectName VARCHAR(255) NOT NULL,
    CONSTRAINT PK_Subjects PRIMARY KEY CLUSTERED (S_ID)
);

IF OBJECT_ID('dbo.Images', 'U') IS NOT NULL
    DROP TABLE dbo.Images;
CREATE TABLE dbo.Images(
    I_ID INT IDENTITY(1,1) NOT NULL,
    IImage VARBINARY(MAX) NOT NULL,
    CONSTRAINT PK_Images PRIMARY KEY CLUSTERED (I_ID)
);

IF OBJECT_ID('dbo.Answers', 'U') IS NOT NULL
    DROP TABLE dbo.Answers;
CREATE TABLE dbo.Answers(
    A_ID INT IDENTITY(1,1) NOT NULL,
    Answer VARCHAR(255) NOT NULL,
    CONSTRAINT PK_Answers PRIMARY KEY CLUSTERED (A_ID)
);

IF OBJECT_ID('dbo.Questions', 'U') IS NOT NULL
    DROP TABLE dbo.Questions;
CREATE TABLE dbo.Questions(
    Q_ID INT IDENTITY(1,1) NOT NULL,
    Question VARCHAR(255) NOT NULL,
    QuestionMark INT NOT NULL,
    S_ID INT NOT NULL,
    CONSTRAINT PK_Questions PRIMARY KEY CLUSTERED (Q_ID),
    CONSTRAINT FK_Questions_Subjects FOREIGN KEY (S_ID) REFERENCES dbo.Subjects(S_ID)
);

IF OBJECT_ID('dbo.QuestionImage', 'U') IS NOT NULL
    DROP TABLE dbo.QuestionImage;
CREATE TABLE dbo.QuestionImage(
    QI_ID INT IDENTITY(1,1) NOT NULL,
    Q_ID INT NOT NULL,
    I_ID INT NOT NULL,
    CONSTRAINT PK_QuestionImage PRIMARY KEY CLUSTERED (QI_ID),
    CONSTRAINT FK_QuestionImage_Questions FOREIGN KEY (Q_ID) REFERENCES dbo.Questions(Q_ID),
    CONSTRAINT FK_QuestionImage_Images FOREIGN KEY (I_ID) REFERENCES dbo.Images(I_ID)
);

IF OBJECT_ID('dbo.QuestionAnswer', 'U') IS NOT NULL
    DROP TABLE dbo.QuestionAnswer;
CREATE TABLE dbo.QuestionAnswer(
    QA_ID INT IDENTITY(1,1) NOT NULL,
    Q_ID INT NOT NULL,
    A_ID INT NOT NULL,
    CONSTRAINT PK_QuestionAnswer PRIMARY KEY CLUSTERED (QA_ID),
    CONSTRAINT FK_QuestionAnswer_Questions FOREIGN KEY (Q_ID) REFERENCES dbo.Questions(Q_ID),
    CONSTRAINT FK_QuestionAnswer_Answers FOREIGN KEY (A_ID) REFERENCES dbo.Answers(A_ID)
);

IF OBJECT_ID('dbo.History', 'U') IS NOT NULL
    DROP TABLE dbo.History;
CREATE TABLE dbo.History(
    H_ID INT IDENTITY(1,1) NOT NULL,
    GeneratedTime DATETIME NOT NULL,
    U_ID INT NOT NULL,
    CONSTRAINT PK_History PRIMARY KEY CLUSTERED (H_ID),
    CONSTRAINT FK_History_Users FOREIGN KEY (U_ID) REFERENCES dbo.Users(U_ID)
);

IF OBJECT_ID('HistoryQuestion', 'U') IS NOT NULL
    DROP TABLE HistoryQuestion;

CREATE TABLE HistoryQuestion (
    HQ_ID INT NOT NULL IDENTITY(1,1),
    H_ID INT NOT NULL,
    Q_ID INT NOT NULL,
    PRIMARY KEY (HQ_ID),
    FOREIGN KEY (H_ID) REFERENCES History(H_ID),
    FOREIGN KEY (Q_ID) REFERENCES Questions(Q_ID)
);

```

Example codes to insert dummy users (**RUN ONLY ONCE AFTER THE DATABASE IS CREATED TO AVOID ERRORS**):

```MSSQL
USE WebTechAssignmentDB;
GO

INSERT INTO Users (UserName, UserPassword, UserEmail)
VALUES 
    ('JohnDoe', 'pass123', 'johndoe@example.com'),
    ('JaneDoe', 'pass456', 'janedoe@example.com'),
    ('BobSmith', 'pass789', 'bobsmith@example.com');

```

If want to use local database, need to replace the `connectionString` in the [**pages that use database**](#Pages-use-database).

Can get `connectionString` from SSMS using these: [^1]

```MSSQL
select
    'data source=' + @@servername +
    ';initial catalog=' + db_name() +
    case type_desc
        when 'WINDOWS_LOGIN' 
            then ';trusted_connection=true'
        else
            ';user id=' + suser_name() + ';password=<<YourPassword>>'
    end
    as ConnectionString
from sys.server_principals
where name = suser_name()
```

## **Pages use database**

* [Index.cshtml.cs](https://github.com/BCS21090011/WebTechAssignment2023/blob/master/Web%20tech/Pages/Index.cshtml.cs)
> To login.
* [Index1.cshtml.cs](https://github.com/BCS21090011/WebTechAssignment2023/blob/master/Web%20tech/Pages/Index1.cshtml.cs)
> To register.
* [CreateQuestionPaper.cshtml.cs (will be)](https://github.com/BCS21090011/WebTechAssignment2023/blob/master/Web%20tech/Pages/CreateQuestionPaper.cshtml.cs)
> To get the questions and the answers.

---

## **References**

* [ChatGPT](https://chat.openai.com/chat)
* https://blog.csdn.net/weixin_43074474/article/details/105106894
* https://stackoverflow.com/questions/10479763/how-to-get-the-connection-string-from-a-database [^1]
* https://www.codinglabweb.com/2022/12/free-sidebar-menu-templates.html

---

[^1]: https://stackoverflow.com/questions/10479763/how-to-get-the-connection-string-from-a-database
