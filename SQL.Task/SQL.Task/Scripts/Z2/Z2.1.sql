SELECT SUM(o.Quantity*(o.UnitPrice-o.UnitPrice*o.Discount)) as Totals
FROM Northwind.Northwind.[Order Details] as o 

SELECT COUNT(CASE WHEN o.ShippedDate IS NULL THEN o.OrderId END) as [Total of not Shipped orders]
FROM Northwind.Northwind.Orders as o 

SELECT DISTINCT o.CustomerID, COUNT (*) OVER (PARTITION BY o.CustomerID)  as [Total customers]
FROM Northwind.Northwind.Orders as o 

--For test
--SELECT COUNT(o.CustomerID) as [Total customers]
--FROM Northwind.Northwind.Orders as o 
--GROUP BY o.CustomerID  
--ORDER BY o.CustomerID  