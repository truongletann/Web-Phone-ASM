using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using PagedList;

namespace AssignmentWebPhone.Areas.User.Controllers
{
    public class ProductUserController : Controller
    {
        private OnlineShopPhoneDbContext db = new OnlineShopPhoneDbContext();
        //ProductIO productIO = new ProductIO();
        //CategoryIO categoryIO = new CategoryIO();
        // GET: ProductUser
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(string category, string search)
        {
            ViewBag.category = category;
            ViewBag.search = search;

            return View("Index");
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct product = db.tblProducts.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public PartialViewResult ProductListPartial(int? page, int? category, string search)
        {
            var pageNumber = page ?? 1;
            var pageSize = 4;

            Session["CATE"] = db.tblCategories.ToList();


            search = search ?? "";
            category = category.Equals("") ? null : category;
            ViewBag.search = search;
            if (category != null)
            {
                ViewBag.category = category;
                var productList = db.tblProducts
                                .OrderByDescending(x => x.price)
                                .Where(x => x.status == true &&
                                           (x.tblCategory.categoryID == category || category == null) &&
                                           x.name.Contains(search))
                                .ToPagedList(pageNumber, pageSize);
                return PartialView(productList);
            }
            else
            {
                var productList = db.tblProducts.OrderByDescending(x => x.price)
                    .Where(x => x.status == true && x.name.Contains(search))
                    .ToPagedList(pageNumber, pageSize);
                return PartialView(productList);
            }
        }
    }
}