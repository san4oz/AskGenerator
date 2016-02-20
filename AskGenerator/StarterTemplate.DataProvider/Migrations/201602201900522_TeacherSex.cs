namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeacherSex : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "IsMale", c => c.Boolean(nullable: false));
            AddColumn("dbo.Teachers", "IsMale", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "IsMale");
            DropColumn("dbo.Students", "IsMale");
        }
    }
}
