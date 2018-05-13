namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTvSeriasTables : DbMigration
    {
        public override void Up()
        {
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
                        Duration = c.Time(precision: 7),
                        UrlForEpisodeSource = c.String(nullable: false),
                        DbSeasonId = c.Int(nullable: false),
                        SeasonEntity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbSeasons", t => t.DbSeasonId, cascadeDelete: true)
                .ForeignKey("dbo.Season", t => t.SeasonEntity_Id)
                .Index(t => t.DbSeasonId)
                .Index(t => t.SeasonEntity_Id);
            
            CreateTable(
                "dbo.DbSeasons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        PosterImagePath = c.String(),
                        Summary = c.String(),
                        DbTVSeriesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbTVSeries", t => t.DbTVSeriesId, cascadeDelete: true)
                .Index(t => t.DbTVSeriesId);
            
            CreateTable(
                "dbo.DbTVSeries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        PosterImagePath = c.String(),
                        Summary = c.String(),
                        DbPerson_Id = c.Int(),
                        DbPerson_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbPersons", t => t.DbPerson_Id)
                .ForeignKey("dbo.DbPersons", t => t.DbPerson_Id1)
                .Index(t => t.DbPerson_Id)
                .Index(t => t.DbPerson_Id1);
            
            CreateTable(
                "dbo.DbPersons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        DbFilm_Id = c.Int(),
                        DbFilm_Id1 = c.Int(),
                        DbTVSeries_Id = c.Int(),
                        DbTVSeries_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbFilms", t => t.DbFilm_Id)
                .ForeignKey("dbo.DbFilms", t => t.DbFilm_Id1)
                .ForeignKey("dbo.DbTVSeries", t => t.DbTVSeries_Id)
                .ForeignKey("dbo.DbTVSeries", t => t.DbTVSeries_Id1)
                .Index(t => t.DbFilm_Id)
                .Index(t => t.DbFilm_Id1)
                .Index(t => t.DbTVSeries_Id)
                .Index(t => t.DbTVSeries_Id1);
            
            CreateTable(
                "dbo.DbFilms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReleaseDate = c.DateTime(nullable: false),
                        Title = c.String(),
                        PosterImagePath = c.String(),
                        Duration = c.Time(precision: 7),
                        Summary = c.String(),
                        TrailerLink = c.String(),
                        DbPerson_Id = c.Int(),
                        DbPerson_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbPersons", t => t.DbPerson_Id)
                .ForeignKey("dbo.DbPersons", t => t.DbPerson_Id1)
                .Index(t => t.DbPerson_Id)
                .Index(t => t.DbPerson_Id1);
            
            CreateTable(
                "dbo.DbCountries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DbVideoGenres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DbEpisodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReleaseDate = c.DateTime(nullable: false),
                        Number = c.Int(nullable: false),
                        Title = c.String(),
                        Duration = c.Time(precision: 7),
                        Summary = c.String(),
                        DbSeasonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbSeasons", t => t.DbSeasonId, cascadeDelete: true)
                .Index(t => t.DbSeasonId);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        TvSeriasEntity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TvSerias", t => t.TvSeriasEntity_Id)
                .Index(t => t.TvSeriasEntity_Id);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullNameRu = c.String(nullable: false, maxLength: 250),
                        FullNameEn = c.String(nullable: false, maxLength: 250),
                        LostfilmPersonalPageUrl = c.String(nullable: false),
                        PhotoThumbnailUrl = c.String(),
                        TvSeriasEntity_Id = c.Int(),
                        TvSeriasEntity_Id1 = c.Int(),
                        TvSeriasEntity_Id2 = c.Int(),
                        TvSeriasEntity_Id3 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TvSerias", t => t.TvSeriasEntity_Id)
                .ForeignKey("dbo.TvSerias", t => t.TvSeriasEntity_Id1)
                .ForeignKey("dbo.TvSerias", t => t.TvSeriasEntity_Id2)
                .ForeignKey("dbo.TvSerias", t => t.TvSeriasEntity_Id3)
                .Index(t => t.TvSeriasEntity_Id)
                .Index(t => t.TvSeriasEntity_Id1)
                .Index(t => t.TvSeriasEntity_Id2)
                .Index(t => t.TvSeriasEntity_Id3);
            
            CreateTable(
                "dbo.PersonType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Season",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        PosterImageUrl = c.String(),
                        DbTvSeriesId = c.Int(nullable: false),
                        TvSeriasEntity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TvSerias", t => t.TvSeriasEntity_Id)
                .Index(t => t.TvSeriasEntity_Id);
            
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
                "dbo.DbCountryDbFilms",
                c => new
                    {
                        DbCountry_Id = c.Int(nullable: false),
                        DbFilm_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DbCountry_Id, t.DbFilm_Id })
                .ForeignKey("dbo.DbCountries", t => t.DbCountry_Id, cascadeDelete: true)
                .ForeignKey("dbo.DbFilms", t => t.DbFilm_Id, cascadeDelete: true)
                .Index(t => t.DbCountry_Id)
                .Index(t => t.DbFilm_Id);
            
            CreateTable(
                "dbo.DbCountryDbTVSeries",
                c => new
                    {
                        DbCountry_Id = c.Int(nullable: false),
                        DbTVSeries_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DbCountry_Id, t.DbTVSeries_Id })
                .ForeignKey("dbo.DbCountries", t => t.DbCountry_Id, cascadeDelete: true)
                .ForeignKey("dbo.DbTVSeries", t => t.DbTVSeries_Id, cascadeDelete: true)
                .Index(t => t.DbCountry_Id)
                .Index(t => t.DbTVSeries_Id);
            
            CreateTable(
                "dbo.DbVideoGenreDbFilms",
                c => new
                    {
                        DbVideoGenre_Id = c.Int(nullable: false),
                        DbFilm_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DbVideoGenre_Id, t.DbFilm_Id })
                .ForeignKey("dbo.DbVideoGenres", t => t.DbVideoGenre_Id, cascadeDelete: true)
                .ForeignKey("dbo.DbFilms", t => t.DbFilm_Id, cascadeDelete: true)
                .Index(t => t.DbVideoGenre_Id)
                .Index(t => t.DbFilm_Id);
            
            CreateTable(
                "dbo.DbVideoGenreDbTVSeries",
                c => new
                    {
                        DbVideoGenre_Id = c.Int(nullable: false),
                        DbTVSeries_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DbVideoGenre_Id, t.DbTVSeries_Id })
                .ForeignKey("dbo.DbVideoGenres", t => t.DbVideoGenre_Id, cascadeDelete: true)
                .ForeignKey("dbo.DbTVSeries", t => t.DbTVSeries_Id, cascadeDelete: true)
                .Index(t => t.DbVideoGenre_Id)
                .Index(t => t.DbTVSeries_Id);
            
            AlterColumn("dbo.Category", "Name", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Person", "TvSeriasEntity_Id3", "dbo.TvSerias");
            DropForeignKey("dbo.Season", "TvSeriasEntity_Id", "dbo.TvSerias");
            DropForeignKey("dbo.Person", "TvSeriasEntity_Id2", "dbo.TvSerias");
            DropForeignKey("dbo.Genre", "TvSeriasEntity_Id", "dbo.TvSerias");
            DropForeignKey("dbo.Person", "TvSeriasEntity_Id1", "dbo.TvSerias");
            DropForeignKey("dbo.Person", "TvSeriasEntity_Id", "dbo.TvSerias");
            DropForeignKey("dbo.Episode", "SeasonEntity_Id", "dbo.Season");
            DropForeignKey("dbo.Episode", "DbSeasonId", "dbo.DbSeasons");
            DropForeignKey("dbo.DbEpisodes", "DbSeasonId", "dbo.DbSeasons");
            DropForeignKey("dbo.DbPersons", "DbTVSeries_Id1", "dbo.DbTVSeries");
            DropForeignKey("dbo.DbSeasons", "DbTVSeriesId", "dbo.DbTVSeries");
            DropForeignKey("dbo.DbPersons", "DbTVSeries_Id", "dbo.DbTVSeries");
            DropForeignKey("dbo.DbTVSeries", "DbPerson_Id1", "dbo.DbPersons");
            DropForeignKey("dbo.DbTVSeries", "DbPerson_Id", "dbo.DbPersons");
            DropForeignKey("dbo.DbFilms", "DbPerson_Id1", "dbo.DbPersons");
            DropForeignKey("dbo.DbFilms", "DbPerson_Id", "dbo.DbPersons");
            DropForeignKey("dbo.DbVideoGenreDbTVSeries", "DbTVSeries_Id", "dbo.DbTVSeries");
            DropForeignKey("dbo.DbVideoGenreDbTVSeries", "DbVideoGenre_Id", "dbo.DbVideoGenres");
            DropForeignKey("dbo.DbVideoGenreDbFilms", "DbFilm_Id", "dbo.DbFilms");
            DropForeignKey("dbo.DbVideoGenreDbFilms", "DbVideoGenre_Id", "dbo.DbVideoGenres");
            DropForeignKey("dbo.DbPersons", "DbFilm_Id1", "dbo.DbFilms");
            DropForeignKey("dbo.DbCountryDbTVSeries", "DbTVSeries_Id", "dbo.DbTVSeries");
            DropForeignKey("dbo.DbCountryDbTVSeries", "DbCountry_Id", "dbo.DbCountries");
            DropForeignKey("dbo.DbCountryDbFilms", "DbFilm_Id", "dbo.DbFilms");
            DropForeignKey("dbo.DbCountryDbFilms", "DbCountry_Id", "dbo.DbCountries");
            DropForeignKey("dbo.DbPersons", "DbFilm_Id", "dbo.DbFilms");
            DropIndex("dbo.DbVideoGenreDbTVSeries", new[] { "DbTVSeries_Id" });
            DropIndex("dbo.DbVideoGenreDbTVSeries", new[] { "DbVideoGenre_Id" });
            DropIndex("dbo.DbVideoGenreDbFilms", new[] { "DbFilm_Id" });
            DropIndex("dbo.DbVideoGenreDbFilms", new[] { "DbVideoGenre_Id" });
            DropIndex("dbo.DbCountryDbTVSeries", new[] { "DbTVSeries_Id" });
            DropIndex("dbo.DbCountryDbTVSeries", new[] { "DbCountry_Id" });
            DropIndex("dbo.DbCountryDbFilms", new[] { "DbFilm_Id" });
            DropIndex("dbo.DbCountryDbFilms", new[] { "DbCountry_Id" });
            DropIndex("dbo.Season", new[] { "TvSeriasEntity_Id" });
            DropIndex("dbo.Person", new[] { "TvSeriasEntity_Id3" });
            DropIndex("dbo.Person", new[] { "TvSeriasEntity_Id2" });
            DropIndex("dbo.Person", new[] { "TvSeriasEntity_Id1" });
            DropIndex("dbo.Person", new[] { "TvSeriasEntity_Id" });
            DropIndex("dbo.Genre", new[] { "TvSeriasEntity_Id" });
            DropIndex("dbo.DbEpisodes", new[] { "DbSeasonId" });
            DropIndex("dbo.DbFilms", new[] { "DbPerson_Id1" });
            DropIndex("dbo.DbFilms", new[] { "DbPerson_Id" });
            DropIndex("dbo.DbPersons", new[] { "DbTVSeries_Id1" });
            DropIndex("dbo.DbPersons", new[] { "DbTVSeries_Id" });
            DropIndex("dbo.DbPersons", new[] { "DbFilm_Id1" });
            DropIndex("dbo.DbPersons", new[] { "DbFilm_Id" });
            DropIndex("dbo.DbTVSeries", new[] { "DbPerson_Id1" });
            DropIndex("dbo.DbTVSeries", new[] { "DbPerson_Id" });
            DropIndex("dbo.DbSeasons", new[] { "DbTVSeriesId" });
            DropIndex("dbo.Episode", new[] { "SeasonEntity_Id" });
            DropIndex("dbo.Episode", new[] { "DbSeasonId" });
            AlterColumn("dbo.Category", "Name", c => c.String(nullable: false));
            DropTable("dbo.DbVideoGenreDbTVSeries");
            DropTable("dbo.DbVideoGenreDbFilms");
            DropTable("dbo.DbCountryDbTVSeries");
            DropTable("dbo.DbCountryDbFilms");
            DropTable("dbo.TvSerias");
            DropTable("dbo.Season");
            DropTable("dbo.PersonType");
            DropTable("dbo.Person");
            DropTable("dbo.Genre");
            DropTable("dbo.DbEpisodes");
            DropTable("dbo.DbVideoGenres");
            DropTable("dbo.DbCountries");
            DropTable("dbo.DbFilms");
            DropTable("dbo.DbPersons");
            DropTable("dbo.DbTVSeries");
            DropTable("dbo.DbSeasons");
            DropTable("dbo.Episode");
        }
    }
}
