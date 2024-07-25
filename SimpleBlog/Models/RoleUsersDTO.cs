using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    [Table("role_users")]
    public class RoleUsersDTO
    {
        [Key, Column(Order = 0)]
        public int user_id { get; set; }
        [Key, Column(Order = 1)]
        public int role_id { get; set; }

        [ForeignKey("user_id")]
        public virtual UserDTO User { get; set; }
        [ForeignKey("role_id")]
        public virtual RoleDTO Role { get; set; }
    }
}