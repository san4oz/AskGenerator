namespace AskGenerator.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class History : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Histories", new[] { "HistoryPrefix" });
            DropTable("dbo.Histories");
        }
    }
}
