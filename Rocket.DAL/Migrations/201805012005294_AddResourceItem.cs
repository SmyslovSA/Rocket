namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddResourceItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Resource Item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResourceId = c.Int(name: "Resource Id", nullable: false),
                        ResourceInternalId = c.String(name: "Resource Internal Id", nullable: false, maxLength: 50),
                        ResourceItemLink = c.String(),
                        CreatedDateTime = c.DateTime(name: "Created Date Time", nullable: false, defaultValueSql: "GETDATE()"),
                        LastModified = c.DateTime(name: "Last Modified", nullable: false, defaultValueSql: "GETDATE()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resource", t => t.ResourceId, cascadeDelete: true)
                .Index(t => t.ResourceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resource Item", "Resource Id", "dbo.Resource");
            DropIndex("dbo.Resource Item", new[] { "Resource Id" });
            DropTable("dbo.Resource Item");
        }
    }
}
