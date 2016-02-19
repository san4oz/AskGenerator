namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeacheQuestion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Votes", "Account_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Votes", "Teacher_Id", "dbo.Teachers");
            DropIndex("dbo.Votes", new[] { "Account_Id" });
            DropIndex("dbo.Votes", new[] { "Teacher_Id" });
            RenameColumn(table: "dbo.Votes", name: "Question_Id", newName: "QuestionId_Id");
            RenameIndex(table: "dbo.Votes", name: "IX_Question_Id", newName: "IX_QuestionId_Id");
            CreateTable(
                "dbo.TeacherQuestions",
                c => new
                    {
                        TeacherId = c.String(nullable: false, maxLength: 128),
                        QuestionId = c.String(nullable: false, maxLength: 128),
                        Answer = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeacherId, t.QuestionId })
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId)
                .Index(t => t.QuestionId);
            
            AddColumn("dbo.Votes", "TeacherId", c => c.String());
            AddColumn("dbo.Votes", "AccountId", c => c.String());
            DropColumn("dbo.Votes", "Account_Id");
            DropColumn("dbo.Votes", "Teacher_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Votes", "Teacher_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Votes", "Account_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.TeacherQuestions", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.TeacherQuestions", "QuestionId", "dbo.Questions");
            DropIndex("dbo.TeacherQuestions", new[] { "QuestionId" });
            DropIndex("dbo.TeacherQuestions", new[] { "TeacherId" });
            DropColumn("dbo.Votes", "AccountId");
            DropColumn("dbo.Votes", "TeacherId");
            DropTable("dbo.TeacherQuestions");
            RenameIndex(table: "dbo.Votes", name: "IX_QuestionId_Id", newName: "IX_Question_Id");
            RenameColumn(table: "dbo.Votes", name: "QuestionId_Id", newName: "Question_Id");
            CreateIndex("dbo.Votes", "Teacher_Id");
            CreateIndex("dbo.Votes", "Account_Id");
            AddForeignKey("dbo.Votes", "Teacher_Id", "dbo.Teachers", "Id");
            AddForeignKey("dbo.Votes", "Account_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
