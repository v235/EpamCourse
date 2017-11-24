:setvar PathToVer1_1 "C:\Users\Vitali_Pimenau\Documents\Visual Studio 2017\Projects\Epam.Courses\SQL.Task\SQL.Task\DB Version\Version 1.1"
:setvar PathToVer1_3 "C:\Users\Vitali_Pimenau\Documents\Visual Studio 2017\Projects\Epam.Courses\SQL.Task\SQL.Task\DB Version\Version 1.3"

USE Northwind
GO
IF EXISTS (SELECT s.DbVersion FROM Settings as s WHERE s.DbVersion=1.0)
:r $(PathToVer1_1)\Z3.DB.V1.1.sql
:r $(PathToVer1_1)\SetDBVersionTableV1.1.sql

IF EXISTS (SELECT s.DbVersion FROM Settings as s WHERE s.DbVersion=1.1)
:r $(PathToVer1_3)\Z3.DB.V1.3.sql
:r $(PathToVer1_3)\SetDBVersionTableV1.3.sql
GO