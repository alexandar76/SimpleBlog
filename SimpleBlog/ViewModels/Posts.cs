using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.ViewModels
{
    public class PostsIndex
    {
        public PagedData<PostDTO> Posts { get; set; }
    }

}