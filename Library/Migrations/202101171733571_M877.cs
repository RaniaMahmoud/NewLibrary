namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M877 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "ImagePath");
        }
    }
}
