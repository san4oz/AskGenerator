namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeVote : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Votes", "Account_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Votes", "Teacher_Id", "dbo.Teachers");
            DropIndex("dbo.Votes", new[] { "Account_Id" });
            DropIndex("dbo.Votes", new[] { "Teacher_Id" });
            RenameColumn(table: "dbo.Votes", name: "Question_Id", newName: "QuestionId_Id");
            RenameIndex(table: "dbo.Votes", name: "IX_Question_Id", newName: "IX_QuestionId_Id");
            AddColumn("dbo.Votes", "TeacherId", c => c.String());
            AddColumn("dbo.Votes", "AccountId", c => c.String());
            DropColumn("dbo.Votes", "Account_Id");
            DropColumn("dbo.Votes", "Teacher_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Votes", "Teacher_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Votes", "Account_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Votes", "AccountId");
            DropColumn("dbo.Votes", "TeacherId");
            RenameIndex(table: "dbo.Votes", name: "IX_QuestionId_Id", newName: "IX_Question_Id");
            RenameColumn(table: "dbo.Votes", name: "QuestionId_Id", newName: "Question_Id");
            CreateIndex("dbo.Votes", "Teacher_Id");
            CreateIndex("dbo.Votes", "Account_Id");
            AddForeignKey("dbo.Votes", "Teacher_Id", "dbo.Teachers", "Id");
            AddForeignKey("dbo.Votes", "Account_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
