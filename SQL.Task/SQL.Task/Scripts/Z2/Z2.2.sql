SELECT YEAR (o.OrderDate) as [Year], COUNT (o.CustomerID) as Total
FROM Orders as o 
GROUP BY YEAR (o.OrderDate) 

SELECT COUNT (o.CustomerID) as Total
FROM Orders as o 

SELECT Seller=(SELECT (e.LastName +' '+ e.FirstName) FROM Employees as e 
WHERE e.EmployeeID=o.EmployeeID), COUNT(o.OrderId) as Amount
FROM Orders as o
GROUP BY o.EmployeeID
HAVING o.EmployeeID IS NOT NULL
ORDER BY COUNT(o.OrderId) DESC

SELECT Seller=(SELECT (e.LastName +' '+ e.FirstName) FROM Employees as e 
WHERE e.EmployeeID=o.EmployeeID), CustomerID, COUNT (o.OrderId) as [Orders Count]
FROM Orders as o 
WHERE YEAR (o.OrderDate)=1998
GROUP BY o.EmployeeID, o.CustomerID 

SELECT DISTINCT* 
FROM
(SELECT 'Employee' as Type, (e.LastName +' '+ e.FirstName) as [Name], e.City as City FROM Employees as e 
WHERE e.City IN (SELECT c.City FROM Customers as c)
UNION
SELECT 'Customer' as Type, c.CustomerID as [Name], c.City as City FROM Customers as c
WHERE c.City IN (SELECT e.City FROM Employees as e)
) as result

SELECT c.CustomerID as [Customers Name], c.City FROM Customers as c
GROUP BY c.City, c.CustomerID

SELECT (e.LastName +' '+ e.FirstName) as [Name], BossTable.Title as [Reports to] 
FROM Employees as e
INNER JOIN
(SELECT EmployeeID, Title FROM Employees
WHERE EmployeeID IN (SELECT DISTINCT ReportsTo
FROM Employees)) as BossTable ON e.ReportsTo=BossTable.EmployeeID

