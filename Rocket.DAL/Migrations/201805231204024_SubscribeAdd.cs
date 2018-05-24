namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubscribeAdd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TvSeriasToGenres", "TvSeriasId", "dbo.TvSerias");
            DropForeignKey("dbo.TvSeriasToGenres", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.TvSeriasToPersons", "TvSeriasId", "dbo.TvSerias");
            DropForeignKey("dbo.TvSeriasToPersons", "PersonId", "dbo.Person");
            DropForeignKey("dbo.Season", "TvSeriesId", "dbo.TvSerias");
            DropForeignKey("dbo.MusicReleaseGenres", "MusicId", "dbo.Music");
            DropForeignKey("dbo.MusicReleaseGenres", "MusicGenreId", "dbo.MusicGenres");
            DropForeignKey("dbo.MusicMusicians", "MusicId", "dbo.Music");
            DropForeignKey("dbo.MusicMusicians", "MusiciansId", "dbo.Musician");
            DropForeignKey("dbo.MusicTrack", "MusicId", "dbo.Music");
            DropForeignKey("dbo.ResourceItem", "MusicId", "dbo.Music");
            DropForeignKey("dbo.AuthorisedUserGenres", "GenreId", "dbo.Genre");
            DropIndex("dbo.AuthorisedUserGenres", new[] { "GenreId" });
            DropIndex("dbo.TvSeriasToGenres", new[] { "GenreId" });
            DropPrimaryKey("dbo.Genre");
            DropPrimaryKey("dbo.TvSerias");
            DropPrimaryKey("dbo.Person");
            DropPrimaryKey("dbo.MusicGenres");
            DropPrimaryKey("dbo.Music");
            DropPrimaryKey("dbo.Musician");
            DropPrimaryKey("dbo.AuthorisedUserGenres");
            DropPrimaryKey("dbo.TvSeriasToGenres");
            CreateTable(
                "dbo.Subscribable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubscriptionsToUsers",
                c => new
                    {
                        SubscriptionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SubscriptionId, t.UserId })
                .ForeignKey("dbo.Subscribable", t => t.SubscriptionId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.SubscriptionId)
                .Index(t => t.UserId);
            
            AlterColumn("dbo.Genre", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.TvSerias", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Person", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.MusicGenres", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Music", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Musician", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AuthorisedUserGenres", "GenreId", c => c.Int(nullable: false));
            AlterColumn("dbo.TvSeriasToGenres", "GenreId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Genre", "Id");
            AddPrimaryKey("dbo.TvSerias", "Id");
            AddPrimaryKey("dbo.Person", "Id");
            AddPrimaryKey("dbo.MusicGenres", "Id");
            AddPrimaryKey("dbo.Music", "Id");
            AddPrimaryKey("dbo.Musician", "Id");
            AddPrimaryKey("dbo.AuthorisedUserGenres", new[] { "AuthorisedUserId", "GenreId" });
            AddPrimaryKey("dbo.TvSeriasToGenres", new[] { "TvSeriasId", "GenreId" });
            CreateIndex("dbo.TvSeriasToGenres", "GenreId");
            CreateIndex("dbo.AuthorisedUserGenres", "GenreId");
            CreateIndex("dbo.MusicGenres", "Id");
            CreateIndex("dbo.Music", "Id");
            CreateIndex("dbo.Genre", "Id");
            CreateIndex("dbo.Person", "Id");
            CreateIndex("dbo.TvSerias", "Id");
            CreateIndex("dbo.Musician", "Id");
            AddForeignKey("dbo.MusicGenres", "Id", "dbo.Subscribable", "Id");
            AddForeignKey("dbo.Music", "Id", "dbo.Subscribable", "Id");
            AddForeignKey("dbo.Genre", "Id", "dbo.Subscribable", "Id");
            AddForeignKey("dbo.Person", "Id", "dbo.Subscribable", "Id");
            AddForeignKey("dbo.TvSerias", "Id", "dbo.Subscribable", "Id");
            AddForeignKey("dbo.Musician", "Id", "dbo.Subscribable", "Id");
            AddForeignKey("dbo.TvSeriasToGenres", "TvSeriasId", "dbo.TvSerias", "Id");
            AddForeignKey("dbo.TvSeriasToGenres", "GenreId", "dbo.Genre", "Id");
            AddForeignKey("dbo.TvSeriasToPersons", "TvSeriasId", "dbo.TvSerias", "Id");
            AddForeignKey("dbo.TvSeriasToPersons", "PersonId", "dbo.Person", "Id");
            AddForeignKey("dbo.Season", "TvSeriesId", "dbo.TvSerias", "Id");
            AddForeignKey("dbo.MusicReleaseGenres", "MusicId", "dbo.Music", "Id");
            AddForeignKey("dbo.MusicReleaseGenres", "MusicGenreId", "dbo.MusicGenres", "Id");
            AddForeignKey("dbo.MusicMusicians", "MusicId", "dbo.Music", "Id");
            AddForeignKey("dbo.MusicMusicians", "MusiciansId", "dbo.Musician", "Id");
            AddForeignKey("dbo.MusicTrack", "MusicId", "dbo.Music", "Id");
            AddForeignKey("dbo.ResourceItem", "MusicId", "dbo.Music", "Id");
            AddForeignKey("dbo.AuthorisedUserGenres", "GenreId", "dbo.Genre", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuthorisedUserGenres", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.ResourceItem", "MusicId", "dbo.Music");
            DropForeignKey("dbo.MusicTrack", "MusicId", "dbo.Music");
            DropForeignKey("dbo.MusicMusicians", "MusiciansId", "dbo.Musician");
            DropForeignKey("dbo.MusicMusicians", "MusicId", "dbo.Music");
            DropForeignKey("dbo.MusicReleaseGenres", "MusicGenreId", "dbo.MusicGenres");
            DropForeignKey("dbo.MusicReleaseGenres", "MusicId", "dbo.Music");
            DropForeignKey("dbo.Season", "TvSeriesId", "dbo.TvSerias");
            DropForeignKey("dbo.TvSeriasToPersons", "PersonId", "dbo.Person");
            DropForeignKey("dbo.TvSeriasToPersons", "TvSeriasId", "dbo.TvSerias");
            DropForeignKey("dbo.TvSeriasToGenres", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.TvSeriasToGenres", "TvSeriasId", "dbo.TvSerias");
            DropForeignKey("dbo.Musician", "Id", "dbo.Subscribable");
            DropForeignKey("dbo.TvSerias", "Id", "dbo.Subscribable");
            DropForeignKey("dbo.Person", "Id", "dbo.Subscribable");
            DropForeignKey("dbo.Genre", "Id", "dbo.Subscribable");
            DropForeignKey("dbo.Music", "Id", "dbo.Subscribable");
            DropForeignKey("dbo.MusicGenres", "Id", "dbo.Subscribable");
            DropForeignKey("dbo.SubscriptionsToUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.SubscriptionsToUsers", "SubscriptionId", "dbo.Subscribable");
            DropIndex("dbo.Musician", new[] { "Id" });
            DropIndex("dbo.TvSerias", new[] { "Id" });
            DropIndex("dbo.Person", new[] { "Id" });
            DropIndex("dbo.Genre", new[] { "Id" });
            DropIndex("dbo.Music", new[] { "Id" });
            DropIndex("dbo.MusicGenres", new[] { "Id" });
            DropIndex("dbo.AuthorisedUserGenres", new[] { "GenreId" });
            DropIndex("dbo.TvSeriasToGenres", new[] { "GenreId" });
            DropIndex("dbo.SubscriptionsToUsers", new[] { "UserId" });
            DropIndex("dbo.SubscriptionsToUsers", new[] { "SubscriptionId" });
            DropPrimaryKey("dbo.TvSeriasToGenres");
            DropPrimaryKey("dbo.AuthorisedUserGenres");
            DropPrimaryKey("dbo.Musician");
            DropPrimaryKey("dbo.Music");
            DropPrimaryKey("dbo.MusicGenres");
            DropPrimaryKey("dbo.Person");
            DropPrimaryKey("dbo.TvSerias");
            DropPrimaryKey("dbo.Genre");
            AlterColumn("dbo.TvSeriasToGenres", "GenreId", c => c.Short(nullable: false));
            AlterColumn("dbo.AuthorisedUserGenres", "GenreId", c => c.Short(nullable: false));
            AlterColumn("dbo.Musician", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Music", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.MusicGenres", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Person", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.TvSerias", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Genre", "Id", c => c.Short(nullable: false, identity: true));
            DropTable("dbo.SubscriptionsToUsers");
            DropTable("dbo.Subscribable");
            AddPrimaryKey("dbo.TvSeriasToGenres", new[] { "TvSeriasId", "GenreId" });
            AddPrimaryKey("dbo.AuthorisedUserGenres", new[] { "AuthorisedUserId", "GenreId" });
            AddPrimaryKey("dbo.Musician", "Id");
            AddPrimaryKey("dbo.Music", "Id");
            AddPrimaryKey("dbo.MusicGenres", "Id");
            AddPrimaryKey("dbo.Person", "Id");
            AddPrimaryKey("dbo.TvSerias", "Id");
            AddPrimaryKey("dbo.Genre", "Id");
            CreateIndex("dbo.TvSeriasToGenres", "GenreId");
            CreateIndex("dbo.AuthorisedUserGenres", "GenreId");
            AddForeignKey("dbo.AuthorisedUserGenres", "GenreId", "dbo.Genre", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ResourceItem", "MusicId", "dbo.Music", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MusicTrack", "MusicId", "dbo.Music", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MusicMusicians", "MusiciansId", "dbo.Musician", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MusicMusicians", "MusicId", "dbo.Music", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MusicReleaseGenres", "MusicGenreId", "dbo.MusicGenres", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MusicReleaseGenres", "MusicId", "dbo.Music", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Season", "TvSeriesId", "dbo.TvSerias", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TvSeriasToPersons", "PersonId", "dbo.Person", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TvSeriasToPersons", "TvSeriasId", "dbo.TvSerias", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TvSeriasToGenres", "GenreId", "dbo.Genre", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TvSeriasToGenres", "TvSeriasId", "dbo.TvSerias", "Id", cascadeDelete: true);
        }
    }
}
