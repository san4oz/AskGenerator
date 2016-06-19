namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LimitFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "RightLimit_IsEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Questions", "LeftLimit_IsEnabled", c => c.Boolean(nullable: false));
            DropColumn("dbo.Questions", "RightLimit_AvgLimit");
            DropColumn("dbo.Questions", "LeftLimit_AvgLimit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "LeftLimit_AvgLimit", c => c.Single(nullable: false));
            AddColumn("dbo.Questions", "RightLimit_AvgLimit", c => c.Single(nullable: false));
            DropColumn("dbo.Questions", "LeftLimit_IsEnabled");
            DropColumn("dbo.Questions", "RightLimit_IsEnabled");
        }
    }
}
