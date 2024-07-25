namespace SimpleBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rolename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.users", "Rolename", c => c.String());
            AddColumn("dbo.users", "UserDTO_Id", c => c.Int());
            CreateIndex("dbo.users", "UserDTO_Id");
            AddForeignKey("dbo.users", "UserDTO_Id", "dbo.users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.users", "UserDTO_Id", "dbo.users");
            DropIndex("dbo.users", new[] { "UserDTO_Id" });
            DropColumn("dbo.users", "UserDTO_Id");
            DropColumn("dbo.users", "Rolename");
        }
    }
}
