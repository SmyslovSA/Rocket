namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Films",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReleaseDate = c.DateTime(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        PosterImagePath = c.String(maxLength: 200),
                        Duration = c.Time(precision: 7),
                        Summary = c.String(),
                        TrailerLink = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Persons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TVSerials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        PosterImagePath = c.String(maxLength: 200),
                        Summary = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Seasons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        PosterImagePath = c.String(maxLength: 200),
                        Summary = c.String(),
                        TVSeriesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TVSerials", t => t.TVSeriesId, cascadeDelete: true)
                .Index(t => t.TVSeriesId);
            
            CreateTable(
                "dbo.Episodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReleaseDate = c.DateTime(nullable: false),
                        Number = c.Int(nullable: false),
                        Title = c.String(maxLength: 50),
                        Duration = c.Time(precision: 7),
                        Summary = c.String(),
                        SeasonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Seasons", t => t.SeasonId, cascadeDelete: true)
                .Index(t => t.SeasonId);
            
            CreateTable(
                "dbo.VideoGenres",
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
                        Title = c.String(nullable: false, maxLength: 50),
                        ReleaseDate = c.DateTime(nullable: false),
                        PosterImagePath = c.String(maxLength: 200),
                        Duration = c.Time(precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MusicGenres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
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
                        Title = c.String(nullable: false, maxLength: 50),
                        Duration = c.Time(precision: 7),
                        MusicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Music", t => t.MusicId, cascadeDelete: true)
                .Index(t => t.MusicId);
            
            CreateTable(
                "dbo.t_user_permission",
                c => new
                    {
                        permission_id = c.Int(nullable: false, identity: true),
                        description = c.String(maxLength: 250),
                        valueName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.permission_id);
            
            CreateTable(
                "dbo.t_user_role",
                c => new
                    {
                        role_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        is_active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.role_id);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 30),
                        LastName = c.String(maxLength: 50),
                        Login = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 30),
                        AvatarPath = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 80),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TVSerialsActors",
                c => new
                    {
                        TVSeriesId = c.Int(nullable: false),
                        ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TVSeriesId, t.ActorId })
                .ForeignKey("dbo.TVSerials", t => t.TVSeriesId, cascadeDelete: true)
                .ForeignKey("dbo.Persons", t => t.ActorId, cascadeDelete: true)
                .Index(t => t.TVSeriesId)
                .Index(t => t.ActorId);
            
            CreateTable(
                "dbo.TVSerialsCountries",
                c => new
                    {
                        TVSeriesId = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TVSeriesId, t.CountryId })
                .ForeignKey("dbo.TVSerials", t => t.TVSeriesId, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.TVSeriesId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.TVSerialsDirectors",
                c => new
                    {
                        TVSeriesId = c.Int(nullable: false),
                        DirectorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TVSeriesId, t.DirectorId })
                .ForeignKey("dbo.TVSerials", t => t.TVSeriesId, cascadeDelete: true)
                .ForeignKey("dbo.Persons", t => t.DirectorId, cascadeDelete: true)
                .Index(t => t.TVSeriesId)
                .Index(t => t.DirectorId);
            
            CreateTable(
                "dbo.TVSerialsVideoGenres",
                c => new
                    {
                        TVSeriesId = c.Int(nullable: false),
                        VideoGenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TVSeriesId, t.VideoGenreId })
                .ForeignKey("dbo.TVSerials", t => t.TVSeriesId, cascadeDelete: true)
                .ForeignKey("dbo.VideoGenres", t => t.VideoGenreId, cascadeDelete: true)
                .Index(t => t.TVSeriesId)
                .Index(t => t.VideoGenreId);
            
            CreateTable(
                "dbo.FilmsActors",
                c => new
                    {
                        FilmId = c.Int(nullable: false),
                        ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FilmId, t.ActorId })
                .ForeignKey("dbo.Films", t => t.FilmId, cascadeDelete: true)
                .ForeignKey("dbo.Persons", t => t.ActorId, cascadeDelete: true)
                .Index(t => t.FilmId)
                .Index(t => t.ActorId);
            
            CreateTable(
                "dbo.FilmsCountries",
                c => new
                    {
                        FilmId = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FilmId, t.CountryId })
                .ForeignKey("dbo.Films", t => t.FilmId, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.FilmId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.FilmsDirectors",
                c => new
                    {
                        FilmId = c.Int(nullable: false),
                        DirectorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FilmId, t.DirectorId })
                .ForeignKey("dbo.Films", t => t.FilmId, cascadeDelete: true)
                .ForeignKey("dbo.Persons", t => t.DirectorId, cascadeDelete: true)
                .Index(t => t.FilmId)
                .Index(t => t.DirectorId);
            
            CreateTable(
                "dbo.FilmsVideoGenres",
                c => new
                    {
                        FilmId = c.Int(nullable: false),
                        VideoGenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FilmId, t.VideoGenreId })
                .ForeignKey("dbo.Films", t => t.FilmId, cascadeDelete: true)
                .ForeignKey("dbo.VideoGenres", t => t.VideoGenreId, cascadeDelete: true)
                .Index(t => t.FilmId)
                .Index(t => t.VideoGenreId);
            
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
            
            CreateTable(
                "dbo.DbRoleDbPermissions",
                c => new
                    {
                        DbRole_Id = c.Int(nullable: false),
                        DbPermission_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DbRole_Id, t.DbPermission_Id })
                .ForeignKey("dbo.t_user_role", t => t.DbRole_Id, cascadeDelete: true)
                .ForeignKey("dbo.t_user_permission", t => t.DbPermission_Id, cascadeDelete: true)
                .Index(t => t.DbRole_Id)
                .Index(t => t.DbPermission_Id);
            
            CreateTable(
                "dbo.UserGenres",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.GenreId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GenreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Genres", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.UserGenres", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.UserGenres", "UserId", "dbo.Users");
            DropForeignKey("dbo.Emails", "UserId", "dbo.Users");
            DropForeignKey("dbo.DbRoleDbPermissions", "DbPermission_Id", "dbo.t_user_permission");
            DropForeignKey("dbo.DbRoleDbPermissions", "DbRole_Id", "dbo.t_user_role");
            DropForeignKey("dbo.MusicTrack", "MusicId", "dbo.Music");
            DropForeignKey("dbo.MusicMusicians", "MusiciansId", "dbo.Musician");
            DropForeignKey("dbo.MusicMusicians", "MusicId", "dbo.Music");
            DropForeignKey("dbo.MusicReleaseGenres", "MusicGenreId", "dbo.MusicGenres");
            DropForeignKey("dbo.MusicReleaseGenres", "MusicId", "dbo.Music");
            DropForeignKey("dbo.FilmsVideoGenres", "VideoGenreId", "dbo.VideoGenres");
            DropForeignKey("dbo.FilmsVideoGenres", "FilmId", "dbo.Films");
            DropForeignKey("dbo.FilmsDirectors", "DirectorId", "dbo.Persons");
            DropForeignKey("dbo.FilmsDirectors", "FilmId", "dbo.Films");
            DropForeignKey("dbo.FilmsCountries", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.FilmsCountries", "FilmId", "dbo.Films");
            DropForeignKey("dbo.FilmsActors", "ActorId", "dbo.Persons");
            DropForeignKey("dbo.FilmsActors", "FilmId", "dbo.Films");
            DropForeignKey("dbo.TVSerialsVideoGenres", "VideoGenreId", "dbo.VideoGenres");
            DropForeignKey("dbo.TVSerialsVideoGenres", "TVSeriesId", "dbo.TVSerials");
            DropForeignKey("dbo.TVSerialsDirectors", "DirectorId", "dbo.Persons");
            DropForeignKey("dbo.TVSerialsDirectors", "TVSeriesId", "dbo.TVSerials");
            DropForeignKey("dbo.Seasons", "TVSeriesId", "dbo.TVSerials");
            DropForeignKey("dbo.Episodes", "SeasonId", "dbo.Seasons");
            DropForeignKey("dbo.TVSerialsCountries", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.TVSerialsCountries", "TVSeriesId", "dbo.TVSerials");
            DropForeignKey("dbo.TVSerialsActors", "ActorId", "dbo.Persons");
            DropForeignKey("dbo.TVSerialsActors", "TVSeriesId", "dbo.TVSerials");
            DropIndex("dbo.UserGenres", new[] { "GenreId" });
            DropIndex("dbo.UserGenres", new[] { "UserId" });
            DropIndex("dbo.DbRoleDbPermissions", new[] { "DbPermission_Id" });
            DropIndex("dbo.DbRoleDbPermissions", new[] { "DbRole_Id" });
            DropIndex("dbo.MusicMusicians", new[] { "MusiciansId" });
            DropIndex("dbo.MusicMusicians", new[] { "MusicId" });
            DropIndex("dbo.MusicReleaseGenres", new[] { "MusicGenreId" });
            DropIndex("dbo.MusicReleaseGenres", new[] { "MusicId" });
            DropIndex("dbo.FilmsVideoGenres", new[] { "VideoGenreId" });
            DropIndex("dbo.FilmsVideoGenres", new[] { "FilmId" });
            DropIndex("dbo.FilmsDirectors", new[] { "DirectorId" });
            DropIndex("dbo.FilmsDirectors", new[] { "FilmId" });
            DropIndex("dbo.FilmsCountries", new[] { "CountryId" });
            DropIndex("dbo.FilmsCountries", new[] { "FilmId" });
            DropIndex("dbo.FilmsActors", new[] { "ActorId" });
            DropIndex("dbo.FilmsActors", new[] { "FilmId" });
            DropIndex("dbo.TVSerialsVideoGenres", new[] { "VideoGenreId" });
            DropIndex("dbo.TVSerialsVideoGenres", new[] { "TVSeriesId" });
            DropIndex("dbo.TVSerialsDirectors", new[] { "DirectorId" });
            DropIndex("dbo.TVSerialsDirectors", new[] { "TVSeriesId" });
            DropIndex("dbo.TVSerialsCountries", new[] { "CountryId" });
            DropIndex("dbo.TVSerialsCountries", new[] { "TVSeriesId" });
            DropIndex("dbo.TVSerialsActors", new[] { "ActorId" });
            DropIndex("dbo.TVSerialsActors", new[] { "TVSeriesId" });
            DropIndex("dbo.Emails", new[] { "UserId" });
            DropIndex("dbo.Genres", new[] { "CategoryId" });
            DropIndex("dbo.MusicTrack", new[] { "MusicId" });
            DropIndex("dbo.Episodes", new[] { "SeasonId" });
            DropIndex("dbo.Seasons", new[] { "TVSeriesId" });
            DropTable("dbo.UserGenres");
            DropTable("dbo.DbRoleDbPermissions");
            DropTable("dbo.MusicMusicians");
            DropTable("dbo.MusicReleaseGenres");
            DropTable("dbo.FilmsVideoGenres");
            DropTable("dbo.FilmsDirectors");
            DropTable("dbo.FilmsCountries");
            DropTable("dbo.FilmsActors");
            DropTable("dbo.TVSerialsVideoGenres");
            DropTable("dbo.TVSerialsDirectors");
            DropTable("dbo.TVSerialsCountries");
            DropTable("dbo.TVSerialsActors");
            DropTable("dbo.Emails");
            DropTable("dbo.Users");
            DropTable("dbo.Genres");
            DropTable("dbo.Category");
            DropTable("dbo.t_user_role");
            DropTable("dbo.t_user_permission");
            DropTable("dbo.MusicTrack");
            DropTable("dbo.Musician");
            DropTable("dbo.MusicGenres");
            DropTable("dbo.Music");
            DropTable("dbo.VideoGenres");
            DropTable("dbo.Episodes");
            DropTable("dbo.Seasons");
            DropTable("dbo.Countries");
            DropTable("dbo.TVSerials");
            DropTable("dbo.Persons");
            DropTable("dbo.Films");
        }
    }
}
