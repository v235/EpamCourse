SELECT DISTINCT o.OrderId 
FROM Northwind.Northwind.[Order Details] as o 
WHERE o.Quantity BETWEEN 3 AND 10 

SELECT c.CustomerID as [Customer Name], c.Country 
FROM Northwind.Northwind.Customers as c 
WHERE SUBSTRING(c.Country,1,1) BETWEEN 'b' AND 'g'  ORDER BY c.Country

SELECT c.CustomerID as [Customer Name], c.Country 
FROM Northwind.Northwind.Customers as c 
WHERE SUBSTRING(c.Country,1,1) >='b' AND SUBSTRING(c.Country,1,1)<='g' ORDER BY c.Country
