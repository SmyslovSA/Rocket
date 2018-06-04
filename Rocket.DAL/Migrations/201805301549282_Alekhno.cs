namespace Rocket.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Alekhno : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DbPermissions", newName: "DBPermission");
            RenameColumn(table: "dbo.DBPermission", name: "Id", newName: "permission_id");
            RenameColumn(table: "dbo.DBPermission", name: "ValueName", newName: "value_name");
            AlterColumn("dbo.DBPermission", "description", c => c.String(maxLength: 250));
            AlterColumn("dbo.DBPermission", "value_name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DBPermission", "value_name", c => c.String());
            AlterColumn("dbo.DBPermission", "description", c => c.String());
            RenameColumn(table: "dbo.DBPermission", name: "value_name", newName: "ValueName");
            RenameColumn(table: "dbo.DBPermission", name: "permission_id", newName: "Id");
            RenameTable(name: "dbo.DBPermission", newName: "DbPermissions");
        }
    }
}
