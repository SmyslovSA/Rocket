namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alekhno : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DbPermissions", newName: "DBPermission");
            DropForeignKey("dbo.DbEmails", "DbAuthorisedUserId", "dbo.AuthorisedUsers");
            DropForeignKey("dbo.AuthorisedUserGenres", "AuthorisedUserId", "dbo.AuthorisedUsers");
            RenameColumn(table: "dbo.DBPermission", name: "Id", newName: "permission_id");
            RenameColumn(table: "dbo.DBPermission", name: "ValueName", newName: "value_name");
            DropPrimaryKey("dbo.AuthorisedUsers");
            AlterColumn("dbo.DBPermission", "description", c => c.String(maxLength: 250));
            AlterColumn("dbo.DBPermission", "value_name", c => c.String(nullable: false, maxLength: 50));
            AddPrimaryKey("dbo.AuthorisedUsers", "DbUserId");
            AddForeignKey("dbo.DbEmails", "DbAuthorisedUserId", "dbo.AuthorisedUsers", "DbUserId", cascadeDelete: true);
            AddForeignKey("dbo.AuthorisedUserGenres", "AuthorisedUserId", "dbo.AuthorisedUsers", "DbUserId", cascadeDelete: true);
            DropColumn("dbo.AuthorisedUsers", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AuthorisedUsers", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.AuthorisedUserGenres", "AuthorisedUserId", "dbo.AuthorisedUsers");
            DropForeignKey("dbo.DbEmails", "DbAuthorisedUserId", "dbo.AuthorisedUsers");
            DropPrimaryKey("dbo.AuthorisedUsers");
            AlterColumn("dbo.DBPermission", "value_name", c => c.String());
            AlterColumn("dbo.DBPermission", "description", c => c.String());
            AddPrimaryKey("dbo.AuthorisedUsers", "Id");
            RenameColumn(table: "dbo.DBPermission", name: "value_name", newName: "ValueName");
            RenameColumn(table: "dbo.DBPermission", name: "permission_id", newName: "Id");
            AddForeignKey("dbo.AuthorisedUserGenres", "AuthorisedUserId", "dbo.AuthorisedUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DbEmails", "DbAuthorisedUserId", "dbo.AuthorisedUsers", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.DBPermission", newName: "DbPermissions");
        }
    }
}
