namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.MusicTrack", "Number", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MusicTrack", "Number", c => c.Int(nullable: false));
            DropTable("dbo.Category");
        }
    }
}
