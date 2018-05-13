namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTvSeriasTables2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DbPersons", "DbFilm_Id", "dbo.DbFilms");
            DropForeignKey("dbo.DbCountryDbFilms", "DbCountry_Id", "dbo.DbCountries");
            DropForeignKey("dbo.DbCountryDbFilms", "DbFilm_Id", "dbo.DbFilms");
            DropForeignKey("dbo.DbCountryDbTVSeries", "DbCountry_Id", "dbo.DbCountries");
            DropForeignKey("dbo.DbCountryDbTVSeries", "DbTVSeries_Id", "dbo.DbTVSeries");
            DropForeignKey("dbo.DbPersons", "DbFilm_Id1", "dbo.DbFilms");
            DropForeignKey("dbo.DbVideoGenreDbFilms", "DbVideoGenre_Id", "dbo.DbVideoGenres");
            DropForeignKey("dbo.DbVideoGenreDbFilms", "DbFilm_Id", "dbo.DbFilms");
            DropForeignKey("dbo.DbVideoGenreDbTVSeries", "DbVideoGenre_Id", "dbo.DbVideoGenres");
            DropForeignKey("dbo.DbVideoGenreDbTVSeries", "DbTVSeries_Id", "dbo.DbTVSeries");
            DropForeignKey("dbo.DbFilms", "DbPerson_Id", "dbo.DbPersons");
            DropForeignKey("dbo.DbFilms", "DbPerson_Id1", "dbo.DbPersons");
            DropForeignKey("dbo.DbTVSeries", "DbPerson_Id", "dbo.DbPersons");
            DropForeignKey("dbo.DbTVSeries", "DbPerson_Id1", "dbo.DbPersons");
            DropForeignKey("dbo.DbPersons", "DbTVSeries_Id", "dbo.DbTVSeries");
            DropForeignKey("dbo.DbSeasons", "DbTVSeriesId", "dbo.DbTVSeries");
            DropForeignKey("dbo.DbPersons", "DbTVSeries_Id1", "dbo.DbTVSeries");
            DropForeignKey("dbo.DbEpisodes", "DbSeasonId", "dbo.DbSeasons");
            DropForeignKey("dbo.Episode", "DbSeasonId", "dbo.DbSeasons");
            DropForeignKey("dbo.Episode", "SeasonEntity_Id", "dbo.Season");
            DropIndex("dbo.Episode", new[] { "DbSeasonId" });
            DropIndex("dbo.Episode", new[] { "SeasonEntity_Id" });
            DropIndex("dbo.DbSeasons", new[] { "DbTVSeriesId" });
            DropIndex("dbo.DbTVSeries", new[] { "DbPerson_Id" });
            DropIndex("dbo.DbTVSeries", new[] { "DbPerson_Id1" });
            DropIndex("dbo.DbPersons", new[] { "DbFilm_Id" });
            DropIndex("dbo.DbPersons", new[] { "DbFilm_Id1" });
            DropIndex("dbo.DbPersons", new[] { "DbTVSeries_Id" });
            DropIndex("dbo.DbPersons", new[] { "DbTVSeries_Id1" });
            DropIndex("dbo.DbFilms", new[] { "DbPerson_Id" });
            DropIndex("dbo.DbFilms", new[] { "DbPerson_Id1" });
            DropIndex("dbo.DbEpisodes", new[] { "DbSeasonId" });
            DropIndex("dbo.DbCountryDbFilms", new[] { "DbCountry_Id" });
            DropIndex("dbo.DbCountryDbFilms", new[] { "DbFilm_Id" });
            DropIndex("dbo.DbCountryDbTVSeries", new[] { "DbCountry_Id" });
            DropIndex("dbo.DbCountryDbTVSeries", new[] { "DbTVSeries_Id" });
            DropIndex("dbo.DbVideoGenreDbFilms", new[] { "DbVideoGenre_Id" });
            DropIndex("dbo.DbVideoGenreDbFilms", new[] { "DbFilm_Id" });
            DropIndex("dbo.DbVideoGenreDbTVSeries", new[] { "DbVideoGenre_Id" });
            DropIndex("dbo.DbVideoGenreDbTVSeries", new[] { "DbTVSeries_Id" });
            RenameColumn(table: "dbo.Episode", name: "SeasonEntity_Id", newName: "SeasonId");
            AddColumn("dbo.Season", "TvSeriesId", c => c.Int(nullable: false));
            AlterColumn("dbo.Episode", "SeasonId", c => c.Int(nullable: false));
            CreateIndex("dbo.Episode", "SeasonId");
            AddForeignKey("dbo.Episode", "SeasonId", "dbo.Season", "Id", cascadeDelete: true);
            DropColumn("dbo.Episode", "DbSeasonId");
            DropColumn("dbo.Season", "DbTvSeriesId");
            DropTable("dbo.DbSeasons");
            DropTable("dbo.DbTVSeries");
            DropTable("dbo.DbPersons");
            DropTable("dbo.DbFilms");
            DropTable("dbo.DbCountries");
            DropTable("dbo.DbVideoGenres");
            DropTable("dbo.DbEpisodes");
            DropTable("dbo.DbCountryDbFilms");
            DropTable("dbo.DbCountryDbTVSeries");
            DropTable("dbo.DbVideoGenreDbFilms");
            DropTable("dbo.DbVideoGenreDbTVSeries");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DbVideoGenreDbTVSeries",
                c => new
                    {
                        DbVideoGenre_Id = c.Int(nullable: false),
                        DbTVSeries_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DbVideoGenre_Id, t.DbTVSeries_Id });
            
            CreateTable(
                "dbo.DbVideoGenreDbFilms",
                c => new
                    {
                        DbVideoGenre_Id = c.Int(nullable: false),
                        DbFilm_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DbVideoGenre_Id, t.DbFilm_Id });
            
            CreateTable(
                "dbo.DbCountryDbTVSeries",
                c => new
                    {
                        DbCountry_Id = c.Int(nullable: false),
                        DbTVSeries_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DbCountry_Id, t.DbTVSeries_Id });
            
            CreateTable(
                "dbo.DbCountryDbFilms",
                c => new
                    {
                        DbCountry_Id = c.Int(nullable: false),
                        DbFilm_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DbCountry_Id, t.DbFilm_Id });
            
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
                "dbo.DbCountries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Season", "DbTvSeriesId", c => c.Int(nullable: false));
            AddColumn("dbo.Episode", "DbSeasonId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Episode", "SeasonId", "dbo.Season");
            DropIndex("dbo.Episode", new[] { "SeasonId" });
            AlterColumn("dbo.Episode", "SeasonId", c => c.Int());
            DropColumn("dbo.Season", "TvSeriesId");
            RenameColumn(table: "dbo.Episode", name: "SeasonId", newName: "SeasonEntity_Id");
            CreateIndex("dbo.DbVideoGenreDbTVSeries", "DbTVSeries_Id");
            CreateIndex("dbo.DbVideoGenreDbTVSeries", "DbVideoGenre_Id");
            CreateIndex("dbo.DbVideoGenreDbFilms", "DbFilm_Id");
            CreateIndex("dbo.DbVideoGenreDbFilms", "DbVideoGenre_Id");
            CreateIndex("dbo.DbCountryDbTVSeries", "DbTVSeries_Id");
            CreateIndex("dbo.DbCountryDbTVSeries", "DbCountry_Id");
            CreateIndex("dbo.DbCountryDbFilms", "DbFilm_Id");
            CreateIndex("dbo.DbCountryDbFilms", "DbCountry_Id");
            CreateIndex("dbo.DbEpisodes", "DbSeasonId");
            CreateIndex("dbo.DbFilms", "DbPerson_Id1");
            CreateIndex("dbo.DbFilms", "DbPerson_Id");
            CreateIndex("dbo.DbPersons", "DbTVSeries_Id1");
            CreateIndex("dbo.DbPersons", "DbTVSeries_Id");
            CreateIndex("dbo.DbPersons", "DbFilm_Id1");
            CreateIndex("dbo.DbPersons", "DbFilm_Id");
            CreateIndex("dbo.DbTVSeries", "DbPerson_Id1");
            CreateIndex("dbo.DbTVSeries", "DbPerson_Id");
            CreateIndex("dbo.DbSeasons", "DbTVSeriesId");
            CreateIndex("dbo.Episode", "SeasonEntity_Id");
            CreateIndex("dbo.Episode", "DbSeasonId");
            AddForeignKey("dbo.Episode", "SeasonEntity_Id", "dbo.Season", "Id");
            AddForeignKey("dbo.Episode", "DbSeasonId", "dbo.DbSeasons", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DbEpisodes", "DbSeasonId", "dbo.DbSeasons", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DbPersons", "DbTVSeries_Id1", "dbo.DbTVSeries", "Id");
            AddForeignKey("dbo.DbSeasons", "DbTVSeriesId", "dbo.DbTVSeries", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DbPersons", "DbTVSeries_Id", "dbo.DbTVSeries", "Id");
            AddForeignKey("dbo.DbTVSeries", "DbPerson_Id1", "dbo.DbPersons", "Id");
            AddForeignKey("dbo.DbTVSeries", "DbPerson_Id", "dbo.DbPersons", "Id");
            AddForeignKey("dbo.DbFilms", "DbPerson_Id1", "dbo.DbPersons", "Id");
            AddForeignKey("dbo.DbFilms", "DbPerson_Id", "dbo.DbPersons", "Id");
            AddForeignKey("dbo.DbVideoGenreDbTVSeries", "DbTVSeries_Id", "dbo.DbTVSeries", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DbVideoGenreDbTVSeries", "DbVideoGenre_Id", "dbo.DbVideoGenres", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DbVideoGenreDbFilms", "DbFilm_Id", "dbo.DbFilms", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DbVideoGenreDbFilms", "DbVideoGenre_Id", "dbo.DbVideoGenres", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DbPersons", "DbFilm_Id1", "dbo.DbFilms", "Id");
            AddForeignKey("dbo.DbCountryDbTVSeries", "DbTVSeries_Id", "dbo.DbTVSeries", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DbCountryDbTVSeries", "DbCountry_Id", "dbo.DbCountries", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DbCountryDbFilms", "DbFilm_Id", "dbo.DbFilms", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DbCountryDbFilms", "DbCountry_Id", "dbo.DbCountries", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DbPersons", "DbFilm_Id", "dbo.DbFilms", "Id");
        }
    }
}
