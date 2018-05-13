using System.IO;

namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdByParser2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resource", "ParseIsSwitchOn", c => c.Boolean(nullable: false));
            AddColumn("dbo.Resource", "ParsePeriodInMinutes", c => c.Int(nullable: false));

            //��������� sql-������, ������� ����������������� ��������� ��� ������� album-info.ru
            var sqlFile = AppDomain.CurrentDomain.BaseDirectory + "../../Migrations/SQLQueries/InitAlbumInfoParserSettings.sql";
            Sql(File.ReadAllText(sqlFile));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Resource", "ParsePeriodInMinutes");
            DropColumn("dbo.Resource", "ParseIsSwitchOn");
        }
    }
}
