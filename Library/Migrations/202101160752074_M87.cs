namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M87 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "Book_ID", "dbo.Books");
            DropIndex("dbo.Images", new[] { "Book_ID" });
            DropTable("dbo.Images");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        photoPath = c.String(),
                        Book_ID = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateIndex("dbo.Images", "Book_ID");
            AddForeignKey("dbo.Images", "Book_ID", "dbo.Books", "ID");
        }
    }
}
