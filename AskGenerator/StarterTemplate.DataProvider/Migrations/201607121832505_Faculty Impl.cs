namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FacultyImpl : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "GroupId", "dbo.Groups");
            DropIndex("dbo.AspNetUsers", new[] { "GroupId" });
            AddColumn("dbo.Groups", "FacultyId", c => c.String(maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "GroupId", c => c.String(maxLength: 64));
            CreateIndex("dbo.Groups", "FacultyId");
            AddForeignKey("dbo.Groups", "FacultyId", "dbo.Faculties", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groups", "FacultyId", "dbo.Faculties");
            DropIndex("dbo.Groups", new[] { "FacultyId" });
            AlterColumn("dbo.AspNetUsers", "GroupId", c => c.String(maxLength: 128));
            DropColumn("dbo.Groups", "FacultyId");
            CreateIndex("dbo.AspNetUsers", "GroupId");
            AddForeignKey("dbo.AspNetUsers", "GroupId", "dbo.Groups", "Id");
        }
    }
}
