using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCProject.Models;

namespace MVCProject.Controllers
{
    public class AccountsController : Controller
    {
        private MVCDatabaseEntities db = new MVCDatabaseEntities();

        // GET: Accounts
        public ViewResult Index(string sortOrder, string searchString, int page = 1, int pageSize = 1)
        {
            // Thiết lập các tham số sort cho View (ví dụ theo AccountName)
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            // Lấy query cơ bản từ database
            var accounts = from a in db.Accounts
                           select a;

            // Tìm kiếm theo AccountName hoặc FullName
            if (!string.IsNullOrEmpty(searchString))
            {
                accounts = accounts.Where(a =>
                    (a.AccountName != null && a.AccountName.Contains(searchString)) ||
                    (a.FullName != null && a.FullName.Contains(searchString))
                );
            }

            // Sắp xếp
            switch (sortOrder)
            {
                case "name_desc":
                    accounts = accounts.OrderByDescending(a => a.AccountName);
                    break;
                default:
                    accounts = accounts.OrderBy(a => a.AccountName);
                    break;
            }

            // Phân trang
            int totalItems = accounts.Count();
            var accountList = accounts.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Truyền thông tin phân trang + search về View
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;
            ViewBag.SearchString = searchString;

            return View(accountList);
        }


        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountID,AccountName,Password,FullName,ImagePath,Birthday,Phone,Mobile,Email,Address,CreatedDate,LastLogin,GroupID,Level,Score,Active")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountID,AccountName,Password,FullName,ImagePath,Birthday,Phone,Mobile,Email,Address,CreatedDate,LastLogin,GroupID,Level,Score,Active")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
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
