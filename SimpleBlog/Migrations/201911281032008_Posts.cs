namespace SimpleBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Posts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Slug = c.String(),
                        Content = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Slug = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.post_tags",
                c => new
                    {
                        tag_id = c.Int(nullable: false),
                        post_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.tag_id, t.post_id })
                .ForeignKey("dbo.posts", t => t.post_id, cascadeDelete: true)
                .ForeignKey("dbo.tags", t => t.tag_id, cascadeDelete: true)
                .Index(t => t.tag_id)
                .Index(t => t.post_id);
            
            CreateTable(
                "dbo.TagDTOPostDTOes",
                c => new
                    {
                        TagDTO_Id = c.Int(nullable: false),
                        PostDTO_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TagDTO_Id, t.PostDTO_Id })
                .ForeignKey("dbo.tags", t => t.TagDTO_Id, cascadeDelete: true)
                .ForeignKey("dbo.posts", t => t.PostDTO_Id, cascadeDelete: true)
                .Index(t => t.TagDTO_Id)
                .Index(t => t.PostDTO_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.post_tags", "tag_id", "dbo.tags");
            DropForeignKey("dbo.post_tags", "post_id", "dbo.posts");
            DropForeignKey("dbo.posts", "User_Id", "dbo.users");
            DropForeignKey("dbo.TagDTOPostDTOes", "PostDTO_Id", "dbo.posts");
            DropForeignKey("dbo.TagDTOPostDTOes", "TagDTO_Id", "dbo.tags");
            DropIndex("dbo.TagDTOPostDTOes", new[] { "PostDTO_Id" });
            DropIndex("dbo.TagDTOPostDTOes", new[] { "TagDTO_Id" });
            DropIndex("dbo.post_tags", new[] { "post_id" });
            DropIndex("dbo.post_tags", new[] { "tag_id" });
            DropIndex("dbo.posts", new[] { "User_Id" });
            DropTable("dbo.TagDTOPostDTOes");
            DropTable("dbo.post_tags");
            DropTable("dbo.tags");
            DropTable("dbo.posts");
        }
    }
}
