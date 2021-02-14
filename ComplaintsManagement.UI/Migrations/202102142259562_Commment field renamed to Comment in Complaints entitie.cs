namespace ComplaintsManagement.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommmentfieldrenamedtoCommentinComplaintsentitie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Complaints", "Comment", c => c.String(nullable: false));
            DropColumn("dbo.Complaints", "Commment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Complaints", "Commment", c => c.String(nullable: false));
            DropColumn("dbo.Complaints", "Comment");
        }
    }
}
