namespace ComplaintsManagement.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CostumersentityrenamedtoCustomers : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Costumers", newName: "Customers");
            DropForeignKey("dbo.CostumersProducts", "CostumersId", "dbo.Costumers");
            DropIndex("dbo.CustomersProducts", new[] { "CostumersId" });
            RenameTable(name: "dbo.CostumersProducts", newName: "CustomersProducts");
            AddColumn("dbo.CustomersProducts", "Customers_Id", c => c.Int());
            CreateIndex("dbo.CustomersProducts", "Customers_Id");
            AddForeignKey("dbo.CustomersProducts", "Customers_Id", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomersProducts", "Customers_Id", "dbo.Customers");
            DropIndex("dbo.CustomersProducts", new[] { "Customers_Id" });
            DropColumn("dbo.CustomersProducts", "Customers_Id");
            CreateIndex("dbo.CustomersProducts", "CostumersId");
            AddForeignKey("dbo.CostumersProducts", "CostumersId", "dbo.Costumers", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.CustomersProducts", newName: "CostumersProducts");
            RenameTable(name: "dbo.Customers", newName: "Costumers");
        }
    }
}
