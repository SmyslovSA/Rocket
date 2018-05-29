namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Serg : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DbPermissions", newName: "t_permission");
            RenameColumn(table: "dbo.t_permission", name: "Id", newName: "permission_id");
            RenameColumn(table: "dbo.t_permission", name: "ValueName", newName: "value_name");
            AlterColumn("dbo.t_permission", "description", c => c.String(maxLength: 250));
            AlterColumn("dbo.t_permission", "value_name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.t_permission", "value_name", c => c.String());
            AlterColumn("dbo.t_permission", "description", c => c.String());
            RenameColumn(table: "dbo.t_permission", name: "value_name", newName: "ValueName");
            RenameColumn(table: "dbo.t_permission", name: "permission_id", newName: "Id");
            RenameTable(name: "dbo.t_permission", newName: "DbPermissions");
        }
    }
}
