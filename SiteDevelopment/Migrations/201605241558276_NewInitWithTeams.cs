namespace SiteDevelopment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewInitWithTeams : DbMigration
    {
        public override void Up()
        {
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
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsId = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Title = c.String(),
                        MainText = c.String(),
                        PublicationTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Like = c.Int(nullable: false),
                        Dislike = c.Int(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NewsId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        IsAdmin = c.Boolean(nullable: false),
                        FirstName = c.String(),
                        Surname = c.String(),
                        Nickname = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        RememberMe = c.Boolean(nullable: false),
                        Avatar = c.String(),
                        City = c.String(),
                        DateOfBirth = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateOfRegistration = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        RoleId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Like = c.Int(nullable: false),
                        Dislike = c.Int(nullable: false),
                        PublicationTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        NewsId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .ForeignKey("dbo.News", t => t.NewsId, cascadeDelete: false)
                .Index(t => t.NewsId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        MatchId = c.Int(nullable: false, identity: true),
                        HomeTeamId = c.Int(nullable: false),
                        AwayTeamId = c.Int(nullable: false),
                        HomeTeamScore = c.Int(nullable: false),
                        AwayTeamScore = c.Int(nullable: false),
                        Result = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Team_TeamId = c.Int(),
                        Team_TeamId1 = c.Int(),
                        AwayTeam_TeamId = c.Int(),
                        HomeTeam_TeamId = c.Int(),
                    })
                .PrimaryKey(t => t.MatchId)
                .ForeignKey("dbo.Teams", t => t.Team_TeamId)
                .ForeignKey("dbo.Teams", t => t.Team_TeamId1)
                .ForeignKey("dbo.Teams", t => t.AwayTeam_TeamId)
                .ForeignKey("dbo.Teams", t => t.HomeTeam_TeamId)
                .Index(t => t.Team_TeamId)
                .Index(t => t.Team_TeamId1)
                .Index(t => t.AwayTeam_TeamId)
                .Index(t => t.HomeTeam_TeamId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamId = c.Int(nullable: false, identity: true),
                        Division = c.String(),
                        Name = c.String(),
                        CityArea = c.String(),
                        Arena = c.String(),
                        Logo = c.String(),
                        Conference = c.String(),
                        ShortTeamName = c.String(),
                    })
                .PrimaryKey(t => t.TeamId);
            
            CreateTable(
                "dbo.Standings",
                c => new
                    {
                        StandingsId = c.Int(nullable: false, identity: true),
                        TeamId = c.Int(nullable: false),
                        SeasonId = c.Int(nullable: false),
                        GamesPlayed = c.Int(nullable: false),
                        Wins = c.Int(nullable: false),
                        Losses = c.Int(nullable: false),
                        OT = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                        ROW = c.Int(nullable: false),
                        GoalsFor = c.Int(nullable: false),
                        GoalsAgainst = c.Int(nullable: false),
                        GoalDifferential = c.Int(nullable: false),
                        HomeWins = c.Int(nullable: false),
                        HomeLosses = c.Int(nullable: false),
                        HomeOT = c.Int(nullable: false),
                        AwayWins = c.Int(nullable: false),
                        AwayLosses = c.Int(nullable: false),
                        AwayOT = c.Int(nullable: false),
                        ShootoutWins = c.Int(nullable: false),
                        ShootoutLosses = c.Int(nullable: false),
                        LastWins = c.Int(nullable: false),
                        LastLosses = c.Int(nullable: false),
                        LastOT = c.Int(nullable: false),
                        Streak = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StandingsId)
                .ForeignKey("dbo.Seasons", t => t.SeasonId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.SeasonId);
            
            CreateTable(
                "dbo.Seasons",
                c => new
                    {
                        SeasonId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        IsCurrent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SeasonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "HomeTeam_TeamId", "dbo.Teams");
            DropForeignKey("dbo.Matches", "AwayTeam_TeamId", "dbo.Teams");
            DropForeignKey("dbo.Standings", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Standings", "SeasonId", "dbo.Seasons");
            DropForeignKey("dbo.Matches", "Team_TeamId1", "dbo.Teams");
            DropForeignKey("dbo.Matches", "Team_TeamId", "dbo.Teams");
            DropForeignKey("dbo.Bundles", "TagId", "dbo.Tags");
            DropForeignKey("dbo.Bundles", "NewsId", "dbo.News");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.News", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "NewsId", "dbo.News");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropIndex("dbo.Standings", new[] { "SeasonId" });
            DropIndex("dbo.Standings", new[] { "TeamId" });
            DropIndex("dbo.Matches", new[] { "HomeTeam_TeamId" });
            DropIndex("dbo.Matches", new[] { "AwayTeam_TeamId" });
            DropIndex("dbo.Matches", new[] { "Team_TeamId1" });
            DropIndex("dbo.Matches", new[] { "Team_TeamId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "NewsId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.News", new[] { "UserId" });
            DropIndex("dbo.Bundles", new[] { "TagId" });
            DropIndex("dbo.Bundles", new[] { "NewsId" });
            DropTable("dbo.Seasons");
            DropTable("dbo.Standings");
            DropTable("dbo.Teams");
            DropTable("dbo.Matches");
            DropTable("dbo.Tags");
            DropTable("dbo.Roles");
            DropTable("dbo.Comments");
            DropTable("dbo.Users");
            DropTable("dbo.News");
            DropTable("dbo.Bundles");
        }
    }
}
