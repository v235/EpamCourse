﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace ORM.Part1.Entity
{
    [Table (Name="Shippers")]
    public class Shippers
    {
        [PrimaryKey, Identity]
        public int ShipperID { get; set; }

        [Column]
        public string CompanyName { get; set; }

        [Column]
        public string Phone { get; set; }

        [Association(ThisKey = "OrderID", OtherKey = "OrderID")]
        public ICollection<Orders> Orders { get; set; }

    }
}
