using System.IO;

namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitData : DbMigration
    {
        public override void Up()
        {
            //запускаем sql-скрипт, который проинициализирует настройки для парсера album-info.ru
            var sqlFile = AppDomain.CurrentDomain.BaseDirectory + "../../Migrations/SQLQueries/InitAlbumInfoParserSettings.sql";
            Sql(File.ReadAllText(sqlFile));

            //запускаем sql-скрипт, который проинициализирует настройки для парсера lostfilm
            var sqlFileInitLostfilmParserSettings = AppDomain.CurrentDomain.BaseDirectory +
                                                    "../../Migrations/SQLQueries/InitLostfilmParserSettings.sql";
            Sql(File.ReadAllText(sqlFileInitLostfilmParserSettings));

            //запускаем sql-скрипт, который проинициализирует типы персон
            var sqlFileInitPersonTypes = AppDomain.CurrentDomain.BaseDirectory +
                                         "../../Migrations/SQLQueries/InitPersonTypes.sql";
            Sql(File.ReadAllText(sqlFileInitPersonTypes));

            //запускаем sql-скрипт, который проинициализирует категории
            var sqlFileInitCategory = AppDomain.CurrentDomain.BaseDirectory +
                                      "../../Migrations/SQLQueries/InitCategory.sql";
            Sql(File.ReadAllText(sqlFileInitCategory));

            var sqlFileEmail = AppDomain.CurrentDomain.BaseDirectory + "../../Migrations/SQLQueries/InitEmailTemplates.sql";
            Sql(File.ReadAllText(sqlFileEmail));

            //инициализация настроек сервиса нотификации
            var sqlFileNotificationSettings = AppDomain.CurrentDomain.BaseDirectory + "../../Migrations/SQLQueries/InitNotificationsSettings.sql";
            Sql(File.ReadAllText(sqlFileNotificationSettings));
        }
        
        public override void Down()
        {
        }
    }
}
