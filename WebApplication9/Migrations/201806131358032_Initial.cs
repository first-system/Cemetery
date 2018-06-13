namespace WebApplication9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BurialPlaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NArea = c.Int(nullable: false),
                        NBurial = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Deceaseds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FName = c.String(nullable: false, maxLength: 20),
                        LName = c.String(nullable: false, maxLength: 50),
                        SName = c.String(nullable: false, maxLength: 50),
                        DOB = c.DateTime(),
                        DateDeath = c.DateTime(),
                        CategoryId = c.Int(),
                        BurialPlaseId = c.Int(),
                        Search = c.String(maxLength: 300),
                        Photo = c.String(),
                        Description = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BurialPlaces", t => t.BurialPlaseId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId)
                .Index(t => t.BurialPlaseId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 50),
                        Password = c.Guid(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Deceaseds", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Deceaseds", "BurialPlaseId", "dbo.BurialPlaces");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Deceaseds", new[] { "BurialPlaseId" });
            DropIndex("dbo.Deceaseds", new[] { "CategoryId" });
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Categories");
            DropTable("dbo.Deceaseds");
            DropTable("dbo.BurialPlaces");
        }
    }
}
