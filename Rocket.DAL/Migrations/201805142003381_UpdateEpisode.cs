namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEpisode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Episode", "DurationInMinutes", c => c.Double());
            DropColumn("dbo.Episode", "Duration");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Episode", "Duration", c => c.Time(precision: 7));
            DropColumn("dbo.Episode", "DurationInMinutes");
        }
    }
}
