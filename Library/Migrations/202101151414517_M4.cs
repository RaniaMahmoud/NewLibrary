namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        photoPath = c.String(),
                        Book_ID = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Books", t => t.Book_ID)
                .Index(t => t.Book_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "Book_ID", "dbo.Books");
            DropIndex("dbo.Images", new[] { "Book_ID" });
            DropTable("dbo.Images");
        }
    }
}
