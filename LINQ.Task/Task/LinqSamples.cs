// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
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
	        int[] param = { 5000, -2, 100, 10000 };
	        for (int i = 0; i < param.Length; i++)
	        {
	            ObjectDumper.Write("Filter=");
	            ObjectDumper.Write(param[i]);
	            var customersList = dataSource.Customers.Where(c => c.Orders.Sum(o => o.Total) > param[i]);
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
	        var customersList = dataSource.Customers.Where(c => c.Orders.Any(o => o.Total >1000));
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
	        var customersList = dataSource.Customers.SelectMany(c=> c.Orders.OrderBy(o=>o.OrderDate).Take(1), (c, d)=>new { Customers= c, Year = d.OrderDate.Year, Month = d.OrderDate.Month});
            foreach (var c in customersList)
            {
                ObjectDumper.Write(c.Customers);
                ObjectDumper.Write(c.Year);
                ObjectDumper.Write(c.Month);
            }
        }
	    [Category("LINQ modules")]
        [Title("Task 5")]
	    [Description("This methode return list of customers which show when they became a customers with ordering by year then by month then by max value then by Name ")]
	    public void Linq005()
	    {
	        var customersList = dataSource.Customers.SelectMany(c => c.Orders.OrderBy(o => o.OrderDate).Take(1), (c, d) => new { Customers = c, Year = d.OrderDate.Year, Month = d.OrderDate.Month, Max=c.Orders.Max(m=>m.Total) }).OrderBy(y=>y.Year).ThenBy(m=>m.Month).ThenByDescending(c=>c.Customers.Orders.Max(o=>o.Total)).ThenBy(c=>c.Customers.CustomerID);
            foreach (var c in customersList)
            {
                ObjectDumper.Write(c.Customers);
                ObjectDumper.Write(c.Year);
                ObjectDumper.Write(c.Month);
            }
        }
	    [Category("LINQ modules")]
	    [Title("Task 6")]
	    [Description("This methode return list of customers with non-digital postal code is indicated or the region is not filled or the operator code is not specified in the phone")]
	    public void Linq006()
	    {
	        var customersList = dataSource.Customers.Where(c =>
	            !c.PostalCode.Contains("("));
	        //|| !c.Region.Contains("(") 
	        //|| !c.Phone.Contains("("));
	        //foreach (var c in customersList)
	        //{
	        //    ObjectDumper.Write(c.Customers);
	        //    ObjectDumper.Write(c.Year);
	        //    ObjectDumper.Write(c.Month);
	        //}
	    }

    }
}
