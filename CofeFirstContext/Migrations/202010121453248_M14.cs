namespace CofeFirstContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M14 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Authors", "BookID", "dbo.Books");
            DropIndex("dbo.Authors", new[] { "BookID" });
            AlterColumn("dbo.Authors", "BookID", c => c.Int());
            CreateIndex("dbo.Authors", "BookID");
            AddForeignKey("dbo.Authors", "BookID", "dbo.Books", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Authors", "BookID", "dbo.Books");
            DropIndex("dbo.Authors", new[] { "BookID" });
            AlterColumn("dbo.Authors", "BookID", c => c.Int(nullable: false));
            CreateIndex("dbo.Authors", "BookID");
            AddForeignKey("dbo.Authors", "BookID", "dbo.Books", "ID", cascadeDelete: true);
        }
    }
}
