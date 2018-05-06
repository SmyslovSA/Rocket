namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdMusic_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Music", "Type", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Music", "Type");
        }
    }
}
