namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdMusic : DbMigration
    {
        public override void Up()
        {
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
                        Title = c.String(nullable: false, maxLength: 50),
                        ReleaseDate = c.DateTime(nullable: false),
                        PosterImagePath = c.String(maxLength: 200),
                        Duration = c.Int(),
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
                        Duration = c.Int(),
                        MusicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Music", t => t.MusicId, cascadeDelete: true)
                .Index(t => t.MusicId);
            
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
            
            AddColumn("dbo.Resource Item", "Music Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Resource Item", "Music Id");
            AddForeignKey("dbo.Resource Item", "Music Id", "dbo.Music", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resource Item", "Music Id", "dbo.Music");
            DropForeignKey("dbo.MusicTrack", "MusicId", "dbo.Music");
            DropForeignKey("dbo.MusicMusicians", "MusiciansId", "dbo.Musician");
            DropForeignKey("dbo.MusicMusicians", "MusicId", "dbo.Music");
            DropForeignKey("dbo.MusicReleaseGenres", "MusicGenreId", "dbo.MusicGenres");
            DropForeignKey("dbo.MusicReleaseGenres", "MusicId", "dbo.Music");
            DropIndex("dbo.MusicMusicians", new[] { "MusiciansId" });
            DropIndex("dbo.MusicMusicians", new[] { "MusicId" });
            DropIndex("dbo.MusicReleaseGenres", new[] { "MusicGenreId" });
            DropIndex("dbo.MusicReleaseGenres", new[] { "MusicId" });
            DropIndex("dbo.Resource Item", new[] { "Music Id" });
            DropIndex("dbo.MusicTrack", new[] { "MusicId" });
            DropColumn("dbo.Resource Item", "Music Id");
            DropTable("dbo.MusicMusicians");
            DropTable("dbo.MusicReleaseGenres");
            DropTable("dbo.MusicTrack");
            DropTable("dbo.Musician");
            DropTable("dbo.Music");
            DropTable("dbo.MusicGenres");
        }
    }
}
