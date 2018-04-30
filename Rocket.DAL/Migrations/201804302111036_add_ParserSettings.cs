namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_ParserSettings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Parser Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResourceId = c.Int(nullable: false),
                        BaseUrl = c.String(name: "Base Url", nullable: false, maxLength: 250),
                        Prefix = c.String(maxLength: 200),
                        StartPoint = c.Int(name: "Start Point"),
                        EndPoint = c.Int(name: "End Point"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resource", t => t.ResourceId, cascadeDelete: true)
                .Index(t => t.ResourceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Parser Settings", "ResourceId", "dbo.Resource");
            DropIndex("dbo.Parser Settings", new[] { "ResourceId" });
            DropTable("dbo.Parser Settings");
        }
    }
}
