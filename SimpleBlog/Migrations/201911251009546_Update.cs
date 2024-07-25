namespace SimpleBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.roles", "UserDTO_Id", c => c.Int());
            CreateIndex("dbo.roles", "UserDTO_Id");
            AddForeignKey("dbo.roles", "UserDTO_Id", "dbo.users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.roles", "UserDTO_Id", "dbo.users");
            DropIndex("dbo.roles", new[] { "UserDTO_Id" });
            DropColumn("dbo.roles", "UserDTO_Id");
        }
    }
}
