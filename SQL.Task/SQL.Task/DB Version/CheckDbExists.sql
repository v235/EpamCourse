:setvar pathToInit "C:\Users\Vitali_Pimenau\Documents\Visual Studio 2017\Projects\Epam.Courses\SQL.Task\SQL.Task\DB Version\Version 1.0"

USE master
GO
if NOT EXISTS (select * from sysdatabases where name='Northwind')
:r $(pathToInit)\Z3.DB.V1.0.sql
:r $(pathToInit)\CreateDBVersionTable.sql