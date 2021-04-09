namespace ComplaintsManagement.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketTypesandBinnacleentitiesadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Binnacles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(),
                        StatusId = c.Int(nullable: false),
                        Comment = c.String(maxLength: 200),
                        ComplaintsId = c.Int(),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Complaints", t => t.ComplaintsId)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.StatusId)
                .Index(t => t.ComplaintsId);
            
            CreateTable(
                "dbo.TicketTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Complaints", "TicketTypesId", c => c.Int(nullable: false));
            CreateIndex("dbo.Complaints", "TicketTypesId");
            AddForeignKey("dbo.Complaints", "TicketTypesId", "dbo.TicketTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Binnacles", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Binnacles", "ComplaintsId", "dbo.Complaints");
            DropForeignKey("dbo.Complaints", "TicketTypesId", "dbo.TicketTypes");
            DropIndex("dbo.Complaints", new[] { "TicketTypesId" });
            DropIndex("dbo.Binnacles", new[] { "ComplaintsId" });
            DropIndex("dbo.Binnacles", new[] { "StatusId" });
            DropColumn("dbo.Complaints", "TicketTypesId");
            DropTable("dbo.TicketTypes");
            DropTable("dbo.Binnacles");
        }
    }
}
