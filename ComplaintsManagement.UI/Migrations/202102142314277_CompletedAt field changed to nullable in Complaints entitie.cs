namespace ComplaintsManagement.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompletedAtfieldchangedtonullableinComplaintsentitie : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Complaints", "CompletedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Complaints", "CompletedAt", c => c.DateTime(nullable: false));
        }
    }
}
