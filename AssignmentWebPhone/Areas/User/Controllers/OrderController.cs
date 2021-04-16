using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssignmentWebPhone.Common;
using Model.EF;
using PagedList;

namespace AssignmentWebPhone.Areas.User.Controllers
{
    public class OrderController : Controller
    {
        private OnlineShopPhoneDbContext db = new OnlineShopPhoneDbContext();
        // GET: Order

        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 5;
            UserLogin user = (UserLogin)Session[AssignmentWebPhone.Common.CommonConstants.USER_SESSION];
            var orderList = db.tblOrders.OrderByDescending(x => x.orderID).Where(x=>x.tblUser.userID==user.UserID).ToPagedList(pageNumber, pageSize);
            return View(orderList);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = db.tblOrders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
    }
}