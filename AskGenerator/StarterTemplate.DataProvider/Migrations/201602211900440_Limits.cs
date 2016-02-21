namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Limits : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "RightLimit_AvgLimit", c => c.Single(nullable: false));
            AddColumn("dbo.Questions", "RightLimit_Image", c => c.String());
            AddColumn("dbo.Questions", "LeftLimit_AvgLimit", c => c.Single(nullable: false));
            AddColumn("dbo.Questions", "LeftLimit_Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "LeftLimit_Image");
            DropColumn("dbo.Questions", "LeftLimit_AvgLimit");
            DropColumn("dbo.Questions", "RightLimit_Image");
            DropColumn("dbo.Questions", "RightLimit_AvgLimit");
        }
    }
}
