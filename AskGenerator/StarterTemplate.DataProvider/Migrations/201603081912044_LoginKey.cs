namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoginKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "AccountId", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "LoginKey", c => c.String(maxLength: 8));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LoginKey");
            DropColumn("dbo.Students", "AccountId");
        }
    }
}
