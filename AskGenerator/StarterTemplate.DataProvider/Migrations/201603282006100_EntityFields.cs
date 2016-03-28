namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntityFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "Fields", c => c.String());
            AddColumn("dbo.Students", "Fields", c => c.String());
            AddColumn("dbo.Teachers", "Fields", c => c.String());
            AddColumn("dbo.Questions", "Fields", c => c.String());
            AddColumn("dbo.Subscribers", "Fields", c => c.String());
            AddColumn("dbo.Teams", "Fields", c => c.String());
            AddColumn("dbo.Votes", "Fields", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Votes", "Fields");
            DropColumn("dbo.Teams", "Fields");
            DropColumn("dbo.Subscribers", "Fields");
            DropColumn("dbo.Questions", "Fields");
            DropColumn("dbo.Teachers", "Fields");
            DropColumn("dbo.Students", "Fields");
            DropColumn("dbo.Groups", "Fields");
        }
    }
}
