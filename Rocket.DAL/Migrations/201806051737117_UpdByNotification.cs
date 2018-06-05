namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdByNotification : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Episode");
            CreateTable(
                "dbo.NotificationsLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReleaseId = c.Int(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Music", "PosterImageUrl", c => c.String());
            AddPrimaryKey("dbo.Episode", "Id");
            CreateIndex("dbo.Episode", "Id");
            AddForeignKey("dbo.Episode", "Id", "dbo.Subscribable", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Episode", "Id", "dbo.Subscribable");
            DropIndex("dbo.Episode", new[] { "Id" });
            DropPrimaryKey("dbo.Episode");
            DropColumn("dbo.NotificationsSettings", "PushUrl");
            DropColumn("dbo.Music", "PosterImageUrl");
            DropTable("dbo.NotificationsLog");
            AddPrimaryKey("dbo.Episode", "Id");
        }
    }
}
