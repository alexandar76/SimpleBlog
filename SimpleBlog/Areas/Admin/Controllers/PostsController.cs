using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.Infrastructure;
using SimpleBlog.Infrastructure.Extensions;
using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTab("posts")]
    public class PostsController : Controller
    {
        private const int PostsPerPage = 5;
        // GET: Admin/Posts
        public ActionResult Index(int page = 1)
        {
            using (Db db = new Db())
            {
                var totalPostCount = db.Posts.Count();
                var currentPostPage = db.Posts.OrderByDescending(c => c.CreatedAt).Skip((page - 1) * PostsPerPage).Take(PostsPerPage).ToList();

                return View(new PostsIndex
                {
                    Posts = new PagedData<PostDTO>(currentPostPage, totalPostCount, page, PostsPerPage)
                });
            }
        }

        public ActionResult New()
        {
            using (Db db = new Db())
            {
                return View("Form", new PostsForm
                {
                    IsNew = true,
                    Tags = db.Tags.Select(tag => new TagCheckbox
                    {
                        Id = tag.Id,
                        Name = tag.Name,
                        IsChecked = false
                    }).ToList()

                });
            }
        }

        public ActionResult Edit(int id)
        {
            using (Db db = new Db())
            {
                var post = db.Posts.Find(id);
                if (post == null)
                    return HttpNotFound();


                return View("Form", new PostsForm
                {
                    IsNew = false,
                    PostId = id,
                    Content = post.Content,
                    Slug = post.Slug,
                    Title = post.Title,
                    Tags = db.Tags.Select(tag => new TagCheckbox
                    {
                        Id = tag.Id,
                        Name = tag.Name,
                        //IsChecked = false
                        IsChecked = post.Tags.Contains(tag)
                    }).ToList()
                });
            }
        }


        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Form(PostsForm form)
        {
            form.IsNew = form.PostId == null;

            if (!ModelState.IsValid)
                return View(form);

            var selectedTags = ReconsileTags(form.Tags).ToList();

            using (Db db = new Db())
            {
                PostDTO post;
                if (form.IsNew)
                {
                    post = new PostDTO
                    {
                        CreatedAt = DateTime.UtcNow,
                        //User = Auth.User,
                        
                    };

                    foreach (var tag in selectedTags)
                        post.Tags.Add(tag);
                }
                else
                {
                    post = db.Posts.Find(form.PostId);

                    if (post == null)
                        return HttpNotFound();
                    post.UpdatedAt = DateTime.UtcNow;

                    foreach (var toAdd in selectedTags.Where(t => !post.Tags.Contains(t)))
                        post.Tags.Add(toAdd);

                    foreach (var toRemove in post.Tags.Where(t => !selectedTags.Contains(t)).ToList())
                        post.Tags.Remove(toRemove);
                }

                post.Title = form.Title;
                post.Slug = form.Slug;
                post.Content = form.Content;
                post.Author = User.Identity.Name;
                if (form.IsNew)
                {
                    db.Posts.Add(post);
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Trash(int id)
        {
            using (Db db = new Db())
            {
                var post = db.Posts.Find(id);
                if (post == null)
                    return HttpNotFound();

                post.DeletedAt = DateTime.UtcNow;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (Db db = new Db())
            {
                var post = db.Posts.Find(id);
                if (post == null)
                    return HttpNotFound();

                post.DeletedAt = DateTime.UtcNow;
                db.Posts.Remove(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Restore(int id)
        {
            using (Db db = new Db())
            {
                var post = db.Posts.Find(id);
                if (post == null)
                    return HttpNotFound();

                post.DeletedAt = null;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        private IEnumerable<TagDTO> ReconsileTags(IEnumerable<TagCheckbox> tags)
        {
            foreach (var tag in tags.Where(t => t.IsChecked))
            {
                using (Db db = new Db())
                {
                    if (tag.Id != null)
                    {
                        yield return db.Tags.Find(tag.Id);
                        continue;

                    }

                    var existingTag = db.Tags.FirstOrDefault(t => t.Name == tag.Name);
                    if (existingTag != null)
                    {
                        yield return existingTag;
                        continue;
                    }

                    var newTag = new TagDTO
                    {
                        Name = tag.Name,
                        Slug = tag.Name.Slugify()
                    };

                    db.SaveChanges();
                    yield return newTag;
                }

            }
        }
    }
}