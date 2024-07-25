namespace SimpleBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.posts", "User_Id", "dbo.users");
            DropIndex("dbo.posts", new[] { "User_Id" });
            DropColumn("dbo.posts", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.posts", "User_Id", c => c.Int());
            CreateIndex("dbo.posts", "User_Id");
            AddForeignKey("dbo.posts", "User_Id", "dbo.users", "Id");
        }
    }
}
