SELECT c.CustomerID as [Customer Name], c.Country 
FROM Customers as c 
WHERE c.Country IN ('USA', 'Canada') ORDER BY c.CustomerID, c.Country

SELECT c.CustomerID as [Customer Name], c.Country 
FROM Customers as c 
WHERE c.Country NOT IN ('USA', 'Canada') ORDER BY c.CustomerID

SELECT DISTINCT c.Country 
FROM Customers as c 