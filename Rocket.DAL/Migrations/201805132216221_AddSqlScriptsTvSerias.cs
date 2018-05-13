using System.IO;

namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSqlScriptsTvSerias : DbMigration
    {
        public override void Up()
        {
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
        }
        
        public override void Down()
        {
        }
    }
}
