namespace ATWPJWebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Trip : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        IsPrivate = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trips", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Trips", new[] { "UserId" });
            DropTable("dbo.Trips");
        }
    }
}
