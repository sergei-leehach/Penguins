namespace SiteDevelopment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Bundle : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tags", "NewsId", "dbo.News");
            DropIndex("dbo.Tags", new[] { "NewsId" });
            CreateTable(
                "dbo.Bundles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NewsId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.News", t => t.NewsId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.NewsId)
                .Index(t => t.TagId);
            
            DropColumn("dbo.Tags", "NewsId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tags", "NewsId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Bundles", "TagId", "dbo.Tags");
            DropForeignKey("dbo.Bundles", "NewsId", "dbo.News");
            DropIndex("dbo.Bundles", new[] { "TagId" });
            DropIndex("dbo.Bundles", new[] { "NewsId" });
            DropTable("dbo.Bundles");
            CreateIndex("dbo.Tags", "NewsId");
            AddForeignKey("dbo.Tags", "NewsId", "dbo.News", "NewsId", cascadeDelete: true);
        }
    }
}
