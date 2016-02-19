namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeacheQuestion1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TeacherQuestions", "Answer", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TeacherQuestions", "Answer", c => c.Int(nullable: false));
        }
    }
}
