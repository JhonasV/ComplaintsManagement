namespace ComplaintsManagement.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClaimsOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        ComplaintsOptionsId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Complaints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomersId = c.Int(nullable: false),
                        AgentId = c.Int(nullable: false),
                        ClaimsOptionsId = c.Int(nullable: false),
                        ComplaintsOptionsId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        ProductsId = c.Int(nullable: false),
                        CompletedAt = c.DateTime(nullable: false),
                        Commment = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClaimsOptions", t => t.ClaimsOptionsId, cascadeDelete: true)
                .ForeignKey("dbo.ComplaintsOptions", t => t.ComplaintsOptionsId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductsId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.ClaimsOptionsId)
                .Index(t => t.ComplaintsOptionsId)
                .Index(t => t.StatusId)
                .Index(t => t.ProductsId);
            
            CreateTable(
                "dbo.ComplaintsOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        ProductsId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductsId, cascadeDelete: false)
                .Index(t => t.ProductsId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(nullable: false, maxLength: 500),
                        Active = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Active = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Active = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(nullable: false, maxLength: 1000),
                        DocumentNumber = c.String(),
                        PhoneNumber = c.String(),
                        Active = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductsId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UsersId, cascadeDelete: true)
                .Index(t => t.UsersId)
                .Index(t => t.ProductsId);
            
            CreateTable(
                "dbo.UsersRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UsersId = c.Int(nullable: false),
                        RolesId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RolesId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UsersId, cascadeDelete: true)
                .Index(t => t.UsersId)
                .Index(t => t.RolesId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersRoles", "UsersId", "dbo.Users");
            DropForeignKey("dbo.UsersRoles", "RolesId", "dbo.Roles");
            DropForeignKey("dbo.UsersProducts", "UsersId", "dbo.Users");
            DropForeignKey("dbo.UsersProducts", "ProductsId", "dbo.Products");
            DropForeignKey("dbo.Complaints", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Complaints", "ProductsId", "dbo.Products");
            DropForeignKey("dbo.Complaints", "ComplaintsOptionsId", "dbo.ComplaintsOptions");
            DropForeignKey("dbo.ComplaintsOptions", "ProductsId", "dbo.Products");
            DropForeignKey("dbo.Complaints", "ClaimsOptionsId", "dbo.ClaimsOptions");
            DropIndex("dbo.UsersRoles", new[] { "RolesId" });
            DropIndex("dbo.UsersRoles", new[] { "UsersId" });
            DropIndex("dbo.UsersProducts", new[] { "ProductsId" });
            DropIndex("dbo.UsersProducts", new[] { "UsersId" });
            DropIndex("dbo.ComplaintsOptions", new[] { "ProductsId" });
            DropIndex("dbo.Complaints", new[] { "ProductsId" });
            DropIndex("dbo.Complaints", new[] { "StatusId" });
            DropIndex("dbo.Complaints", new[] { "ComplaintsOptionsId" });
            DropIndex("dbo.Complaints", new[] { "ClaimsOptionsId" });
            DropTable("dbo.UsersRoles");
            DropTable("dbo.UsersProducts");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Status");
            DropTable("dbo.Products");
            DropTable("dbo.ComplaintsOptions");
            DropTable("dbo.Complaints");
            DropTable("dbo.ClaimsOptions");
        }
    }
}
