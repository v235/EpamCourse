using ORM.Part2.Entity;

namespace ORM.Part2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ORM.Part2.DBContext.NorthwindDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ORM.Part2.DBContext.NorthwindDB context)
        {
            context.Categories.AddOrUpdate(
                new Category() {CategoryID = 1, CategoryName = "testCat"},
                new Category() {CategoryID = 2, CategoryName = "testCat2"});

            context.Regions.AddOrUpdate(
                new Regions() {RegionID = 1, RegionDescription = "TestReg"},
                new Regions() {RegionID = 2, RegionDescription = "TestReg2"});

            context.Territories.AddOrUpdate(
                new Territory() {TerritoryID = "1", RegionID = 1, TerritoryDescription = "testTer"},
                new Territory() {TerritoryID = "2", RegionID = 1, TerritoryDescription = "testTer2"});
        }
    }
}
