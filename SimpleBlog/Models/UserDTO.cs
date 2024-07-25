using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    [Table("users")]
    public class UserDTO
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Rolename { get; set; }
        
        public IList<UserDTO> Users { get; set; }
        public IList<RoleDTO> Roles { get; set; }
        public IList<RoleUsersDTO> RoleUsers { get; set; }

        public UserDTO()
        {
            Roles = new List<RoleDTO>();
            RoleUsers = new List<RoleUsersDTO>();
            Users = new List<UserDTO>();
        }

        public void SetPassword(string password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, 13);
        }

        public bool CheckPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }

        public static void Fakehash()
        {
            BCrypt.Net.BCrypt.HashPassword("", 13);
        }
    }
}