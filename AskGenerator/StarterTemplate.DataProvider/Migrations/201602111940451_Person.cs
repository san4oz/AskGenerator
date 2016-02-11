namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Person : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "Image", c => c.String());
            DropColumn("dbo.Groups", "FirstName");
            DropColumn("dbo.Groups", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Groups", "LastName", c => c.String());
            AddColumn("dbo.Groups", "FirstName", c => c.String());
            DropColumn("dbo.Teachers", "Image");
        }
    }
}
