using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.ViewModels
{
    public class PostsTag
    {
        public TagDTO Tag { get; set; }
        public PagedData<PostDTO> Posts { get; set; }
    }
}