namespace CofeFirstContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "Text", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "Text", c => c.String(nullable: false));
        }
    }
}
