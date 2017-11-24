USE Northwind

GO
EXEC SP_RENAME 'Region', 'Regions'
ALTER TABLE Customers	
ADD CreatedDate "datetime" NULL 
GO