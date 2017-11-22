SELECT DISTINCT p.ProductID, p.ProductName as [Product Name] 
FROM Northwind.Northwind.Products as p 
WHERE p.ProductName LIKE '%cho_olade%'