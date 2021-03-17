namespace ComplaintsManagement.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Claims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimsOptionsId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        ProductsId = c.Int(nullable: false),
                        DepartmentsId = c.Int(nullable: false),
                        CompletedAt = c.DateTime(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClaimsOptions", t => t.ClaimsOptionsId, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DepartmentsId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductsId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.ClaimsOptionsId)
                .Index(t => t.StatusId)
                .Index(t => t.ProductsId)
                .Index(t => t.DepartmentsId);
            
            CreateTable(
                "dbo.ClaimsOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        DepartmentsId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(nullable: false, maxLength: 500),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Complaints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComplaintsOptionsId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        ProductsId = c.Int(nullable: false),
                        DepartmentsId = c.Int(nullable: false),
                        CompletedAt = c.DateTime(),
                        Comment = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ComplaintsOptions", t => t.ComplaintsOptionsId, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DepartmentsId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductsId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.ComplaintsOptionsId)
                .Index(t => t.StatusId)
                .Index(t => t.ProductsId)
                .Index(t => t.DepartmentsId);
            
            CreateTable(
                "dbo.ComplaintsOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        ProductsId = c.Int(),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductsId)
                .Index(t => t.ProductsId);
            
            CreateTable(
                "dbo.CustomersProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(nullable: false),
                        ProductsId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductsId, cascadeDelete: true)
                .Index(t => t.ProductsId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        LastName = c.String(),
                        DocumentNumber = c.String(),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CustomersProducts", "ProductsId", "dbo.Products");
            DropForeignKey("dbo.Complaints", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Complaints", "ProductsId", "dbo.Products");
            DropForeignKey("dbo.Complaints", "DepartmentsId", "dbo.Departments");
            DropForeignKey("dbo.Complaints", "ComplaintsOptionsId", "dbo.ComplaintsOptions");
            DropForeignKey("dbo.ComplaintsOptions", "ProductsId", "dbo.Products");
            DropForeignKey("dbo.Claims", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Claims", "ProductsId", "dbo.Products");
            DropForeignKey("dbo.Claims", "DepartmentsId", "dbo.Departments");
            DropForeignKey("dbo.Claims", "ClaimsOptionsId", "dbo.ClaimsOptions");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.CustomersProducts", new[] { "ProductsId" });
            DropIndex("dbo.ComplaintsOptions", new[] { "ProductsId" });
            DropIndex("dbo.Complaints", new[] { "DepartmentsId" });
            DropIndex("dbo.Complaints", new[] { "ProductsId" });
            DropIndex("dbo.Complaints", new[] { "StatusId" });
            DropIndex("dbo.Complaints", new[] { "ComplaintsOptionsId" });
            DropIndex("dbo.Claims", new[] { "DepartmentsId" });
            DropIndex("dbo.Claims", new[] { "ProductsId" });
            DropIndex("dbo.Claims", new[] { "StatusId" });
            DropIndex("dbo.Claims", new[] { "ClaimsOptionsId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.CustomersProducts");
            DropTable("dbo.ComplaintsOptions");
            DropTable("dbo.Complaints");
            DropTable("dbo.Status");
            DropTable("dbo.Products");
            DropTable("dbo.Departments");
            DropTable("dbo.ClaimsOptions");
            DropTable("dbo.Claims");
        }
    }
}
