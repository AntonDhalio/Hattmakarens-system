namespace Hattmakarens_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderIdMaterial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Hats", "MaterialModels_Id", "dbo.MaterialModels");
            DropForeignKey("dbo.Hats", "OrderModels_Id", "dbo.OrderModels");
            DropIndex("dbo.Hats", new[] { "OrderModels_Id" });
            DropIndex("dbo.Hats", new[] { "MaterialModels_Id" });
            RenameColumn(table: "dbo.Hats", name: "OrderModels_Id", newName: "OrderId");
            CreateTable(
                "dbo.HatsMaterialModels",
                c => new
                    {
                        Hats_Id = c.Int(nullable: false),
                        MaterialModels_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Hats_Id, t.MaterialModels_Id })
                .ForeignKey("dbo.Hats", t => t.Hats_Id, cascadeDelete: true)
                .ForeignKey("dbo.MaterialModels", t => t.MaterialModels_Id, cascadeDelete: true)
                .Index(t => t.Hats_Id)
                .Index(t => t.MaterialModels_Id);
            
            AlterColumn("dbo.Hats", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Hats", "OrderId");
            AddForeignKey("dbo.Hats", "OrderId", "dbo.OrderModels", "Id", cascadeDelete: true);
            DropColumn("dbo.Hats", "MaterialModels_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Hats", "MaterialModels_Id", c => c.Int());
            DropForeignKey("dbo.Hats", "OrderId", "dbo.OrderModels");
            DropForeignKey("dbo.HatsMaterialModels", "MaterialModels_Id", "dbo.MaterialModels");
            DropForeignKey("dbo.HatsMaterialModels", "Hats_Id", "dbo.Hats");
            DropIndex("dbo.HatsMaterialModels", new[] { "MaterialModels_Id" });
            DropIndex("dbo.HatsMaterialModels", new[] { "Hats_Id" });
            DropIndex("dbo.Hats", new[] { "OrderId" });
            AlterColumn("dbo.Hats", "OrderId", c => c.Int());
            DropTable("dbo.HatsMaterialModels");
            RenameColumn(table: "dbo.Hats", name: "OrderId", newName: "OrderModels_Id");
            CreateIndex("dbo.Hats", "MaterialModels_Id");
            CreateIndex("dbo.Hats", "OrderModels_Id");
            AddForeignKey("dbo.Hats", "OrderModels_Id", "dbo.OrderModels", "Id");
            AddForeignKey("dbo.Hats", "MaterialModels_Id", "dbo.MaterialModels", "Id");
        }
    }
}
