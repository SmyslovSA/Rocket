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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryCode, cascadeDelete: true)
                .Index(t => t.CategoryCode);
            
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
                "dbo.Episode",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReleaseDateRu = c.DateTime(),
                        ReleaseDateEn = c.DateTime(),
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
                "dbo.TvSeriasToGenres",
                c => new
                    {
                        TvSeriasId = c.Int(nullable: false),
                        GenreId = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.TvSeriasId, t.GenreId })
                .ForeignKey("dbo.TvSerias", t => t.TvSeriasId, cascadeDelete: true)
                .ForeignKey("dbo.Genre", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.TvSeriasId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.TvSeriasToPersons",
                c => new
                    {
                        TvSeriasId = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TvSeriasId, t.PersonId })
                .ForeignKey("dbo.TvSerias", t => t.TvSeriasId, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.TvSeriasId)
                .Index(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Season", "TvSeriesId", "dbo.TvSerias");
            DropForeignKey("dbo.Episode", "SeasonId", "dbo.Season");
            DropForeignKey("dbo.TvSeriasToPersons", "PersonId", "dbo.Person");
            DropForeignKey("dbo.TvSeriasToPersons", "TvSeriasId", "dbo.TvSerias");
            DropForeignKey("dbo.Person", "PersonTypeCode", "dbo.PersonType");
            DropForeignKey("dbo.TvSeriasToGenres", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.TvSeriasToGenres", "TvSeriasId", "dbo.TvSerias");
            DropForeignKey("dbo.Genre", "CategoryCode", "dbo.Category");
            DropIndex("dbo.TvSeriasToPersons", new[] { "PersonId" });
            DropIndex("dbo.TvSeriasToPersons", new[] { "TvSeriasId" });
            DropIndex("dbo.TvSeriasToGenres", new[] { "GenreId" });
            DropIndex("dbo.TvSeriasToGenres", new[] { "TvSeriasId" });
            DropIndex("dbo.Episode", new[] { "SeasonId" });
            DropIndex("dbo.Season", new[] { "TvSeriesId" });
            DropIndex("dbo.Person", new[] { "PersonTypeCode" });
            DropIndex("dbo.Genre", new[] { "CategoryCode" });
            DropTable("dbo.TvSeriasToPersons");
            DropTable("dbo.TvSeriasToGenres");
            DropTable("dbo.Episode");
            DropTable("dbo.Season");
            DropTable("dbo.PersonType");
            DropTable("dbo.Person");
            DropTable("dbo.TvSerias");
            DropTable("dbo.Genre");
            DropTable("dbo.Category");
        }
    }
}
