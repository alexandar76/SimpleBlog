using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Areas.Admin.ViewModels
{
    public class UsersIndex
    {
        public UsersIndex(UserDTO row)
        {
            Id = row.Id;
            Username = row.Username;
            Email = row.Email;
            PasswordHash = row.PasswordHash;
            Rolename = row.Rolename;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Rolename { get; set; }
        public IEnumerable<RoleDTO> Roles { get; set; }
        public IEnumerable<UserDTO> Users { get; set; }
    }
}