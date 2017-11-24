--if Error use Region instead  of Regions 
SELECT DISTINCT Seller=(SELECT e.LastName +' '+ e.FirstName 
FROM Employees as e
WHERE e.EmployeeID=t.EmployeeID)
FROM EmployeeTerritories as t
INNER JOIN
Territories as r ON t.TerritoryID=r.TerritoryID AND r.RegionID=(SELECT RegionID 
FROM Regions WHERE RegionDescription='Western')


--OR
--if Error use Region instead Regions 
SELECT DISTINCT(LastName +' '+ FirstName) as [Name], r.RegionDescription as Region
FROM ((Employees as e
INNER JOIN
EmployeeTerritories as t ON e.EmployeeID = t.EmployeeID)
INNER JOIN
Territories as et ON t.TerritoryID=et.TerritoryID 
INNER JOIN 
Regions as r ON et.RegionID=r.RegionID AND r.RegionDescription='Western')

 SELECT c.ContactName as [Name], COUNT(o.OrderId) 
 FROM (Customers as c
 LEFT JOIN 
 Orders as o
 ON c.CustomerID=o.CustomerID)
 GROUP BY c.ContactName 
 ORDER BY COUNT(o.OrderId)
