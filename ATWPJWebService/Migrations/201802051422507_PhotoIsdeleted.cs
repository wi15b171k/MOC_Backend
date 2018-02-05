namespace ATWPJWebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhotoIsdeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "isDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "isDeleted");
        }
    }
}
