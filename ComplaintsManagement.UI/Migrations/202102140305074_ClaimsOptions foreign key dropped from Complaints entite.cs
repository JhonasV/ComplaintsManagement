namespace ComplaintsManagement.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClaimsOptionsforeignkeydroppedfromComplaintsentite : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Complaints", "ClaimsOptionsId", "dbo.ClaimsOptions");
            DropIndex("dbo.Complaints", new[] { "ClaimsOptionsId" });
            DropColumn("dbo.Complaints", "ClaimsOptionsId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Complaints", "ClaimsOptionsId", c => c.Int(nullable: false));
            CreateIndex("dbo.Complaints", "ClaimsOptionsId");
            AddForeignKey("dbo.Complaints", "ClaimsOptionsId", "dbo.ClaimsOptions", "Id", cascadeDelete: true);
        }
    }
}
