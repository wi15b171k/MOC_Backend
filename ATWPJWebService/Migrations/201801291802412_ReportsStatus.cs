namespace ATWPJWebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportsStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reports", "Status");
        }
    }
}
