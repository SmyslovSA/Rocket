namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotifications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReceiverId = c.Int(nullable: false),
                        SenderName = c.String(nullable: false),
                        Subject = c.String(),
                        Body = c.String(nullable: false),
                        HtmlBody = c.Boolean(nullable: false),
                        Viewed = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Receivers", t => t.ReceiverId, cascadeDelete: true)
                .Index(t => t.ReceiverId);
            
            CreateTable(
                "dbo.Receivers",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        NotifyByEmail = c.Boolean(nullable: false),
                        NotifyByPush = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.ReceiversJoinReleases",
                c => new
                    {
                        ReceiverId = c.Int(nullable: false),
                        ReleaseMessageId = c.Int(nullable: false),
                        Viewed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ReceiverId, t.ReleaseMessageId })
                .ForeignKey("dbo.ReleaseMessages", t => t.ReleaseMessageId, cascadeDelete: true)
                .ForeignKey("dbo.Receivers", t => t.ReceiverId, cascadeDelete: true)
                .Index(t => t.ReceiverId)
                .Index(t => t.ReleaseMessageId);
            
            CreateTable(
                "dbo.ReleaseMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReleaseId = c.Int(nullable: false),
                        ReleaseType = c.Int(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserBillingMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReceiverId = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Viewed = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Receivers", t => t.ReceiverId, cascadeDelete: true)
                .Index(t => t.ReceiverId);
            
            CreateTable(
                "dbo.EmailTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Body = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GuestBillingMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserBillingMessages", "ReceiverId", "dbo.Receivers");
            DropForeignKey("dbo.ReceiversJoinReleases", "ReceiverId", "dbo.Receivers");
            DropForeignKey("dbo.ReceiversJoinReleases", "ReleaseMessageId", "dbo.ReleaseMessages");
            DropForeignKey("dbo.CustomMessages", "ReceiverId", "dbo.Receivers");
            DropIndex("dbo.UserBillingMessages", new[] { "ReceiverId" });
            DropIndex("dbo.ReceiversJoinReleases", new[] { "ReleaseMessageId" });
            DropIndex("dbo.ReceiversJoinReleases", new[] { "ReceiverId" });
            DropIndex("dbo.CustomMessages", new[] { "ReceiverId" });
            DropTable("dbo.GuestBillingMessages");
            DropTable("dbo.EmailTemplates");
            DropTable("dbo.UserBillingMessages");
            DropTable("dbo.ReleaseMessages");
            DropTable("dbo.ReceiversJoinReleases");
            DropTable("dbo.Receivers");
            DropTable("dbo.CustomMessages");
        }
    }
}
