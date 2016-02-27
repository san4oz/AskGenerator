namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IndexFirstName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "FirstName", c => c.String(maxLength: 127));
            AlterColumn("dbo.Teachers", "FirstName", c => c.String(maxLength: 127));
            CreateIndex("dbo.Students", "FirstName");
            CreateIndex("dbo.Teachers", "FirstName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Teachers", new[] { "FirstName" });
            DropIndex("dbo.Students", new[] { "FirstName" });
            AlterColumn("dbo.Teachers", "FirstName", c => c.String());
            AlterColumn("dbo.Students", "FirstName", c => c.String());
        }
    }
}
