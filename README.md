# **WebTechAssignment2023**

--- 

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
> Currently link offline, need to create database locally. More information at [**Local database**](#Local-database) section.
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

* Index.cshtml.cs
> To login.
* Index1.cshtml.cs
> To register.
* CreateQuestionPaper.cshtml.cs (will be)
> To get the questions.

## **References**

* [ChatGPT](https://chat.openai.com/chat)
* https://blog.csdn.net/weixin_43074474/article/details/105106894
* https://stackoverflow.com/questions/10479763/how-to-get-the-connection-string-from-a-database [^1]

[^1]: https://stackoverflow.com/questions/10479763/how-to-get-the-connection-string-from-a-database
