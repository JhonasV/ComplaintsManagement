namespace ComplaintsManagement.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsersIdaddetocomplaintsentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Complaints", "UsersId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Complaints", "UsersId");
        }
    }
}
