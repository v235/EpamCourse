﻿using System;
using System.Collections.Generic;
using NorthwindLibrary;

namespace CustomSerializationLib.SerializationHelpers
{
    internal class OrderSurrogate
    {
        public int OrderID { get; set; }

        public string CustomerID { get; set; }

        public int? EmployeeID { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public int? ShipVia { get; set; }

        public decimal? Freight { get; set; }

        public string ShipName { get; set; }

        public string ShipAddress { get; set; }

        public string ShipCity { get; set; }

        public string ShipRegion { get; set; }

        public string ShipPostalCode { get; set; }

        public string ShipCountry { get; set; }

        //public virtual Customer Customer { get; set; }

        //public virtual Employee Employee { get; set; }

       // public virtual ICollection<Order_Detail> Order_Details { get; set; }

        //public virtual Shipper Shipper { get; set; }
    }
}
