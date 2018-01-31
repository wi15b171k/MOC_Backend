namespace ATWPJWebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PicsDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Photos", "Longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Photos", "Latitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Photos", "Latitude", c => c.Single(nullable: false));
            AlterColumn("dbo.Photos", "Longitude", c => c.Single(nullable: false));
        }
    }
}
