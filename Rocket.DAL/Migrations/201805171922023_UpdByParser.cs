namespace Rocket.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UpdByParser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MusicTrack", "Number");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MusicTrack", "Number", c => c.Int(nullable: false));
        }
    }
}
