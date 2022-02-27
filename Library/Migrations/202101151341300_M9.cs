namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M9 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "ImagePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "ImagePath", c => c.String());
        }
    }
}
