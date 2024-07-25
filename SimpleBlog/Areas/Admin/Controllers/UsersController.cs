using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.Infrastructure;
using SimpleBlog.Migrations;
using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Areas.Admin.Controllers
{

    [Authorize(Roles = "admin")]
    [SelectedTab("users")]
    public class UsersController : Controller
    {
        // GET: Admin/Users
        public ActionResult Index()
        {
            List<UsersIndex> UserList;
            using (Db db = new Db())
            {
                UserList = db.Users.ToArray().Select(x => new UsersIndex(x)).ToList();
            }
            
            return View(UserList);
        }

        public ActionResult New()
        {
            using (Db db = new Db())
            {
                return View(new UsersNew
                {
                    Roles = db.Roles.Select(role => new RoleCheckbox
                    {
                        Id = role.Id,
                        IsChecked = false,
                        Name = role.Name
                    }).ToList()
                });
            }
            
        }

        [HttpPost]
        public ActionResult New(UsersNew form, IList<UserDTO> roles, IList<RoleCheckbox> checkboxes)
        {
            using (Db db = new Db())
            {
               
                if (db.Users.Any(x => x.Username == form.Username))
                    ModelState.AddModelError("Username", "Username must be unique");

                if (!ModelState.IsValid)
                    return View(form);

                var user = new UserDTO
                {
                    Email = form.Email,
                    Username = form.Username,
                    //PasswordHash = form.Password
                    Rolename = SyncRoles(form.Roles)
                };
                //SyncRoles(form.Roles, user.Users);
                //Save(user.Users);

             

                    //foreach (var role in db.Users)
                    //{
                    //    var checkbox = checkboxes.ToList().FirstOrDefault(p => p.Id == role.Id);
                    //    checkbox.Name = role.Rolename;
                    //}



                user.SetPassword(form.Password);         
                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("index");
            }
        }

        public ActionResult Edit(int id)
        {
            
            using (Db db = new Db())
            {
                UserDTO dto = db.Users.Find(id);

                if (dto == null)
                {
                    return HttpNotFound();
                }
                return View(new UsersEdit
                {
                    Username = dto.Username,
                    Email = dto.Email,
                    //Password = dto.PasswordHash
                    Roles = db.Roles.Select(role => new RoleCheckbox
                    {
                        Id = role.Id,
                        IsChecked = dto.Roles.Contains(role),
                   
                        //IsChecked = true,
                        Name = role.Name
                    }).ToList()
                });
            }         
        }

        [HttpPost]
        public ActionResult Edit(int id, UsersEdit form)
        {
            using (Db db = new Db())
            {
                var user = db.Users.Find(id);

                if (user == null)
                {
                    return HttpNotFound();
                }
            }

            //Make sure  username is unique
            //using (Db db = new Db())
            //{
            //    if (db.Users.Where(x => x.Id != id).Any(x => x.Username == form.Username))
            //    {
            //        ModelState.AddModelError("", "Username is taken!");
            //    }
            //}

            //if (!ModelState.IsValid)
            //{
            //    return View(form);
            //}

            //Update User
            using (Db db = new Db())
            {
                var user = db.Users.Find(id);
                user.Username = form.Username;
                //user.PasswordHash = form.Password;
                user.Email = form.Email;

                user.Rolename = SyncRoles(form.Roles);

                db.SaveChanges();
                //SaveRoles(form.Roles, user.RoleUsers);
            }

            return RedirectToAction("index");
        }

        public ActionResult ResetPassword(int id)
        {
            using (Db db = new Db())
            {
                UserDTO dto = db.Users.Find(id);

                if (dto == null)
                {
                    return HttpNotFound();
                }
                return View(new UsersResetPassword
                {
                    Username = dto.Username,
                    
                });
            }
        }

        [HttpPost]
        public ActionResult ResetPassword(int id, UsersResetPassword form)
        {
            using (Db db = new Db())
            {
                var user = db.Users.Find(id);

                if (user == null)
                {
                    return HttpNotFound();
                }
            }

            using (Db db = new Db())
            {
                var user = db.Users.Find(id);
                form.Username = user.Username;
                user.SetPassword(form.Password);
                //form.Password = user.PasswordHash;
                db.SaveChanges();
            }

            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (Db db = new Db())
            {
                var user = db.Users.Find(id);

                if (user == null)
                {
                    return HttpNotFound();
                }
                db.Users.Remove(user);
                db.SaveChanges();
            }

            return RedirectToAction("index");
        }
       
        private string SyncRoles(IList<RoleCheckbox> checkboxes)
        {
            var selectedRoles = new List<UserDTO>();
            
            var checkbox = checkboxes.First(x => x.IsChecked == true);
            var value = checkbox.Name;
            return value;

            using (Db db = new Db())
            {
                

                foreach (var role in db.Users)
                {
                    //var checkbox = checkboxes.First(x => x.IsChecked == true);
                    //role.Rolename = checkbox.Name;
                    //var value = checkbox.Name;
                    //var checkbox = checkboxes.SingleOrDefault(c => c.Id == role.role_id);
                    //var checkbox = checkboxes.ToList().Find(p => p.Id == role.Id);
                    //checkbox.Name = role.Rolename;
                    //checkbox.Id = role.user_id;
                    //if (checkbox.IsChecked)
                    //db.Users.Add(role);
                    //selectedRoles.Add(role);
                    //db.SaveChanges();
                    //return value;
                }
                

                //foreach (var toAdd in selectedRoles.Where(t => !roles.Contains(t)))
                //    roles.Add(toAdd);

                //foreach (var toRemove in roles.Where(t => !selectedRoles.Contains(t)).ToList())
                //    roles.Remove(toRemove);
            }
        }

        private void Save(IList<UserDTO> roles)
        {
            using (Db db = new Db())
            {
                foreach (UserDTO role in roles)
                {
                    UserDTO update = db.Users.ToList().Find(p => p.Id == role.Id);
                    update.Rolename = role.Rolename;
                }
                db.SaveChanges();
            }
        }



    }
}