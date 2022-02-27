namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M54 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "FilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "FilePath");
        }
    }
}
