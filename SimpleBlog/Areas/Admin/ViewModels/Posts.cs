using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Areas.Admin.ViewModels
{
    public class PostsIndex
    {
        //public PostsIndex(PostDTO row)
        //{
        //    Id = row.Id;
        //    User = row.User;
        //    Title = row.Title;
        //    Slug = row.Slug;
        //    Content = row.Content;
        //    CreatedAt = row.CreatedAt;
        //    UpdatedAt = row.UpdatedAt;
        //    DeletedAt = row.DeletedAt;
        //}
        public PagedData<PostDTO> Posts { get; set; }

        public int Id { get; set; }
        public UserDTO User { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

     
    }
}