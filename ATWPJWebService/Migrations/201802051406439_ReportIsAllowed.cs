namespace ATWPJWebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportIsAllowed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "IsAllowed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reports", "IsAllowed");
        }
    }
}
