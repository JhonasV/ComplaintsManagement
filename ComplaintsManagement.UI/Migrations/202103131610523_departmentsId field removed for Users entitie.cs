namespace ComplaintsManagement.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class departmentsIdfieldremovedforUsersentitie : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "DepartmentsId" });
            DropForeignKey("dbo.AspNetUsers", "DepartmentsId", "dbo.Departments");

            DropColumn("dbo.AspNetUsers", "DepartmentsId");



        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "DepartmentsId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "DepartmentsId");
            AddForeignKey("dbo.AspNetUsers", "DepartmentsId", "dbo.Departments", "Id");
        }
    }
}
