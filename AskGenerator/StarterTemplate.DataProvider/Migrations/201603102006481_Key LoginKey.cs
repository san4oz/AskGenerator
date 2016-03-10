namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KeyLoginKey : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.AspNetUsers", "LoginKey", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "LoginKey" });
        }
    }
}
