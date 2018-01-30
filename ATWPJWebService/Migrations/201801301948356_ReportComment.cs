namespace ATWPJWebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reports", "Comment");
        }
    }
}
