using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityModel;
using EC = EntityController.Core;

namespace MvcBlog.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog/Get/...
        public ActionResult Get(int id)
        {
            // 1. Tạo instance của controller ở tầng EntityController
            var blogCtrl = new EC.BlogController();

            // 2. Lấy blog theo BlogID
            var result = blogCtrl.ExecuteQuery($"SELECT * FROM Blogs WHERE BlogId = {id}");

            // 3. Xu ly truong hop bi loi
            if (result == null || result.Count == 0)
            {
                return HttpNotFound("Blog không tồn tại.");
            }

            return View(result.First()); // Truyền 1 blog sang View
        }

        // GET: Blog
        public ActionResult Index()
        {
            // 1. Khoi tao blogCtrl
            var blogCtrl = new EC.BlogController();
            // 2. Lay tat ca cac blog
            var blogs = blogCtrl.ExecuteQuery("SELECT * FROM Blogs");
            // 3. Tra ve no
            return View(blogs);
        }
    }
}