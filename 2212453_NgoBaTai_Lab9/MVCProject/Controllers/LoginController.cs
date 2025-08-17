using MVCProject.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCProject.Controllers
{
    public class LoginController : Controller
    {
        private MVCDatabaseEntities db = new MVCDatabaseEntities();
        public ActionResult Index(string username = "", string password = "")
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return View();

            //string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
            var account = db.Accounts.SingleOrDefault(p =>
                p.AccountName == username &&
                p.Password == password &&
                p.Active == 1);

            if (account == null)
            {
                ViewBag.Error = "Tên tài khoản hoặc mật khẩu không chính xác";
                return View();
            }

            Session["Account"] = account.AccountID; // chỉ lưu Id
            account.LastLogin = DateTime.Now;
            db.Entry(account).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "Paths");
        }

    }
}