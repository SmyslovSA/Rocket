namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Serg1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.t_permission", newName: "DBPermission");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.DBPermission", newName: "t_permission");
        }
    }
}
