using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace AssignmentWebPhone.Areas.Admin.Controllers
{
    public class RoleAdminController : Controller
    {
        private OnlineShopPhoneDbContext db = new OnlineShopPhoneDbContext();

        // GET: Admin/RoleAdmin
        public ActionResult Index()
        {
            return View(db.tblRoles.ToList());
        }

        // GET: Admin/RoleAdmin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRole tblRole = db.tblRoles.Find(id);
            if (tblRole == null)
            {
                return HttpNotFound();
            }
            return View(tblRole);
        }

        // GET: Admin/RoleAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/RoleAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "roleID,name")] tblRole tblRole)
        {
            if (ModelState.IsValid)
            {
                db.tblRoles.Add(tblRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblRole);
        }

        // GET: Admin/RoleAdmin/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRole tblRole = db.tblRoles.Find(id);
            if (tblRole == null)
            {
                return HttpNotFound();
            }
            return View(tblRole);
        }

        // POST: Admin/RoleAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "roleID,name")] tblRole tblRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblRole);
        }

        // GET: Admin/RoleAdmin/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRole tblRole = db.tblRoles.Find(id);
            if (tblRole == null)
            {
                return HttpNotFound();
            }
            return View(tblRole);
        }

        // POST: Admin/RoleAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tblRole tblRole = db.tblRoles.Find(id);
            db.tblRoles.Remove(tblRole);
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
