// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{
	    private DataSource dataSource = new DataSource();

	    [Category("Restriction Operators")]
		[Title("Where - Task 1")]
		[Description("This sample uses the where clause to find all elements of an array with a value less than 5.")]
		public void Linq1()
		{
			int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

			var lowNums =
				from num in numbers
				where num < 5
				select num;

			Console.WriteLine("Numbers < 5:");
		    foreach (var x in lowNums)
		    {
		        Console.WriteLine(x);
		    }
		}

	    [Category("Restriction Operators")]
		[Title("Where - Task 2")]
		[Description("This sample return return all presented in market products")]
		public void Linq2()
		{
			var products =
				from p in dataSource.Products
				where p.UnitsInStock > 0
				select p;

			foreach (var p in products)
			{
				ObjectDumper.Write(p);
			}
		}

	    [Category("LINQ modules")]
	    [Title("Task 1")]
	    [Description("This methode return list of customers where amount of all orders > X")]

	    public void Linq001()
	    {
	        int[] param = {5000, -2, 100, 10000};
	        for (int i = 0; i < param.Length; i++)
	        {
	            ObjectDumper.Write("Filter=");
	            ObjectDumper.Write(param[i]);
	            var customersList = dataSource
	                .Customers
	                .Where(c => c
	                            .Orders.Sum(o => o
	                                .Total) > param[i]);
	            foreach (var c in customersList)
	            {
	                ObjectDumper.Write(c);
	            }
	        }
	    }

	    [Category("LINQ modules")]
	    [Title("Task 2")]
	    [Description("This methode return list of suppliers located in the same country and the same city")]
	    public void Linq002()
	    {
	        ObjectDumper.Write("WithGroup");
	        var customersListWithGroup = dataSource.Customers.GroupJoin(dataSource
	            .Suppliers, c => c
	            .Country, s => s
	            .Country, (cus, sup) => new
	        {
	            Customers = cus,
	            Suppliers = sup
	                .Where(s => s.City == cus.City)
	        });
	        foreach (var c in customersListWithGroup)
	        {
	            ObjectDumper.Write(c.Customers);
	            ObjectDumper.Write(c.Suppliers);
	        }
	        ObjectDumper.Write("WithoutGroup");
	        var customersListWithoutGroup = dataSource.Customers.Join(dataSource
	            .Suppliers, c => c
	            .Country, s => s
	            .Country, (cus, sup) => new
	        {
	            Customers = cus,
	            Suppliers = sup
	        }).Where(c => c.Customers.City == c.Suppliers.City);

	        foreach (var c in customersListWithoutGroup)
	        {
	            ObjectDumper.Write(c.Customers);
	            ObjectDumper.Write(c.Suppliers);
	        }
	    }

	    [Category("LINQ modules")]
	    [Title("Task 3")]
	    [Description("This methode return list of customers where sum of orders > X")]
	    public void Linq003()
	    {
	        var customersList = dataSource
	            .Customers
	            .Where(c => c.Orders
	                .Any(o => o.Total > 1000));
	        foreach (var c in customersList)
	        {
	            ObjectDumper.Write(c);
	        }
	    }

	    [Category("LINQ modules")]
	    [Title("Task 4")]
	    [Description("This methode return list of customers which show when they became a customers")]
	    public void Linq004()
	    {
	        var customersList = dataSource
	            .Customers
	            .SelectMany(c => c
	                .Orders.OrderBy(o => o
	                    .OrderDate)
	                .Take(1), (c, d) => new {Customers = c, Year = d.OrderDate.Year, Month = d.OrderDate.Month});
	        foreach (var c in customersList)
	        {
	            ObjectDumper.Write(c.Customers);
	            ObjectDumper.Write(c.Year);
	            ObjectDumper.Write(c.Month);
	        }
	    }

	    [Category("LINQ modules")]
	    [Title("Task 5")]
	    [Description("This method returns a list of clients that show when they become clients from which year, and then from which month, and then to the maximum value, and then by name")]
	    public void Linq005()
	    {
	        var customersList = dataSource
	            .Customers
	            .SelectMany(c => c.Orders
	                    .OrderBy(o => o.OrderDate)
	                    .Take(1),
	                (c, d) => new
	                {
	                    Customers = c,
	                    d.OrderDate.Year,
	                    d.OrderDate.Month,
	                    Max = c.Orders.Max(m => m.Total)
	                })
	            .OrderBy(y => y.Year).ThenBy(m => m.Month)
	            .ThenByDescending(c => c.Customers.Orders.Max(o => o.Total))
	            .ThenBy(c => c.Customers.CustomerID);
	        foreach (var c in customersList)
	        {
	            ObjectDumper.Write(c.Customers);
	            ObjectDumper.Write(c.Year);
	            ObjectDumper.Write(c.Month);
	        }
	    }

	    [Category("LINQ modules")]
	    [Title("Task 6")]
	    [Description("This method returns a list of clients with a non - digital zip code or a non - filled region or an unnamed operator code in the phone")]
	    public void Linq006()
	    {
	        var customersList = dataSource.Customers.Where(c =>
	            !string.IsNullOrEmpty(c.PostalCode) && c.PostalCode.StartsWith("(")
                || !string.IsNullOrEmpty(c.Region) && c.Region.StartsWith("(")
                ||!string.IsNullOrEmpty(c.Phone) && c.Phone.StartsWith("("));
	        foreach (var c in customersList)
	        {
	            ObjectDumper.Write(c);
	        }
	    }

	    [Category("LINQ modules")]
	    [Title("Task 7")]
	    [Description("This method returns a list of products grouped by category, and then by quantity and cost")]
	    public void Linq007()
	    {
	        var productsList = dataSource.Products.GroupBy(p => p.Category)
	            .Select(c => new
	                {
	                    Filter = "ByCategory: " + c.Key,
	                    Items = c
	                        .GroupBy(p => p.UnitsInStock)
	                        .Select(s => new
	                        {
	                            Filter = "ByUnitsInStock: " + s.Key,
	                            Items = s
	                                .GroupBy(p => new
	                                {
	                                    Filter = "ByUnitPrice: " + p.UnitPrice,
	                                    Items = p
	                                        .UnitPrice
	                                })
	                        })
	                }
	            );
	        foreach (var p in productsList)
	        {
	            ObjectDumper.Write(p.Filter);
	            ObjectDumper.Write(p.Items);
	            foreach (var sp in p.Items)
	            {
	                ObjectDumper.Write(sp.Filter);
	                ObjectDumper.Write(sp.Items);
	                foreach (var ssp in sp.Items)
	                {
	                    ObjectDumper.Write(ssp.Key.Filter);
	                    ObjectDumper.Write(ssp.Key.Items);
	                }
	            }
	        }
	    }

	    private string GetProductGroup(decimal unitPrice)
        {
            if (unitPrice < 20)
                return "chip";
            if (unitPrice > 50)
                return "expencive";
            return "avrege";

        }

	    [Category("LINQ modules")]
	    [Title("Task 8")]
	    [Description("This method returns a list of products grouped by price")]
	    public void Linq008()
	    {
	        var productsList = dataSource.Products
	            .GroupBy(p => GetProductGroup(p.UnitPrice));
	        foreach (var p in productsList)
	        {
	            ObjectDumper.Write(p.Key);
	            foreach (var item in p)
	            {
	                ObjectDumper.Write(item);
	            }
	        }
	    }

	    [Category("LINQ modules")]
	    [Title("Task 9")]
	    [Description("This method returns with the average profit and intensity of each city in which customers live")]
	    public void Linq009()
	    {
	        var averegeList = dataSource.Customers.GroupBy(c => c.City).Select(c => new
	            {
	                city = c.Key,
	                averageProfit = c
	                                    .Sum(a => a.Orders.Length > 0
	                                        ? a.Orders.Average(o => o.Total)
	                                        : 0) / c.Count(),
	                averageIntensity = c.Average(o => o.Orders.Length)
	            }
	        );

	        foreach (var a in averegeList)
	        {
	            ObjectDumper.Write(a);
	        }
	    }

	    [Category("LINQ modules")]
	    [Title("Task 10")]
	    [Description(
	        "This method returns a list with an average annual activity statistics of clients by months and years")]
	    public void Linq010()
	    {
	        var statisticList = dataSource.Customers.Select(c => new
	            {
	                customer = c.CustomerID,
	                groupByMonth = c.Orders.GroupBy(o => o.OrderDate.Month).OrderBy(o => o.Key),
	                groupByYear = c.Orders.GroupBy(o => o.OrderDate.Year)
	            })
	            .Select(c => new
	            {
	                statisticByMonthsList = c
	                    .groupByMonth
	                    .Select(o => new
	                        {
	                            customer = c.customer,
	                            month = o.Key,
	                            statisticActivityByMonth = o.Count()
	                        }
	                    ),
	                statisticByYearsList = c
	                    .groupByYear
	                    .Select(o => new
	                        {
	                            customer = c.customer,
	                            year = o.Key,
	                            statisticActivityByYear = o.Count()
	                        }
	                    ),
	                statisticByYearsAndMonthsList = c
	                    .groupByYear
	                    .Select(y => new
	                    {
	                        year = y.Key,
	                        monthgroup = y.Select(m => m.OrderDate)
	                            .GroupBy(d => d.Month)
	                            .Select(d => new
	                            {
	                                customer = c.customer,
	                                month = d.Key,
	                                statisticActivityByYearAndMonth = d.Count()
	                            })
	                    })

	            });
	        foreach (var s in statisticList)
	        {
	            ObjectDumper.Write(s.statisticByMonthsList);
	            ObjectDumper.Write(s.statisticByYearsList);
                ObjectDumper.Write(s.statisticByYearsAndMonthsList);
            }
	    }
	}
}
