namespace Rocket.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddNotificationsSettings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotificationsSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        NotifyIsSwitchOn = c.Boolean(nullable: false),
                        NotifyPeriodInMinutes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.NotificationsSettings");
        }
    }
}
