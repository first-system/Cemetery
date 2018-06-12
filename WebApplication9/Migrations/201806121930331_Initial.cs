namespace WebApplication9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deceaseds", "Description", c => c.String(maxLength: 200));
            AlterColumn("dbo.Deceaseds", "FName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Deceaseds", "LName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Deceaseds", "SName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Deceaseds", "SName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Deceaseds", "LName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Deceaseds", "FName", c => c.String(maxLength: 20));
            DropColumn("dbo.Deceaseds", "Description");
        }
    }
}
