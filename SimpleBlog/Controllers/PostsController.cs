using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using SimpleBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    public class PostsController : Controller
    {
        private const int PostPerPage = 10;
        // GET: Posts
        public ActionResult Index(int page = 1)
        {
           //return View();
            using (Db db = new Db())
            {
                var baseQuery = db.Posts.Where(t => t.DeletedAt == null).OrderByDescending(t => t.CreatedAt);

                var totalPostCount = baseQuery.Count();
                var postIds = baseQuery.Skip((page - 1) * PostPerPage).Take(PostPerPage).Select(t => t.Id).ToArray();
                //var posts = baseQuery.Where(t => postIds.Contains(t.Id)).FetchMany(t => t.Tags).Fetch(t => t.User).toList();
                var posts = db.Posts.OrderByDescending(c => c.CreatedAt).Skip((page - 1) * PostPerPage).Take(PostPerPage).ToList();
                return View(new PostsIndex
                {
                    Posts = new PagedData<PostDTO>(posts, totalPostCount, page, PostPerPage)
                });
            }
        }

        public ActionResult Tag(string idAndSlug, int page = 1)
        {
            using (Db db = new Db())
            {
                var parts = SeperateIdAndSlug(idAndSlug);
                if (parts == null)
                    return HttpNotFound();

                var tag = db.Tags.Find(parts.Item1); //load data using id of slug
                if (tag == null)
                    return HttpNotFound();

                //if id matched, but slug doesn't match, display the post of the id and use the correct slug from database
                if (!tag.Slug.Equals(parts.Item2, StringComparison.CurrentCultureIgnoreCase))
                    return RedirectToRoutePermanent("tag", new { id = parts.Item1, slug = tag.Slug }); //this ensures that theres only 1 way to get to a post - for seo purposes

                var totalPostCount = tag.Posts.Count();
                var postIds = tag.Posts
                                       .OrderByDescending(t => t.CreatedAt)
                                       .Skip((page - 1) * PostPerPage)
                                       .Take(PostPerPage)
                                       .Where(t => t.DeletedAt == null)
                                       .Select(t => t.Id)
                                       .ToArray();

                var posts = db.Posts.OrderByDescending(c => c.CreatedAt).Skip((page - 1) * PostPerPage).Take(PostPerPage).ToList();

                return View(new PostsTag
                {
                    Tag = tag,
                    Posts = new PagedData<PostDTO>(posts, totalPostCount, page, PostPerPage)
                });
            }
        }

        public ActionResult Show(string idAndSlug)
        {
            var parts = SeperateIdAndSlug(idAndSlug);
            if (parts == null)
                return HttpNotFound();
            using (Db db = new Db())
            {
                var post = db.Posts.Find(parts.Item1); //load data using id of slug
                if (post == null || post.IsDeleted)
                    return HttpNotFound();

                //if id matched, but slug doesn't match, display the post of the id and use the correct slug from database
                if (!post.Slug.Equals(parts.Item2, StringComparison.CurrentCultureIgnoreCase))
                    return RedirectToRoutePermanent("Post", new { id = parts.Item1, slug = post.Slug }); //this ensures that theres only 1 way to get to a post - for seo purposes

                return View(new PostsShow
                {
                    Post = post
                });
            }
        }

        private Tuple<int, string> SeperateIdAndSlug(string idAndSlug)
        {
            var matches = Regex.Match(idAndSlug, @"^(\d+)\-(.*)?$");
            if (!matches.Success)
                return null;

            var id = int.Parse(matches.Result("$1")); //extracts the first group - (/d+)
                                                      //since regex succeeded, we can assume it's a valid in & int.parse wont fail
            var slug = matches.Result("$2"); //extract 2nd group
            return System.Tuple.Create(id, slug);
        }
    }
}