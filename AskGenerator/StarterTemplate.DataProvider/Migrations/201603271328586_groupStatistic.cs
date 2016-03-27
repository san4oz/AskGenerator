namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class groupStatistic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "Avg", c => c.Single(nullable: false));
            AddColumn("dbo.Groups", "VotesCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "VotesCount");
            DropColumn("dbo.Groups", "Avg");
        }
    }
}
