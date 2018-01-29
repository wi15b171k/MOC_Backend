namespace ATWPJWebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Requests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsNew = c.Boolean(nullable: false),
                        IsAccepted = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        RequestFromUserId = c.String(maxLength: 128),
                        RequestToUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.RequestFromUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.RequestToUserId)
                .Index(t => t.RequestFromUserId)
                .Index(t => t.RequestToUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "RequestToUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Requests", "RequestFromUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Requests", new[] { "RequestToUserId" });
            DropIndex("dbo.Requests", new[] { "RequestFromUserId" });
            DropTable("dbo.Requests");
        }
    }
}
