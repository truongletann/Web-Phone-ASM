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
    public class OrderAdminController : Controller
    {
        private OnlineShopPhoneDbContext db = new OnlineShopPhoneDbContext();

        // GET: Admin/OrderAdmin
        public ActionResult Index()
        {
            var tblOrders = db.tblOrders.Include(t => t.tblUser);
            return View(tblOrders.ToList());
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
