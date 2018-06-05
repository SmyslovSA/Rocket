using System.IO;

namespace Rocket.DAL.Migrations
{
    using System;
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
                        PushUrl = c.String(nullable: false, maxLength: 100),
                })
                .PrimaryKey(t => t.Id);

            //запускаем sql-скрипт, который проинициализирует настройки службы уведомлений
            var sqlFile = AppDomain.CurrentDomain.BaseDirectory + "../../Migrations/SQLQueries/InitNotificationsSettings.sql";
            Sql(File.ReadAllText(sqlFile));
        }

        public override void Down()
        {
            DropTable("dbo.NotificationsSettings");
        }
    }
}
