using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class PathsController : Controller
    {
        private MVCDatabaseEntities db = new MVCDatabaseEntities();

        // GET: Paths
        public ActionResult Index(string sortOrder, string searchString, int page = 1, int pageSize = 1)
        {
            // Lấy query cơ bản
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            // Lấy query cơ bản
            var paths = from p in db.Paths
                        where p.Display == 1 && p.Active == 1
                        select p;

            // Tìm kiếm nhiều cột
            if (!String.IsNullOrEmpty(searchString))
            {
                paths = paths.Where(p =>
                    (p.PathName != null && p.PathName.Contains(searchString)) ||
                    (p.PathDescription != null && p.PathDescription.Contains(searchString))
                );
            }


            // Sắp xếp theo OrderNo
            paths = paths.OrderBy(p => p.OrderNo);

            // Phân trang
            int totalItems = paths.Count();
            var pathList = paths.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Truyền dữ liệu ViewBag để hiển thị phân trang
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;
            ViewBag.SearchString = searchString;

            return View(pathList);
        }


        // GET: Paths/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Path path = db.Paths.Find(id);
            if (path == null)
            {
                return HttpNotFound();
            }
            return View(path);
        }

        // GET: Paths/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Paths/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PathID,PathName,PathDescription,PathURL,ImageURL,OrderNo,Display,ParentID,Active")] Path path)
        {
            if (ModelState.IsValid)
            {
                db.Paths.Add(path);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(path);
        }

        // GET: Paths/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Path path = db.Paths.Find(id);
            if (path == null)
            {
                return HttpNotFound();
            }
            return View(path);
        }

        // POST: Paths/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PathID,PathName,PathDescription,PathURL,ImageURL,OrderNo,Display,ParentID,Active")] Path path)
        {
            if (ModelState.IsValid)
            {
                db.Entry(path).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(path);
        }

        // GET: Paths/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Path path = db.Paths.Find(id);
            if (path == null)
            {
                return HttpNotFound();
            }
            return View(path);
        }

        // POST: Paths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Path path = db.Paths.Find(id);
            db.Paths.Remove(path);
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
