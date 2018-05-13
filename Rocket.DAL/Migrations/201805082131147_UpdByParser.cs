namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdByParser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MusicTrack", "Title", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.MusicTrack", "Number");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MusicTrack", "Number", c => c.Int(nullable: false));
            AlterColumn("dbo.MusicTrack", "Title", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
