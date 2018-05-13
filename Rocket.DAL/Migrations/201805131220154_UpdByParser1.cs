namespace Rocket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdByParser1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Resource Item", newName: "ResourceItem");
            RenameTable(name: "dbo.Parser Settings", newName: "ParserSettings");
            RenameColumn(table: "dbo.ResourceItem", name: "Resource Id", newName: "ResourceId");
            RenameColumn(table: "dbo.ResourceItem", name: "Resource Internal Id", newName: "ResourceInternalId");
            RenameColumn(table: "dbo.ResourceItem", name: "Created Date Time", newName: "CreatedDateTime");
            RenameColumn(table: "dbo.ResourceItem", name: "Last Modified", newName: "LastModified");
            RenameColumn(table: "dbo.ResourceItem", name: "Music Id", newName: "MusicId");
            RenameColumn(table: "dbo.Resource", name: "Resource Link", newName: "ResourceLink");
            RenameColumn(table: "dbo.ParserSettings", name: "Base Url", newName: "BaseUrl");
            RenameColumn(table: "dbo.ParserSettings", name: "Start Point", newName: "StartPoint");
            RenameColumn(table: "dbo.ParserSettings", name: "End Point", newName: "EndPoint");
            RenameIndex(table: "dbo.ResourceItem", name: "IX_Resource Id", newName: "IX_ResourceId");
            RenameIndex(table: "dbo.ResourceItem", name: "IX_Music Id", newName: "IX_MusicId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ResourceItem", name: "IX_MusicId", newName: "IX_Music Id");
            RenameIndex(table: "dbo.ResourceItem", name: "IX_ResourceId", newName: "IX_Resource Id");
            RenameColumn(table: "dbo.ParserSettings", name: "EndPoint", newName: "End Point");
            RenameColumn(table: "dbo.ParserSettings", name: "StartPoint", newName: "Start Point");
            RenameColumn(table: "dbo.ParserSettings", name: "BaseUrl", newName: "Base Url");
            RenameColumn(table: "dbo.Resource", name: "ResourceLink", newName: "Resource Link");
            RenameColumn(table: "dbo.ResourceItem", name: "MusicId", newName: "Music Id");
            RenameColumn(table: "dbo.ResourceItem", name: "LastModified", newName: "Last Modified");
            RenameColumn(table: "dbo.ResourceItem", name: "CreatedDateTime", newName: "Created Date Time");
            RenameColumn(table: "dbo.ResourceItem", name: "ResourceInternalId", newName: "Resource Internal Id");
            RenameColumn(table: "dbo.ResourceItem", name: "ResourceId", newName: "Resource Id");
            RenameTable(name: "dbo.ParserSettings", newName: "Parser Settings");
            RenameTable(name: "dbo.ResourceItem", newName: "Resource Item");
        }
    }
}
