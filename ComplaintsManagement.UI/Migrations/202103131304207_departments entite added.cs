namespace ComplaintsManagement.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class departmentsentiteadded : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Deparments", newName: "Departments");
            DropColumn("dbo.AspNetUsers", "DepartmentsId");
            RenameColumn(table: "dbo.AspNetUsers", name: "Deparment_Id", newName: "DepartmentsId");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Deparment_Id", newName: "IX_DepartmentsId");
            AddColumn("dbo.Complaints", "DepartmentsId", c => c.Int(nullable: false));
            AddColumn("dbo.Departments", "Name", c => c.String());
            CreateIndex("dbo.Complaints", "DepartmentsId");
            AddForeignKey("dbo.Complaints", "DepartmentsId", "dbo.Departments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Complaints", "DepartmentsId", "dbo.Departments");
            DropIndex("dbo.Complaints", new[] { "DepartmentsId" });
            DropColumn("dbo.Departments", "Name");
            DropColumn("dbo.Complaints", "DepartmentsId");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_DepartmentsId", newName: "IX_Deparment_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "DepartmentsId", newName: "Deparment_Id");
            AddColumn("dbo.AspNetUsers", "DepartmentsId", c => c.Int());
            RenameTable(name: "dbo.Departments", newName: "Deparments");
        }
    }
}
