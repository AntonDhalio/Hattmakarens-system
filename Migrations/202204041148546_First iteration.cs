namespace Hattmakarens_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Firstiteration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ColorModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MaterialModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Name = c.String(),
                        Type = c.String(),
                        ColorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ColorModels", t => t.ColorId, cascadeDelete: true)
                .Index(t => t.ColorId);
            
            CreateTable(
                "dbo.HatModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Size = c.String(),
                        Price = c.Double(nullable: false),
                        Status = c.String(),
                        Comment = c.String(),
                        UserId = c.String(maxLength: 128),
                        ModelID = c.Int(nullable: false),
                        OrderModels_Id = c.Int(),
                        MaterialModels_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HatModels", t => t.ModelID, cascadeDelete: true)
                .ForeignKey("dbo.OrderModels", t => t.OrderModels_Id)
                .ForeignKey("dbo.UserModels", t => t.UserId)
                .ForeignKey("dbo.MaterialModels", t => t.MaterialModels_Id)
                .Index(t => t.UserId)
                .Index(t => t.ModelID)
                .Index(t => t.OrderModels_Id)
                .Index(t => t.MaterialModels_Id);
            
            CreateTable(
                "dbo.ImageModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Priority = c.Boolean(nullable: false),
                        Status = c.String(),
                        Moms = c.Double(nullable: false),
                        TotalSum = c.Double(nullable: false),
                        Comment = c.String(),
                        UserId = c.String(maxLength: 128),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerModels", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.UserModels", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.CustomerModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.Int(nullable: false),
                        Email = c.String(),
                        Adress = c.String(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.ImageModelsHatModels",
                c => new
                    {
                        ImageModels_Id = c.Int(nullable: false),
                        HatModels_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ImageModels_Id, t.HatModels_Id })
                .ForeignKey("dbo.ImageModels", t => t.ImageModels_Id, cascadeDelete: true)
                .ForeignKey("dbo.HatModels", t => t.HatModels_Id, cascadeDelete: true)
                .Index(t => t.ImageModels_Id)
                .Index(t => t.HatModels_Id);
            
            CreateTable(
                "dbo.ImageModelsHats",
                c => new
                    {
                        ImageModels_Id = c.Int(nullable: false),
                        Hats_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ImageModels_Id, t.Hats_Id })
                .ForeignKey("dbo.ImageModels", t => t.ImageModels_Id, cascadeDelete: true)
                .ForeignKey("dbo.Hats", t => t.Hats_Id, cascadeDelete: true)
                .Index(t => t.ImageModels_Id)
                .Index(t => t.Hats_Id);
            
            CreateTable(
                "dbo.HatModelsMaterialModels",
                c => new
                    {
                        HatModels_Id = c.Int(nullable: false),
                        MaterialModels_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HatModels_Id, t.MaterialModels_Id })
                .ForeignKey("dbo.HatModels", t => t.HatModels_Id, cascadeDelete: true)
                .ForeignKey("dbo.MaterialModels", t => t.MaterialModels_Id, cascadeDelete: true)
                .Index(t => t.HatModels_Id)
                .Index(t => t.MaterialModels_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Hats", "MaterialModels_Id", "dbo.MaterialModels");
            DropForeignKey("dbo.HatModelsMaterialModels", "MaterialModels_Id", "dbo.MaterialModels");
            DropForeignKey("dbo.HatModelsMaterialModels", "HatModels_Id", "dbo.HatModels");
            DropForeignKey("dbo.Hats", "UserId", "dbo.UserModels");
            DropForeignKey("dbo.OrderModels", "UserId", "dbo.UserModels");
            DropForeignKey("dbo.Hats", "OrderModels_Id", "dbo.OrderModels");
            DropForeignKey("dbo.OrderModels", "CustomerId", "dbo.CustomerModels");
            DropForeignKey("dbo.Hats", "ModelID", "dbo.HatModels");
            DropForeignKey("dbo.ImageModelsHats", "Hats_Id", "dbo.Hats");
            DropForeignKey("dbo.ImageModelsHats", "ImageModels_Id", "dbo.ImageModels");
            DropForeignKey("dbo.ImageModelsHatModels", "HatModels_Id", "dbo.HatModels");
            DropForeignKey("dbo.ImageModelsHatModels", "ImageModels_Id", "dbo.ImageModels");
            DropForeignKey("dbo.MaterialModels", "ColorId", "dbo.ColorModels");
            DropIndex("dbo.HatModelsMaterialModels", new[] { "MaterialModels_Id" });
            DropIndex("dbo.HatModelsMaterialModels", new[] { "HatModels_Id" });
            DropIndex("dbo.ImageModelsHats", new[] { "Hats_Id" });
            DropIndex("dbo.ImageModelsHats", new[] { "ImageModels_Id" });
            DropIndex("dbo.ImageModelsHatModels", new[] { "HatModels_Id" });
            DropIndex("dbo.ImageModelsHatModels", new[] { "ImageModels_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.OrderModels", new[] { "CustomerId" });
            DropIndex("dbo.OrderModels", new[] { "UserId" });
            DropIndex("dbo.Hats", new[] { "MaterialModels_Id" });
            DropIndex("dbo.Hats", new[] { "OrderModels_Id" });
            DropIndex("dbo.Hats", new[] { "ModelID" });
            DropIndex("dbo.Hats", new[] { "UserId" });
            DropIndex("dbo.MaterialModels", new[] { "ColorId" });
            DropTable("dbo.HatModelsMaterialModels");
            DropTable("dbo.ImageModelsHats");
            DropTable("dbo.ImageModelsHatModels");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.CustomerModels");
            DropTable("dbo.OrderModels");
            DropTable("dbo.UserModels");
            DropTable("dbo.ImageModels");
            DropTable("dbo.Hats");
            DropTable("dbo.HatModels");
            DropTable("dbo.MaterialModels");
            DropTable("dbo.ColorModels");
        }
    }
}
