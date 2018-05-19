namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 35),
                        LastName = c.String(maxLength: 35),
                        Login = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 35),
                        AccountStatusId = c.Int(),
                        AccountLevelId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountLevels", t => t.AccountLevelId)
                .ForeignKey("dbo.AccountStatuses", t => t.AccountStatusId)
                .Index(t => t.AccountStatusId)
                .Index(t => t.AccountLevelId);
            
            CreateTable(
                "dbo.AccountStatuses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuthorisedUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AvatarPath = c.String(maxLength: 200),
                        DbUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.DbUserId)
                .Index(t => t.DbUserId);
            
            CreateTable(
                "dbo.DbEmails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DbAuthorisedUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuthorisedUsers", t => t.DbAuthorisedUserId, cascadeDelete: true)
                .Index(t => t.DbAuthorisedUserId);
            
            CreateTable(
                "dbo.DbGenres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DbCategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbCategories", t => t.DbCategoryId)
                .Index(t => t.DbCategoryId);
            
            CreateTable(
                "dbo.DbCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DbRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DbPermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        ValueName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ActivationNeeded = c.Boolean(),
                        SitizenshipId = c.Int(),
                        LanguageId = c.Int(),
                        DateOfBirth = c.DateTime(),
                        GenderId = c.Int(),
                        HowToCallId = c.Int(),
                        MailAddressId = c.Int(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genders", t => t.GenderId)
                .ForeignKey("dbo.HowToCalls", t => t.HowToCallId)
                .ForeignKey("dbo.Languages", t => t.LanguageId)
                .ForeignKey("dbo.Countries", t => t.SitizenshipId)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.SitizenshipId)
                .Index(t => t.LanguageId)
                .Index(t => t.GenderId)
                .Index(t => t.HowToCallId);
            
            CreateTable(
                "dbo.EmailAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HowToCalls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ZipCode = c.String(maxLength: 15),
                        CountryId = c.Int(),
                        City = c.String(maxLength: 25),
                        Building = c.String(maxLength: 25),
                        BuildingBlock = c.String(maxLength: 20),
                        Flat = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.UserDetails", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DbFilms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PosterImagePath = c.String(),
                        Duration = c.Time(precision: 7),
                        Summary = c.String(),
                        TrailerLink = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        Title = c.String(),
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
                        DbTVSeries_Id = c.Int(),
                        DbTVSeries_Id1 = c.Int(),
                        DbFilm_Id = c.Int(),
                        DbFilm_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbTVSeries", t => t.DbTVSeries_Id)
                .ForeignKey("dbo.DbTVSeries", t => t.DbTVSeries_Id1)
                .ForeignKey("dbo.DbFilms", t => t.DbFilm_Id)
                .ForeignKey("dbo.DbFilms", t => t.DbFilm_Id1)
                .Index(t => t.DbTVSeries_Id)
                .Index(t => t.DbTVSeries_Id1)
                .Index(t => t.DbFilm_Id)
                .Index(t => t.DbFilm_Id1);
            
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
                "dbo.DbEpisodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Duration = c.Time(precision: 7),
                        Summary = c.String(),
                        DbSeasonId = c.Int(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbSeasons", t => t.DbSeasonId, cascadeDelete: true)
                .Index(t => t.DbSeasonId);
            
            CreateTable(
                "dbo.DbVideoGenres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PhoneNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuthorisedUserGenres",
                c => new
                    {
                        AuthorisedUserId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AuthorisedUserId, t.GenreId })
                .ForeignKey("dbo.AuthorisedUsers", t => t.AuthorisedUserId, cascadeDelete: true)
                .ForeignKey("dbo.DbGenres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.AuthorisedUserId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.DbPermissionDbRoles",
                c => new
                    {
                        DbPermission_Id = c.Int(nullable: false),
                        DbRole_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DbPermission_Id, t.DbRole_Id })
                .ForeignKey("dbo.DbPermissions", t => t.DbPermission_Id, cascadeDelete: true)
                .ForeignKey("dbo.DbRoles", t => t.DbRole_Id, cascadeDelete: true)
                .Index(t => t.DbPermission_Id)
                .Index(t => t.DbRole_Id);
            
            CreateTable(
                "dbo.UsersRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.DbRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserDetailsEmailAddresses",
                c => new
                    {
                        UserDetailId = c.Int(nullable: false),
                        EmailAddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserDetailId, t.EmailAddressId })
                .ForeignKey("dbo.UserDetails", t => t.UserDetailId, cascadeDelete: true)
                .ForeignKey("dbo.EmailAddresses", t => t.EmailAddressId, cascadeDelete: true)
                .Index(t => t.UserDetailId)
                .Index(t => t.EmailAddressId);
            
            CreateTable(
                "dbo.DbTVSeriesDbCountries",
                c => new
                    {
                        DbTVSeries_Id = c.Int(nullable: false),
                        DbCountry_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DbTVSeries_Id, t.DbCountry_Id })
                .ForeignKey("dbo.DbTVSeries", t => t.DbTVSeries_Id, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.DbCountry_Id, cascadeDelete: true)
                .Index(t => t.DbTVSeries_Id)
                .Index(t => t.DbCountry_Id);
            
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
            
            CreateTable(
                "dbo.DbFilmDbCountries",
                c => new
                    {
                        DbFilm_Id = c.Int(nullable: false),
                        DbCountry_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DbFilm_Id, t.DbCountry_Id })
                .ForeignKey("dbo.DbFilms", t => t.DbFilm_Id, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.DbCountry_Id, cascadeDelete: true)
                .Index(t => t.DbFilm_Id)
                .Index(t => t.DbCountry_Id);
            
            CreateTable(
                "dbo.UserDetailsPhoneNumbers",
                c => new
                    {
                        UserDetailId = c.Int(nullable: false),
                        PhoneNumberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserDetailId, t.PhoneNumberId })
                .ForeignKey("dbo.UserDetails", t => t.UserDetailId, cascadeDelete: true)
                .ForeignKey("dbo.PhoneNumbers", t => t.PhoneNumberId, cascadeDelete: true)
                .Index(t => t.UserDetailId)
                .Index(t => t.PhoneNumberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDetails", "Id", "dbo.Users");
            DropForeignKey("dbo.UserDetails", "SitizenshipId", "dbo.Countries");
            DropForeignKey("dbo.UserDetailsPhoneNumbers", "PhoneNumberId", "dbo.PhoneNumbers");
            DropForeignKey("dbo.UserDetailsPhoneNumbers", "UserDetailId", "dbo.UserDetails");
            DropForeignKey("dbo.Addresses", "Id", "dbo.UserDetails");
            DropForeignKey("dbo.Addresses", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.DbPersons", "DbFilm_Id1", "dbo.DbFilms");
            DropForeignKey("dbo.DbFilmDbCountries", "DbCountry_Id", "dbo.Countries");
            DropForeignKey("dbo.DbFilmDbCountries", "DbFilm_Id", "dbo.DbFilms");
            DropForeignKey("dbo.DbPersons", "DbFilm_Id", "dbo.DbFilms");
            DropForeignKey("dbo.DbTVSeries", "DbPerson_Id1", "dbo.DbPersons");
            DropForeignKey("dbo.DbTVSeries", "DbPerson_Id", "dbo.DbPersons");
            DropForeignKey("dbo.DbVideoGenreDbTVSeries", "DbTVSeries_Id", "dbo.DbTVSeries");
            DropForeignKey("dbo.DbVideoGenreDbTVSeries", "DbVideoGenre_Id", "dbo.DbVideoGenres");
            DropForeignKey("dbo.DbVideoGenreDbFilms", "DbFilm_Id", "dbo.DbFilms");
            DropForeignKey("dbo.DbVideoGenreDbFilms", "DbVideoGenre_Id", "dbo.DbVideoGenres");
            DropForeignKey("dbo.DbPersons", "DbTVSeries_Id1", "dbo.DbTVSeries");
            DropForeignKey("dbo.DbEpisodes", "DbSeasonId", "dbo.DbSeasons");
            DropForeignKey("dbo.DbSeasons", "DbTVSeriesId", "dbo.DbTVSeries");
            DropForeignKey("dbo.DbTVSeriesDbCountries", "DbCountry_Id", "dbo.Countries");
            DropForeignKey("dbo.DbTVSeriesDbCountries", "DbTVSeries_Id", "dbo.DbTVSeries");
            DropForeignKey("dbo.DbPersons", "DbTVSeries_Id", "dbo.DbTVSeries");
            DropForeignKey("dbo.DbFilms", "DbPerson_Id1", "dbo.DbPersons");
            DropForeignKey("dbo.DbFilms", "DbPerson_Id", "dbo.DbPersons");
            DropForeignKey("dbo.UserDetails", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.UserDetails", "HowToCallId", "dbo.HowToCalls");
            DropForeignKey("dbo.UserDetails", "GenderId", "dbo.Genders");
            DropForeignKey("dbo.UserDetailsEmailAddresses", "EmailAddressId", "dbo.EmailAddresses");
            DropForeignKey("dbo.UserDetailsEmailAddresses", "UserDetailId", "dbo.UserDetails");
            DropForeignKey("dbo.UsersRoles", "RoleId", "dbo.DbRoles");
            DropForeignKey("dbo.UsersRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.DbPermissionDbRoles", "DbRole_Id", "dbo.DbRoles");
            DropForeignKey("dbo.DbPermissionDbRoles", "DbPermission_Id", "dbo.DbPermissions");
            DropForeignKey("dbo.AuthorisedUserGenres", "GenreId", "dbo.DbGenres");
            DropForeignKey("dbo.AuthorisedUserGenres", "AuthorisedUserId", "dbo.AuthorisedUsers");
            DropForeignKey("dbo.DbGenres", "DbCategoryId", "dbo.DbCategories");
            DropForeignKey("dbo.DbEmails", "DbAuthorisedUserId", "dbo.AuthorisedUsers");
            DropForeignKey("dbo.AuthorisedUsers", "DbUserId", "dbo.Users");
            DropForeignKey("dbo.Users", "AccountStatusId", "dbo.AccountStatuses");
            DropForeignKey("dbo.Users", "AccountLevelId", "dbo.AccountLevels");
            DropIndex("dbo.UserDetailsPhoneNumbers", new[] { "PhoneNumberId" });
            DropIndex("dbo.UserDetailsPhoneNumbers", new[] { "UserDetailId" });
            DropIndex("dbo.DbFilmDbCountries", new[] { "DbCountry_Id" });
            DropIndex("dbo.DbFilmDbCountries", new[] { "DbFilm_Id" });
            DropIndex("dbo.DbVideoGenreDbTVSeries", new[] { "DbTVSeries_Id" });
            DropIndex("dbo.DbVideoGenreDbTVSeries", new[] { "DbVideoGenre_Id" });
            DropIndex("dbo.DbVideoGenreDbFilms", new[] { "DbFilm_Id" });
            DropIndex("dbo.DbVideoGenreDbFilms", new[] { "DbVideoGenre_Id" });
            DropIndex("dbo.DbTVSeriesDbCountries", new[] { "DbCountry_Id" });
            DropIndex("dbo.DbTVSeriesDbCountries", new[] { "DbTVSeries_Id" });
            DropIndex("dbo.UserDetailsEmailAddresses", new[] { "EmailAddressId" });
            DropIndex("dbo.UserDetailsEmailAddresses", new[] { "UserDetailId" });
            DropIndex("dbo.UsersRoles", new[] { "RoleId" });
            DropIndex("dbo.UsersRoles", new[] { "UserId" });
            DropIndex("dbo.DbPermissionDbRoles", new[] { "DbRole_Id" });
            DropIndex("dbo.DbPermissionDbRoles", new[] { "DbPermission_Id" });
            DropIndex("dbo.AuthorisedUserGenres", new[] { "GenreId" });
            DropIndex("dbo.AuthorisedUserGenres", new[] { "AuthorisedUserId" });
            DropIndex("dbo.DbEpisodes", new[] { "DbSeasonId" });
            DropIndex("dbo.DbSeasons", new[] { "DbTVSeriesId" });
            DropIndex("dbo.DbTVSeries", new[] { "DbPerson_Id1" });
            DropIndex("dbo.DbTVSeries", new[] { "DbPerson_Id" });
            DropIndex("dbo.DbPersons", new[] { "DbFilm_Id1" });
            DropIndex("dbo.DbPersons", new[] { "DbFilm_Id" });
            DropIndex("dbo.DbPersons", new[] { "DbTVSeries_Id1" });
            DropIndex("dbo.DbPersons", new[] { "DbTVSeries_Id" });
            DropIndex("dbo.DbFilms", new[] { "DbPerson_Id1" });
            DropIndex("dbo.DbFilms", new[] { "DbPerson_Id" });
            DropIndex("dbo.Addresses", new[] { "CountryId" });
            DropIndex("dbo.Addresses", new[] { "Id" });
            DropIndex("dbo.UserDetails", new[] { "HowToCallId" });
            DropIndex("dbo.UserDetails", new[] { "GenderId" });
            DropIndex("dbo.UserDetails", new[] { "LanguageId" });
            DropIndex("dbo.UserDetails", new[] { "SitizenshipId" });
            DropIndex("dbo.UserDetails", new[] { "Id" });
            DropIndex("dbo.DbGenres", new[] { "DbCategoryId" });
            DropIndex("dbo.DbEmails", new[] { "DbAuthorisedUserId" });
            DropIndex("dbo.AuthorisedUsers", new[] { "DbUserId" });
            DropIndex("dbo.Users", new[] { "AccountLevelId" });
            DropIndex("dbo.Users", new[] { "AccountStatusId" });
            DropTable("dbo.UserDetailsPhoneNumbers");
            DropTable("dbo.DbFilmDbCountries");
            DropTable("dbo.DbVideoGenreDbTVSeries");
            DropTable("dbo.DbVideoGenreDbFilms");
            DropTable("dbo.DbTVSeriesDbCountries");
            DropTable("dbo.UserDetailsEmailAddresses");
            DropTable("dbo.UsersRoles");
            DropTable("dbo.DbPermissionDbRoles");
            DropTable("dbo.AuthorisedUserGenres");
            DropTable("dbo.PhoneNumbers");
            DropTable("dbo.DbVideoGenres");
            DropTable("dbo.DbEpisodes");
            DropTable("dbo.DbSeasons");
            DropTable("dbo.DbTVSeries");
            DropTable("dbo.DbPersons");
            DropTable("dbo.DbFilms");
            DropTable("dbo.Countries");
            DropTable("dbo.Addresses");
            DropTable("dbo.Languages");
            DropTable("dbo.HowToCalls");
            DropTable("dbo.Genders");
            DropTable("dbo.EmailAddresses");
            DropTable("dbo.UserDetails");
            DropTable("dbo.DbPermissions");
            DropTable("dbo.DbRoles");
            DropTable("dbo.DbCategories");
            DropTable("dbo.DbGenres");
            DropTable("dbo.DbEmails");
            DropTable("dbo.AuthorisedUsers");
            DropTable("dbo.AccountStatuses");
            DropTable("dbo.Users");
            DropTable("dbo.AccountLevels");
        }
    }
}
