namespace ComplaintsManagement.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Costumerentitieadded : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "Costumers");
            DropForeignKey("dbo.UsersProducts", "ProductsId", "dbo.Products");
            DropForeignKey("dbo.UsersProducts", "UsersId", "dbo.Users");
            DropIndex("dbo.UsersProducts", new[] { "UsersId" });
            DropIndex("dbo.UsersProducts", new[] { "ProductsId" });
            CreateTable(
                "dbo.CostumersProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CostumersId = c.Int(nullable: false),
                        ProductsId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Costumers", t => t.CostumersId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductsId, cascadeDelete: true)
                .Index(t => t.CostumersId)
                .Index(t => t.ProductsId);
            
            AlterColumn("dbo.Costumers", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Costumers", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Costumers", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Costumers", "DocumentNumber", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Costumers", "PhoneNumber", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Costumers", "Password");
            DropTable("dbo.UsersProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UsersProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UsersId = c.Int(nullable: false),
                        ProductsId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Costumers", "Password", c => c.String());
            DropForeignKey("dbo.CostumersProducts", "ProductsId", "dbo.Products");
            DropForeignKey("dbo.CostumersProducts", "CostumersId", "dbo.Costumers");
            DropIndex("dbo.CostumersProducts", new[] { "ProductsId" });
            DropIndex("dbo.CostumersProducts", new[] { "CostumersId" });
            AlterColumn("dbo.Costumers", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Costumers", "DocumentNumber", c => c.String());
            AlterColumn("dbo.Costumers", "Email", c => c.String());
            AlterColumn("dbo.Costumers", "LastName", c => c.String());
            AlterColumn("dbo.Costumers", "Name", c => c.String());
            DropTable("dbo.CostumersProducts");
            CreateIndex("dbo.UsersProducts", "ProductsId");
            CreateIndex("dbo.UsersProducts", "UsersId");
            AddForeignKey("dbo.UsersProducts", "UsersId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UsersProducts", "ProductsId", "dbo.Products", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.Costumers", newName: "Users");
        }
    }
}
