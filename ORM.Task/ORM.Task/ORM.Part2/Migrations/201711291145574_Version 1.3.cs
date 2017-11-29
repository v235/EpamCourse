namespace ORM.Part2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version13 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Region", newName: "Regions");
            AddColumn("dbo.Customers", "CreatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
        }
    }
}
