namespace SimpleBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Author : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.posts", "Author", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.posts", "Author");
        }
    }
}
