namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignStudentkeyforuser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "StudentId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "StudentId");
            AddForeignKey("dbo.AspNetUsers", "StudentId", "dbo.Students", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "StudentId", "dbo.Students");
            DropIndex("dbo.AspNetUsers", new[] { "StudentId" });
            AlterColumn("dbo.AspNetUsers", "StudentId", c => c.String());
        }
    }
}
