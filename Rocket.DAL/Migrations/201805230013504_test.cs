namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotificationsLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NotificationType = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        ReleaseType = c.Int(nullable: false),
                        ReleaseId = c.Int(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NotificationsLog");
        }
    }
}
