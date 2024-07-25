using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    public class Db : DbContext
    {
        public DbSet<UserDTO> Users { get; set; }
        public DbSet<RoleDTO> Roles { get; set; }
        public DbSet<RoleUsersDTO> RoleUsers { get; set; }
        public DbSet<PostDTO> Posts { get; set; }
        public DbSet<TagDTO> Tags { get; set; }
        public DbSet<PostTagsDTO> PostsTags { get; set; }
    }
}