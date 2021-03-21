namespace ComplaintsManagement.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class departmentsfkaddedtoComplaintsOptions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComplaintsOptions", "DepartmentsId", c => c.Int());
            CreateIndex("dbo.ComplaintsOptions", "DepartmentsId");
            AddForeignKey("dbo.ComplaintsOptions", "DepartmentsId", "dbo.Departments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComplaintsOptions", "DepartmentsId", "dbo.Departments");
            DropIndex("dbo.ComplaintsOptions", new[] { "DepartmentsId" });
            DropColumn("dbo.ComplaintsOptions", "DepartmentsId");
        }
    }
}
