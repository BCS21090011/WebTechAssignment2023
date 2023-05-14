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
* [X] Add the function to generate the pdf files (both question and answer).
* [ ] Link to the database (online).

> Currently link offline, need to create database locally. More information in [**Local database**](#Local-database) section.

* [ ] Push the project online.
* [ ] Submit the project.

## **Pages needed**

* [X] [Login page](https://github.com/BCS21090011/WebTechAssignment2023/blob/master/Web%20tech/Pages/Index.cshtml)
* [X] [Register page](https://github.com/BCS21090011/WebTechAssignment2023/blob/master/Web%20tech/Pages/Index1.cshtml)
* [ ] Dashboard page
* [X] [Home page](https://github.com/BCS21090011/WebTechAssignment2023/blob/master/Web%20tech/Pages/Index4.cshtml)
* [ ] [User profile page](https://github.com/BCS21090011/WebTechAssignment2023/blob/master/Web%20tech/Pages/ProfilePage.cshtml)

> * Need to connect to database.

* [ ] [Insert question page](https://github.com/BCS21090011/WebTechAssignment2023/blob/master/Web%20tech/Pages/KeyInPage.cshtml)

> * Connect to database.

* [ ] [Question bank page](https://github.com/BCS21090011/WebTechAssignment2023/blob/master/Web%20tech/Pages/QuestionBank.cshtml)
* [ ] [Create question paper page](https://github.com/BCS21090011/WebTechAssignment2023/blob/master/Web%20tech/Pages/CreateQuestionPaper.cshtml)

> * Need to connect to database (maybe using ajax).

* [ ] History page
* [X] [Contact us](https://github.com/BCS21090011/WebTechAssignment2023/blob/master/Web%20tech/Pages/ContactUS.cshtml)

## **Local database**

Need to install SSMS first, [guide for installation](https://blog.csdn.net/weixin_43074474/article/details/105106894).

Codes to create database:

```MSSQL
IF EXISTS(SELECT * FROM sys.databases WHERE name = 'WebTechAssignmentDB')
BEGIN
    DROP DATABASE WebTechAssignmentDB;
END

CREATE DATABASE WebTechAssignmentDB;
Go
USE WebTechAssignmentDB;
GO

CREATE TABLE dbo.Users(
    Users_ID INT IDENTITY(1,1) NOT NULL,
    UserName VARCHAR(255) NOT NULL,
    UserPassword VARCHAR(255) NOT NULL,
    UserGender VARCHAR(255) NOT NULL,
    UserCountry VARCHAR(255) NOT NULL,
    UserEmail VARCHAR(255) NOT NULL,
    UserPhoneNumber INT NOT NULL,
    UserSchoolName VARCHAR(255) NOT NULL,
    CONSTRAINT PK_Users PRIMARY KEY CLUSTERED (Users_ID)
);

CREATE TABLE dbo.Subjects(
    Subjects_ID INT IDENTITY(1,1) NOT NULL,
    SubjectName VARCHAR(255) NOT NULL,
    CONSTRAINT PK_Subjects PRIMARY KEY CLUSTERED (Subjects_ID)
);

CREATE TABLE dbo.Questions(
    Questions_ID INT IDENTITY(1,1) NOT NULL,
    Question VARCHAR(255) NOT NULL,
    QuestionMark INT NOT NULL,
    QuestionDifficulty INT NOT NULL,
    Subjects_ID INT NOT NULL,
    QuestionImageFileName VARCHAR(255) DEFAULT NULL,
    Answer VARCHAR(255) NOT NULL,
    AnswerImageFileName VARCHAR(255) DEFAULT NULL,
    CONSTRAINT PK_Questions PRIMARY KEY CLUSTERED (Questions_ID),
    CONSTRAINT FK_Questions_Subjects FOREIGN KEY (Subjects_ID) REFERENCES dbo.Subjects(Subjects_ID)
);

CREATE TABLE dbo.History(
    History_ID INT IDENTITY(1,1) NOT NULL,
    GeneratedTime DATETIME NOT NULL,
    Users_ID INT NOT NULL,
    CONSTRAINT PK_History PRIMARY KEY CLUSTERED (History_ID),
    CONSTRAINT FK_History_Users FOREIGN KEY (Users_ID) REFERENCES dbo.Users(Users_ID)
);

CREATE TABLE HistoryQuestion (
    HistoryQuestion_ID INT NOT NULL IDENTITY(1,1),
    History_ID INT NOT NULL,
    Questions_ID INT NOT NULL,
    Question_Number INT NOT NULL,
    PRIMARY KEY (HistoryQuestion_ID),
    FOREIGN KEY (History_ID) REFERENCES History(History_ID),
    FOREIGN KEY (Questions_ID) REFERENCES Questions(Questions_ID)
);

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

> * To get the questions and the answers.
> * Insert new history into database.

* [ProfilePage.cshtml.cs](https://github.com/BCS21090011/WebTechAssignment2023/blob/master/Web%20tech/Pages/ProfilePage.cshtml)

> Get the user profile.

* [KeyInPage.cshtml.cs](https://github.com/BCS21090011/WebTechAssignment2023/blob/master/Web%20tech/Pages/KeyInPage.cshtml)

> Insert the question into the database.

* [QuestionBank.cshtml.cs](https://github.com/BCS21090011/WebTechAssignment2023/blob/master/Web%20tech/Pages/QuestionBank.cshtml)

> Get the questions from database.

---

## **References**

* [ChatGPT](https://chat.openai.com/chat)
* Bing
* [Monica](https://chrome.google.com/webstore/detail/monica-%E2%80%94-your-chatgpt-cop/ofpnmcalabcbjgholdjcjblkibolbppb)
* [Bard](https://bard.google.com/)
* https://blog.csdn.net/weixin_43074474/article/details/105106894
* https://stackoverflow.com/questions/10479763/how-to-get-the-connection-string-from-a-database [^1]
* https://www.codinglabweb.com/2022/12/free-sidebar-menu-templates.html
* http://jotform.com/login/?rp=https://www.jotform.com/build/231019333502039
* https://www.w3schools.com/xml/ajax_database.asp
* https://bobbyhadz.com/blog/javascript-split-string-by-newline
* https://teleporthq.io/professional-website-builder
* https://www.codingnepalweb.com/drag-and-drop-sortable-list-html-javascript/
* https://codingartistweb.com/2023/02/drag-and-drop-sortable-list-javascript/
* https://stackoverflow.com/questions/63654654/drag-and-drop-between-two-lists-list-2-only-has-sort
* https://www.geeksforgeeks.org/jquery-ui-draggable-connecttosortable-option/
* https://youtu.be/9HUlUnM3UG8
* https://developer.mozilla.org/en-US/docs/Web/API/DataTransfer/setData
* https://www.freecodecamp.org/news/how-to-insert-an-element-into-an-array-in-javascript/amp/
* https://stackoverflow.com/questions/21064101/understanding-offsetwidth-clientwidth-scrollwidth-and-height-respectively/21064102#21064102
* https://stackoverflow.com/questions/67960714/how-to-get-the-scroll-amount-if-a-div-has-the-scroll
* https://www.tutorialspoint.com/how-to-generate-a-pdf-from-an-html-webpage
* https://www.w3schools.com/cssref/pr_text_white-space.php
* https://stackoverflow.com/questions/15131072/check-whether-string-contains-a-line-break
* https://stackoverflow.com/questions/1039827/when-to-use-ul-or-ol-in-html/1039834#1039834
* https://discuss.codecademy.com/t/what-happens-if-i-directly-add-the-li-without-adding-the-ul/380816/7
* https://stackoverflow.com/questions/24768630/is-there-a-way-to-show-a-progressbar-on-github-wiki

---

[^1]: https://stackoverflow.com/questions/10479763/how-to-get-the-connection-string-from-a-database
