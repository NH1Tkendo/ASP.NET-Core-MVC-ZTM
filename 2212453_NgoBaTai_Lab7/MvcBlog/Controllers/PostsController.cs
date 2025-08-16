using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EntityModel;

namespace MvcBlog.Controllers
{
    public class PostsController : Controller
    {
        private EF db = new EF();

        // GET: Posts
        public ActionResult Index(int? blogId, string searchString, DateTime? fromDate, DateTime? toDate, string sortOrder)
        {
            // giữ trạng thái để hiển thị lại trên view
            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentBlogId = blogId;
            ViewBag.FromDate = fromDate?.ToString("yyyy-MM-dd");
            ViewBag.ToDate = toDate?.ToString("yyyy-MM-dd");
            ViewBag.CurrentSort = sortOrder;

            // tham số sort
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var posts = from p in db.Posts.Include("Blog")
                        select p;

            // --- lọc theo BlogId ---
            if (blogId.HasValue && blogId > 0)
            {
                posts = posts.Where(p => p.BlogId == blogId.Value);
            }

            // --- lọc theo searchString ---
            if (!String.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(p => p.Title.Contains(searchString)
                                      || p.Content.Contains(searchString));
            }

            // --- lọc theo khoảng ngày ---
            if (fromDate.HasValue && toDate.HasValue)
            {
                if (toDate.Value < fromDate.Value)
                {
                    ModelState.AddModelError("", "Ngày kết thúc phải >= ngày bắt đầu.");
                }
                else
                {
                    posts = posts.Where(p => p.CreatedDate >= fromDate.Value && p.CreatedDate <= toDate.Value);
                }
            }
            else if (fromDate.HasValue)
            {
                posts = posts.Where(p => p.CreatedDate >= fromDate.Value);
            }
            else if (toDate.HasValue)
            {
                posts = posts.Where(p => p.CreatedDate <= toDate.Value);
            }

            // --- DropDownList Blog ---
            ViewBag.BlogList = new SelectList(db.Blogs.ToList(), "BlogId", "Name", blogId);

            return View(posts.ToList());
        }



        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            ViewBag.BlogId = new SelectList(db.Blogs, "BlogId", "Name");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostId,Title,Content,BlogId,CreatedDate")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BlogId = new SelectList(db.Blogs, "BlogId", "Name", post.BlogId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogId = new SelectList(db.Blogs, "BlogId", "Name", post.BlogId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostId,Title,Content,BlogId,CreatedDate")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogId = new SelectList(db.Blogs, "BlogId", "Name", post.BlogId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
