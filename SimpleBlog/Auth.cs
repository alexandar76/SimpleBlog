using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleBlog.Models;


namespace SimpleBlog
{
    public class Auth
    {
        private const string UserKey = "SimpleBlog.Auth.UserKey";

        public static UserDTO User
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;

                var user = HttpContext.Current.Items[UserKey] as UserDTO;
                if (user == null)
                {
                    using (Db db = new Db())
                    {
                        user = db.Users.FirstOrDefault(u => u.Username == HttpContext.Current.User.Identity.Name);

                        if (user == null)
                            return null;

                        HttpContext.Current.Items[UserKey] = user;
                    }
                }

                return user;
            }
        }
    }
}