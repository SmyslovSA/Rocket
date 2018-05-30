namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alekhno : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DbEmails", "DbAuthorisedUserId", "dbo.AuthorisedUsers");
            DropForeignKey("dbo.AuthorisedUserGenres", "AuthorisedUserId", "dbo.AuthorisedUsers");
            DropPrimaryKey("dbo.AuthorisedUsers");
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
            AddPrimaryKey("dbo.AuthorisedUsers", "Id");
            AddForeignKey("dbo.AuthorisedUserGenres", "AuthorisedUserId", "dbo.AuthorisedUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DbEmails", "DbAuthorisedUserId", "dbo.AuthorisedUsers", "Id", cascadeDelete: true);
        }
    }
}
