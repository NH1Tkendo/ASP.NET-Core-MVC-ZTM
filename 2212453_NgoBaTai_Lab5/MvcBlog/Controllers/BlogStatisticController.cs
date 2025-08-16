using EntityModel;
using MvcBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBlog.Controllers
{
    public class BlogStatisticController : Controller
    {
        private EF db = new EF();
        public ActionResult Index()
        {
            // Gom nhóm Posts theo Blog
            var data = from post in db.Blogs
                       group post by post.Owner into g
                       select new BlogStatistic
                       {
                           Owner = g.Key,
                           BlogCount = g.Count()
                       };

            return View(data.ToList());
        }
    }
}