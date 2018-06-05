namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAutUserMusicGenres : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthorisedUserMusicGenres",
                c => new
                    {
                        AuthorisedUserId = c.Int(nullable: false),
                        MusicGenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AuthorisedUserId, t.MusicGenreId })
                .ForeignKey("dbo.AuthorisedUsers", t => t.AuthorisedUserId, cascadeDelete: true)
                .ForeignKey("dbo.MusicGenres", t => t.MusicGenreId, cascadeDelete: true)
                .Index(t => t.AuthorisedUserId)
                .Index(t => t.MusicGenreId);           
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuthorisedUserMusicGenres", "MusicGenreId", "dbo.MusicGenres");
            DropForeignKey("dbo.AuthorisedUserMusicGenres", "AuthorisedUserId", "dbo.AuthorisedUsers");
            DropIndex("dbo.AuthorisedUserMusicGenres", new[] { "MusicGenreId" });
            DropIndex("dbo.AuthorisedUserMusicGenres", new[] { "AuthorisedUserId" });
            DropTable("dbo.AuthorisedUserMusicGenres");
        }
    }
}
