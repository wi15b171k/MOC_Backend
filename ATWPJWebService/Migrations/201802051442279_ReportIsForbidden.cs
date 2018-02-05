namespace ATWPJWebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportIsForbidden : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "IsForbidden", c => c.Boolean(nullable: false));
            DropColumn("dbo.Reports", "IsAllowed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reports", "IsAllowed", c => c.Boolean(nullable: false));
            DropColumn("dbo.Reports", "IsForbidden");
        }
    }
}
