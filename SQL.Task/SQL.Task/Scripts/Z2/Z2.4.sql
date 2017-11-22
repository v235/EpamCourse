SELECT CompanyName as [Company Name] 
FROM Northwind.Northwind.Suppliers as s 
WHERE s.SupplierID IN (SELECT p.SupplierID FROM Northwind.Northwind.Products as p WHERE UnitsINStock=0)

SELECT (e.LastName +' '+ e.FirstName) as [Name], subTable.[Orders Quantity] as [Orders Quantity]
FROM Northwind.Northwind.Employees as e 
INNER JOIN
(SELECT DISTINCT o.EmployeeID, [QuantityTable].[Orders Quantity] FROM Northwind.Northwind.Orders as o 
INNER JOIN
(SELECT od.OrderId, SUM(od.Quantity) as [Orders Quantity] 
FROM Northwind.Northwind.[Order Details] as od 
GROUP BY od.OrderId
HAVING SUM(od.Quantity)>150)
as [QuantityTable] ON o.OrderId=[QuantityTable].OrderId) as subTable ON e.EmployeeID=subTable.EmployeeID
ORDER BY subTable.[Orders Quantity]

SELECT Seller=(SELECT (e.LastName +' '+ e.FirstName) FROM Northwind.Northwind.Employees as e 
WHERE e.EmployeeID=o.EmployeeID)
FROM Northwind.Northwind.Orders as o
WHERE o.OrderId IN (SELECT od.OrderId 
FROM Northwind.Northwind.[Order Details] as od 
GROUP BY od.OrderId
HAVING SUM(od.Quantity)>150)

SELECT DISTINCT [Name]=(SELECT c.ContactName FROM Northwind.Northwind.Customers as c 
WHERE c.CustomerID=o.CustomerID)
FROM Northwind.Northwind.Orders as o
WHERE EXISTS(SELECT od.OrderId 
FROM Northwind.Northwind.[Order Details] as od 
GROUP BY od.OrderId
HAVING SUM(od.Quantity)=0)
