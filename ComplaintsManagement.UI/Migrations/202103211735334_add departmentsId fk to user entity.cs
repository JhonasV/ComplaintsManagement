namespace ComplaintsManagement.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddepartmentsIdfktouserentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DepartmentId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "DepartmentId");
            AddForeignKey("dbo.AspNetUsers", "DepartmentId", "dbo.Departments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.AspNetUsers", new[] { "DepartmentId" });
            DropColumn("dbo.AspNetUsers", "DepartmentId");
        }
    }
}
