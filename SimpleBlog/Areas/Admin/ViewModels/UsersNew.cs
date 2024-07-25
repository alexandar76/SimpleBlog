using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleBlog.Areas.Admin.ViewModels
{
    public class UsersNew
    {
        [Required, MaxLength(128)]
        public string Username { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MaxLength(256),DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public IList<RoleCheckbox> Roles { get; set; }
    }
}