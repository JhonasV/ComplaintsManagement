namespace ComplaintsManagement.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class servicerateentityadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceRates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComplaintsId = c.Int(nullable: false),
                        Rate = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Complaints", t => t.ComplaintsId, cascadeDelete: true)
                .Index(t => t.ComplaintsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceRates", "ComplaintsId", "dbo.Complaints");
            DropIndex("dbo.ServiceRates", new[] { "ComplaintsId" });
            DropTable("dbo.ServiceRates");
        }
    }
}
