namespace ATWPJWebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportStatusIsDone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "IsDone", c => c.Boolean(nullable: false));
            DropColumn("dbo.Reports", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reports", "Status", c => c.Boolean(nullable: false));
            DropColumn("dbo.Reports", "IsDone");
        }
    }
}
