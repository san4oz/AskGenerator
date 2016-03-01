namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentHasUserAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "HasUserAccount", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "HasUserAccount");
        }
    }
}
