namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignGroupkeyforstudent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "StudentId", c => c.String());
            AlterColumn("dbo.AspNetUsers", "GroupId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "GroupId");
            AddForeignKey("dbo.AspNetUsers", "GroupId", "dbo.Groups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "GroupId", "dbo.Groups");
            DropIndex("dbo.AspNetUsers", new[] { "GroupId" });
            AlterColumn("dbo.AspNetUsers", "GroupId", c => c.String());
            DropColumn("dbo.AspNetUsers", "StudentId");
        }
    }
}
