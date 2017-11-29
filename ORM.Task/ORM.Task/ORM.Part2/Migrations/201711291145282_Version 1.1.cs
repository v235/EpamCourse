namespace ORM.Part2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.EmployeeCreditCard",
                    c => new
                    {
                        CardID = c.Int(nullable: false, identity: true),
                        CardNumber = c.String(nullable: false, maxLength: 25),
                        EndDate = c.DateTime(storeType: "datetime"),
                        CardHolder = c.String(nullable: false, maxLength: 25),
                        EmployeeID = c.Int(nullable: false)
                    })
                .PrimaryKey(t => t.CardID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID);
        }

        public override void Down()
        {
        }
    }
}
