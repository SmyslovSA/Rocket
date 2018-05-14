namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTvSerias : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Code = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        CategoryCode = c.Int(nullable: false),
                        TvSeriasEntity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryCode, cascadeDelete: true)
                .ForeignKey("dbo.TvSerias", t => t.TvSeriasEntity_Id)
                .Index(t => t.CategoryCode)
                .Index(t => t.TvSeriasEntity_Id);
            
            CreateTable(
                "dbo.Episode",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReleaseDateRu = c.DateTime(nullable: false),
                        ReleaseDateEn = c.DateTime(nullable: false),
                        Number = c.Int(nullable: false),
                        TitleRu = c.String(nullable: false, maxLength: 250),
                        TitleEn = c.String(nullable: false, maxLength: 250),
                        DurationInMinutes = c.Double(),
                        UrlForEpisodeSource = c.String(nullable: false),
                        SeasonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Season", t => t.SeasonId, cascadeDelete: true)
                .Index(t => t.SeasonId);
            
            CreateTable(
                "dbo.Season",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        PosterImageUrl = c.String(),
                        TvSeriesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TvSerias", t => t.TvSeriesId, cascadeDelete: true)
                .Index(t => t.TvSeriesId);
            
            CreateTable(
                "dbo.TvSerias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TitleRu = c.String(nullable: false, maxLength: 250),
                        TitleEn = c.String(nullable: false, maxLength: 250),
                        PosterImageUrl = c.String(),
                        LostfilmRate = c.Double(),
                        RateImDb = c.Double(),
                        UrlToOfficialSite = c.String(),
                        CurrentStatus = c.String(maxLength: 50),
                        TvSerialCanal = c.String(maxLength: 150),
                        TvSerialYearStart = c.String(),
                        Summary = c.String(),
                        UrlToSource = c.String(),
                        ListGenreForParse = c.String(),
                        PremiereDateForParse = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullNameRu = c.String(nullable: false, maxLength: 250),
                        FullNameEn = c.String(nullable: false, maxLength: 250),
                        LostfilmPersonalPageUrl = c.String(nullable: false),
                        PhotoThumbnailUrl = c.String(),
                        PersonTypeCode = c.Int(nullable: false),
                        TvSeriasId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonType", t => t.PersonTypeCode, cascadeDelete: true)
                .Index(t => t.PersonTypeCode);
            
            CreateTable(
                "dbo.PersonType",
                c => new
                    {
                        Code = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.PersonsToTvSerias",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        TvSeriasId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PersonId, t.TvSeriasId })
                .ForeignKey("dbo.TvSerias", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.TvSeriasId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.TvSeriasId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Episode", "SeasonId", "dbo.Season");
            DropForeignKey("dbo.Season", "TvSeriesId", "dbo.TvSerias");
            DropForeignKey("dbo.PersonsToTvSerias", "TvSeriasId", "dbo.Person");
            DropForeignKey("dbo.PersonsToTvSerias", "PersonId", "dbo.TvSerias");
            DropForeignKey("dbo.Person", "PersonTypeCode", "dbo.PersonType");
            DropForeignKey("dbo.Genre", "TvSeriasEntity_Id", "dbo.TvSerias");
            DropForeignKey("dbo.Genre", "CategoryCode", "dbo.Category");
            DropIndex("dbo.PersonsToTvSerias", new[] { "TvSeriasId" });
            DropIndex("dbo.PersonsToTvSerias", new[] { "PersonId" });
            DropIndex("dbo.Person", new[] { "PersonTypeCode" });
            DropIndex("dbo.Season", new[] { "TvSeriesId" });
            DropIndex("dbo.Episode", new[] { "SeasonId" });
            DropIndex("dbo.Genre", new[] { "TvSeriasEntity_Id" });
            DropIndex("dbo.Genre", new[] { "CategoryCode" });
            DropTable("dbo.PersonsToTvSerias");
            DropTable("dbo.PersonType");
            DropTable("dbo.Person");
            DropTable("dbo.TvSerias");
            DropTable("dbo.Season");
            DropTable("dbo.Episode");
            DropTable("dbo.Genre");
            DropTable("dbo.Category");
        }
    }
}
