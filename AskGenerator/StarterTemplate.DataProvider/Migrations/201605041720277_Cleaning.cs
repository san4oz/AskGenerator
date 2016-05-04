namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cleaning : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Questions", "IsAboutTeacher");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "IsAboutTeacher", c => c.Boolean(nullable: false));
        }
    }
}
