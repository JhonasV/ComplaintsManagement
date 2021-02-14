namespace ComplaintsManagement.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgentIdremovedfromComplaintsentitie : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Complaints", "AgentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Complaints", "AgentId", c => c.Int(nullable: false));
        }
    }
}
