SELECT DISTINCT Seller=(SELECT e.LastName +' '+ e.FirstName 
FROM Northwind.Northwind.Employees as e
WHERE e.EmployeeID=t.EmployeeID)
FROM Northwind.Northwind.EmployeeTerritories as t
INNER JOIN
Northwind.Northwind.Territories as r ON t.TerritoryID=r.TerritoryID AND r.RegionID=(SELECT RegionID 
FROM Northwind.Northwind.Region WHERE RegionDescription='Western')

--OR

SELECT DISTINCT(LastName +' '+ FirstName) as [Name], r.RegionDescription as Region
FROM ((Northwind.Northwind.Employees as e
INNER JOIN
Northwind.Northwind.EmployeeTerritories as t ON e.EmployeeID = t.EmployeeID)
INNER JOIN
Northwind.Northwind.Territories as et ON t.TerritoryID=et.TerritoryID 
INNER JOIN 
Northwind.Northwind.Region as r ON et.RegionID=r.RegionID AND r.RegionDescription='Western')

 SELECT c.ContactName as [Name], COUNT(o.OrderId) 
 FROM (Northwind.Northwind.Customers as c
 LEFT JOIN 
 Northwind.Northwind.Orders as o
 ON c.CustomerID=o.CustomerID)
 GROUP BY c.ContactName 
 ORDER BY COUNT(o.OrderId)
