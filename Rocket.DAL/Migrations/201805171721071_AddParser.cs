namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddParser : DbMigration
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
                "dbo.MusicGenres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Music",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        PosterImagePath = c.String(maxLength: 200),
                        Duration = c.Int(),
                        Artist = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Musician",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MusicTrack",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                        Duration = c.Time(precision: 7),
                        MusicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Music", t => t.MusicId, cascadeDelete: true)
                .Index(t => t.MusicId);
            
            CreateTable(
                "dbo.ResourceItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResourceId = c.Int(nullable: false),
                        ResourceInternalId = c.String(nullable: false, maxLength: 50),
                        ResourceItemLink = c.String(),
                        CreatedDateTime = c.DateTime(name: "CreatedDateTime", nullable: false, defaultValueSql: "GETDATE()"),
                        LastModified = c.DateTime(name: "LastModified", nullable: false, defaultValueSql: "GETDATE()"),
                    MusicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Music", t => t.MusicId, cascadeDelete: true)
                .ForeignKey("dbo.Resource", t => t.ResourceId, cascadeDelete: true)
                .Index(t => t.ResourceId)
                .Index(t => t.MusicId);
            
            CreateTable(
                "dbo.Resource",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ResourceLink = c.String(nullable: false, maxLength: 150),
                        ParseIsSwitchOn = c.Boolean(nullable: false),
                        ParsePeriodInMinutes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParserSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResourceId = c.Int(nullable: false),
                        BaseUrl = c.String(nullable: false, maxLength: 250),
                        Prefix = c.String(maxLength: 200),
                        StartPoint = c.Int(),
                        EndPoint = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resource", t => t.ResourceId, cascadeDelete: true)
                .Index(t => t.ResourceId);
            
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
            
            CreateTable(
                "dbo.MusicReleaseGenres",
                c => new
                    {
                        MusicId = c.Int(nullable: false),
                        MusicGenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MusicId, t.MusicGenreId })
                .ForeignKey("dbo.Music", t => t.MusicId, cascadeDelete: true)
                .ForeignKey("dbo.MusicGenres", t => t.MusicGenreId, cascadeDelete: true)
                .Index(t => t.MusicId)
                .Index(t => t.MusicGenreId);
            
            CreateTable(
                "dbo.MusicMusicians",
                c => new
                    {
                        MusicId = c.Int(nullable: false),
                        MusiciansId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MusicId, t.MusiciansId })
                .ForeignKey("dbo.Music", t => t.MusicId, cascadeDelete: true)
                .ForeignKey("dbo.Musician", t => t.MusiciansId, cascadeDelete: true)
                .Index(t => t.MusicId)
                .Index(t => t.MusiciansId);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResourceItem", "ResourceId", "dbo.Resource");
            DropForeignKey("dbo.ParserSettings", "ResourceId", "dbo.Resource");
            DropForeignKey("dbo.ResourceItem", "MusicId", "dbo.Music");
            DropForeignKey("dbo.MusicTrack", "MusicId", "dbo.Music");
            DropForeignKey("dbo.MusicMusicians", "MusiciansId", "dbo.Musician");
            DropForeignKey("dbo.MusicMusicians", "MusicId", "dbo.Music");
            DropForeignKey("dbo.MusicReleaseGenres", "MusicGenreId", "dbo.MusicGenres");
            DropForeignKey("dbo.MusicReleaseGenres", "MusicId", "dbo.Music");
            DropForeignKey("dbo.Season", "TvSeriesId", "dbo.TvSerias");
            DropForeignKey("dbo.Episode", "SeasonId", "dbo.Season");
            DropForeignKey("dbo.TvSeriasToPersons", "PersonId", "dbo.Person");
            DropForeignKey("dbo.TvSeriasToPersons", "TvSeriasId", "dbo.TvSerias");
            DropForeignKey("dbo.Person", "PersonTypeCode", "dbo.PersonType");
            DropForeignKey("dbo.TvSeriasToGenres", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.TvSeriasToGenres", "TvSeriasId", "dbo.TvSerias");
            DropForeignKey("dbo.Genre", "CategoryCode", "dbo.Category");
            DropIndex("dbo.MusicMusicians", new[] { "MusiciansId" });
            DropIndex("dbo.MusicMusicians", new[] { "MusicId" });
            DropIndex("dbo.MusicReleaseGenres", new[] { "MusicGenreId" });
            DropIndex("dbo.MusicReleaseGenres", new[] { "MusicId" });
            DropIndex("dbo.TvSeriasToPersons", new[] { "PersonId" });
            DropIndex("dbo.TvSeriasToPersons", new[] { "TvSeriasId" });
            DropIndex("dbo.TvSeriasToGenres", new[] { "GenreId" });
            DropIndex("dbo.TvSeriasToGenres", new[] { "TvSeriasId" });
            DropIndex("dbo.ParserSettings", new[] { "ResourceId" });
            DropIndex("dbo.ResourceItem", new[] { "MusicId" });
            DropIndex("dbo.ResourceItem", new[] { "ResourceId" });
            DropIndex("dbo.MusicTrack", new[] { "MusicId" });
            DropIndex("dbo.Episode", new[] { "SeasonId" });
            DropIndex("dbo.Season", new[] { "TvSeriesId" });
            DropIndex("dbo.Person", new[] { "PersonTypeCode" });
            DropIndex("dbo.Genre", new[] { "CategoryCode" });
            DropTable("dbo.MusicMusicians");
            DropTable("dbo.MusicReleaseGenres");
            DropTable("dbo.TvSeriasToPersons");
            DropTable("dbo.TvSeriasToGenres");
            DropTable("dbo.ParserSettings");
            DropTable("dbo.Resource");
            DropTable("dbo.ResourceItem");
            DropTable("dbo.MusicTrack");
            DropTable("dbo.Musician");
            DropTable("dbo.Music");
            DropTable("dbo.MusicGenres");
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
