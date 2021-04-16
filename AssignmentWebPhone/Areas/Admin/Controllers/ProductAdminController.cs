using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.Dao;
using System.IO;

namespace AssignmentWebPhone.Areas.Admin.Controllers
{
    public class ProductAdminController : Controller
    {
        private OnlineShopPhoneDbContext db = new OnlineShopPhoneDbContext();

        // GET: Admin/ProductAdmin
        public ActionResult Index(int page = 1,int pagesize=10, string SearchString = null)
        {
            var dao = new ProductDAO();
            ViewBag.SearchString = SearchString;
            var model = dao.ListAllPaging(page, pagesize, SearchString);
            //var tblProducts = db.tblProducts.Include(t => t.tblCategory); 
            return View(model);
        }

        // GET: Admin/ProductAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct tblProduct = db.tblProducts.Find(id);
            if (tblProduct == null)
            {
                return HttpNotFound();
            }
            return View(tblProduct);
        }

        // GET: ProductAdmin/Create
        public ActionResult Create()
        {
            ViewBag.categoryID = new SelectList(db.tblCategories, "categoryID", "name");
            return View();
        }

        // POST: ProductAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "phoneID,name,Description,quantity,price,categoryID")] tblProduct tblProduct, HttpPostedFileBase imgfile)
        {
            if (ModelState.IsValid)
            {
                string path = uploadimage(imgfile, "create");
                if (path.Equals("-1"))
                {

                }
                else
                {
                    tblProduct.imagePath = path;
                    tblProduct.status = true;
                    db.tblProducts.Add(tblProduct);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.categoryID = new SelectList(db.tblCategories, "categoryID", "name", tblProduct.categoryID);
            return View(tblProduct);
        }
        public string uploadimage(HttpPostedFileBase imgfile, string statusEvent)
        {
            Random r = new Random();
            string path = "-1";
            int radom = r.Next();
            if (imgfile != null && imgfile.ContentLength > 0)
            {
                string extension = Path.GetExtension(imgfile.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Content/upload"), radom + Path.GetFileName(imgfile.FileName));
                        imgfile.SaveAs(path);
                        path = "/Content/upload/" + radom + Path.GetFileName(imgfile.FileName);

                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable.....'); </script>");
                }
            }
            else if (statusEvent.Equals("create"))
            {
                Response.Write("<script>alert('Please select a file'); </script>");
            }
            return path;
        }

        // GET: ProductAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct tblProduct = db.tblProducts.Find(id);
            if (tblProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryID = new SelectList(db.tblCategories, "categoryID", "name", tblProduct.categoryID);
            return View(tblProduct);
        }

        // POST: ProductAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "phoneID,name,Description,quantity,price,categoryID,imagePath,status")] tblProduct tblProduct, HttpPostedFileBase imgfile)
        {
            if (ModelState.IsValid)
            {
                string path = uploadimage(imgfile, "edit");
                if (path.Equals("-1"))
                {
                    db.Entry(tblProduct).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    tblProduct.imagePath = path;
                    db.Entry(tblProduct).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            ViewBag.categoryID = new SelectList(db.tblCategories, "categoryID", "name", tblProduct.categoryID);
            return View(tblProduct);
        }

        // GET: Admin/ProductAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct tblProduct = db.tblProducts.Find(id);
            if (tblProduct == null)
            {
                return HttpNotFound();
            }
            return View(tblProduct);
        }

        // POST: Admin/ProductAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblProduct tblProduct = db.tblProducts.Find(id);
            db.tblProducts.Remove(tblProduct);
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
