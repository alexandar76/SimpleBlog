namespace SimpleBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.users", new[] { "UserDTO_Id" });
            AlterColumn("dbo.users", "UserDTO_Id", c => c.Int());
            CreateIndex("dbo.users", "UserDTO_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.users", new[] { "UserDTO_Id" });
            AlterColumn("dbo.users", "UserDTO_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.users", "UserDTO_Id");
        }
    }
}
