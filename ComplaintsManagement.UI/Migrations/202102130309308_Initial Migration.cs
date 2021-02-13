namespace ComplaintsManagement.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UsersRoles", "RolesId", "dbo.Roles");
            DropForeignKey("dbo.UsersRoles", "UsersId", "dbo.Users");
            DropIndex("dbo.UsersRoles", new[] { "UsersId" });
            DropIndex("dbo.UsersRoles", new[] { "RolesId" });
            AlterColumn("dbo.Users", "Name", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
            DropTable("dbo.UsersRoles");
            DropTable("dbo.Roles");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.UsersRoles", "RolesId");
            CreateIndex("dbo.UsersRoles", "UsersId");
            AddForeignKey("dbo.UsersRoles", "UsersId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UsersRoles", "RolesId", "dbo.Roles", "Id", cascadeDelete: true);
        }
    }
}
