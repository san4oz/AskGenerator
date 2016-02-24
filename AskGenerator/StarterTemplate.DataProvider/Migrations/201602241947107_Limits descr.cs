namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Limitsdescr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "RightLimit_ToolTip", c => c.String(maxLength: 50));
            AddColumn("dbo.Questions", "RightLimit_Description", c => c.String(maxLength: 250));
            AddColumn("dbo.Questions", "LeftLimit_ToolTip", c => c.String(maxLength: 50));
            AddColumn("dbo.Questions", "LeftLimit_Description", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "LeftLimit_Description");
            DropColumn("dbo.Questions", "LeftLimit_ToolTip");
            DropColumn("dbo.Questions", "RightLimit_Description");
            DropColumn("dbo.Questions", "RightLimit_ToolTip");
        }
    }
}
