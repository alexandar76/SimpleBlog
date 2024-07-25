using SimpleBlog.Models;
using SimpleBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    public class LayoutController : Controller
    {
        // GET: Layout
        [ChildActionOnly]
        public ActionResult Sidebar()
        {
            using (Db db = new Db())
            {
                return View(new LayoutSidebar
                {
                    IsLoggedIn = Auth.User != null,
                    Username = Auth.User != null ? Auth.User.Username : "",
                    IsAdmin = User.IsInRole("Admin"),
                    Tags = db.Tags.Select(tag => new
                    {
                        tag.Id,
                        tag.Name,
                        tag.Slug,
                        PostCount = tag.Posts.Count
                    }).Where(t => t.PostCount > 0).OrderByDescending(p => p.PostCount).AsEnumerable().Select(
                        tag => new SidebarTag(tag.Id, tag.Name, tag.Slug, tag.PostCount)).ToList()
                });
            }
        }
    }
}