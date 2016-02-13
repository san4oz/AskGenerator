namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Team1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "TeamId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "TeamId");
        }
    }
}
