SELECT o.OrderId, o.ShippedDate, o.ShipVia FROM Northwind.Northwind.Orders as o 
WHERE CAST(o.ShippedDate as DATE)>='1998-05-06' AND o.ShipVia>=2

SELECT o.OrderId, 
CASE WHEN o.ShippedDate IS NULL THEN 'Not Shipped' END as ShippedDate
FROM Northwind.Northwind.Orders as o 
WHERE o.ShippedDate IS NULL

SELECT o.OrderId as [Order Number], 
CASE WHEN o.ShippedDate IS NULL THEN ('Not Shipped') ELSE CONVERT(NVARCHAR, o.ShippedDate, 101)+N' '+ CONVERT(NVARCHAR, CAST(o.ShippedDate AS time(0)),109) END as [Shipped Date]
FROM Northwind.Northwind.Orders as o 
WHERE CAST(o.ShippedDate as DATE)>'1998-05-06'OR o.ShippedDate IS NULL