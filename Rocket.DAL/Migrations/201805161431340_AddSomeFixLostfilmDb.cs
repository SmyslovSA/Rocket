namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSomeFixLostfilmDb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Genre", "TvSeriasEntity_Id", "dbo.TvSerias");
            DropIndex("dbo.Genre", new[] { "TvSeriasEntity_Id" });
            CreateTable(
                "dbo.GenresToTvSerias",
                c => new
                    {
                        GenreId = c.Int(nullable: false),
                        TvSeriasId = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.GenreId, t.TvSeriasId })
                .ForeignKey("dbo.TvSerias", t => t.GenreId, cascadeDelete: true)
                .ForeignKey("dbo.Genre", t => t.TvSeriasId, cascadeDelete: true)
                .Index(t => t.GenreId)
                .Index(t => t.TvSeriasId);
            
            DropColumn("dbo.Genre", "TvSeriasEntity_Id");
            DropColumn("dbo.TvSerias", "ListGenreForParse");
            DropColumn("dbo.Person", "TvSeriasId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Person", "TvSeriasId", c => c.Int(nullable: false));
            AddColumn("dbo.TvSerias", "ListGenreForParse", c => c.String());
            AddColumn("dbo.Genre", "TvSeriasEntity_Id", c => c.Int());
            DropForeignKey("dbo.GenresToTvSerias", "TvSeriasId", "dbo.Genre");
            DropForeignKey("dbo.GenresToTvSerias", "GenreId", "dbo.TvSerias");
            DropIndex("dbo.GenresToTvSerias", new[] { "TvSeriasId" });
            DropIndex("dbo.GenresToTvSerias", new[] { "GenreId" });
            DropTable("dbo.GenresToTvSerias");
            CreateIndex("dbo.Genre", "TvSeriasEntity_Id");
            AddForeignKey("dbo.Genre", "TvSeriasEntity_Id", "dbo.TvSerias", "Id");
        }
    }
}
