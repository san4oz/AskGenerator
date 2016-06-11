namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NOtMappedfieldstogroup : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Groups", "Avg");
            DropColumn("dbo.Groups", "VotesCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Groups", "VotesCount", c => c.Int(nullable: false));
            AddColumn("dbo.Groups", "Avg", c => c.Single(nullable: false));
        }
    }
}
