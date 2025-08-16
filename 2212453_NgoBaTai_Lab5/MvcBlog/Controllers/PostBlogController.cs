using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EntityModel;
using MvcBlog.Models;

namespace MvcBlog.Controllers
{
    public class PostBlogController : Controller
    {
        private EF db = new EF();
        public ActionResult Index()
        {
            //var posts = db.Posts.Include(p => p.Blog);
            var query = from p in db.Posts
                        join b in db.Blogs on p.BlogId equals b.BlogId
                        select new PostBlog
                        {
                            PostId = p.PostId,
                            Title = p.Title,
                            Content = p.Content,
                            BlogId = b.BlogId,
                            Name = b.Name
                        };
            return View(query.ToList());
        }
    }
}