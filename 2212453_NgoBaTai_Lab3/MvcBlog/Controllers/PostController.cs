using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityModel;
using EC = EntityController.Core;

namespace MvcBlog.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            // 1. Khoi tao postCtrl
            var postCtrl = new EC.PostController();
            // 2. Lay tat ca cac post
            var posts = postCtrl.ExecuteQuery("SELECT * FROM Posts");
            // 3. Tra ve no
            return View(posts);
        }
        // GET: Post/Get/...
        public ActionResult Get(int id)
        {
            // 1. Tạo instance của controller ở tầng EntityController
            var postCtrl = new EC.PostController();

            // 2. Lấy blog theo BlogID
            var result = postCtrl.ExecuteQuery($"SELECT * FROM Posts WHERE PostId = {id}");

            // 3. Xu ly truong hop bi loi
            if (result == null || result.Count == 0)
            {
                return HttpNotFound("Post không tồn tại.");
            }

            return View(result.First()); // Truyền 1 blog sang View
        }
    }
}