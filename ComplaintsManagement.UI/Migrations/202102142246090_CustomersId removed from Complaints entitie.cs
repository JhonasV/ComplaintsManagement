namespace ComplaintsManagement.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomersIdremovedfromComplaintsentitie : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Complaints", "CustomersId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Complaints", "CustomersId", c => c.Int(nullable: false));
        }
    }
}
