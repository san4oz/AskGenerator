namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 128),
                        ShortName = c.String(maxLength: 8),
                        Fields = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        FacultyId = c.String(maxLength: 128),
                        Fields = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Faculties", t => t.FacultyId)
                .Index(t => t.FacultyId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        HasUserAccount = c.Boolean(nullable: false),
                        AccountId = c.String(maxLength: 128),
                        FirstName = c.String(maxLength: 127),
                        LastName = c.String(),
                        Image = c.String(),
                        IsMale = c.Boolean(nullable: false),
                        Fields = c.String(),
                        Group_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .Index(t => t.FirstName)
                .Index(t => t.Group_Id);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        TeamId = c.String(),
                        FirstName = c.String(maxLength: 127),
                        LastName = c.String(),
                        Image = c.String(),
                        IsMale = c.Boolean(nullable: false),
                        Fields = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.FirstName);
            
            CreateTable(
                "dbo.Histories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        HistoryPrefix = c.String(maxLength: 16),
                        Fields = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.HistoryPrefix);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        QuestionBody = c.String(),
                        LowerRateDescription = c.String(),
                        HigherRateDescription = c.String(),
                        Order = c.Int(nullable: false),
                        RightLimit_Image = c.String(),
                        RightLimit_ToolTip = c.String(maxLength: 50),
                        RightLimit_Description = c.String(maxLength: 250),
                        RightLimit_IsEnabled = c.Boolean(nullable: false),
                        LeftLimit_Image = c.String(),
                        LeftLimit_ToolTip = c.String(maxLength: 50),
                        LeftLimit_Description = c.String(maxLength: 250),
                        LeftLimit_IsEnabled = c.Boolean(nullable: false),
                        Fields = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Fields = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subscribers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 128),
                        Fields = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.TeacherQuestions",
                c => new
                    {
                        TeacherId = c.String(nullable: false, maxLength: 128),
                        QuestionId = c.String(nullable: false, maxLength: 128),
                        Answer = c.Single(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeacherId, t.QuestionId })
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Fields = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        GroupId = c.String(maxLength: 64),
                        Id = c.String(nullable: false, maxLength: 128),
                        StudentId = c.String(maxLength: 128),
                        LoginKey = c.String(maxLength: 8),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.LoginKey, unique: true)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Answer = c.Short(nullable: false),
                        TeacherId = c.String(),
                        AccountId = c.String(),
                        Fields = c.String(),
                        QuestionId_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId_Id)
                .Index(t => t.QuestionId_Id);
            
            CreateTable(
                "dbo.TeacherGroups",
                c => new
                    {
                        Teacher_Id = c.String(nullable: false, maxLength: 128),
                        Group_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Teacher_Id, t.Group_Id })
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .Index(t => t.Teacher_Id)
                .Index(t => t.Group_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "QuestionId_Id", "dbo.Questions");
            DropForeignKey("dbo.AspNetUsers", "StudentId", "dbo.Students");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeacherQuestions", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.TeacherQuestions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.TeacherGroups", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.TeacherGroups", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.Students", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Groups", "FacultyId", "dbo.Faculties");
            DropIndex("dbo.TeacherGroups", new[] { "Group_Id" });
            DropIndex("dbo.TeacherGroups", new[] { "Teacher_Id" });
            DropIndex("dbo.Votes", new[] { "QuestionId_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "LoginKey" });
            DropIndex("dbo.AspNetUsers", new[] { "StudentId" });
            DropIndex("dbo.TeacherQuestions", new[] { "QuestionId" });
            DropIndex("dbo.TeacherQuestions", new[] { "TeacherId" });
            DropIndex("dbo.Subscribers", new[] { "Email" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Histories", new[] { "HistoryPrefix" });
            DropIndex("dbo.Teachers", new[] { "FirstName" });
            DropIndex("dbo.Students", new[] { "Group_Id" });
            DropIndex("dbo.Students", new[] { "FirstName" });
            DropIndex("dbo.Groups", new[] { "FacultyId" });
            DropTable("dbo.TeacherGroups");
            DropTable("dbo.Votes");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Teams");
            DropTable("dbo.TeacherQuestions");
            DropTable("dbo.Subscribers");
            DropTable("dbo.Settings");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Questions");
            DropTable("dbo.Histories");
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.Groups");
            DropTable("dbo.Faculties");
        }
    }
}
