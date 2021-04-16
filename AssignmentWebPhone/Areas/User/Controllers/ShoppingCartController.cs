using AssignmentWebPhone.Common;
using Model.EF;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AssignmentWebPhone.Areas.User.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: User/ShoppingCart
        private OnlineShopPhoneDbContext db = new OnlineShopPhoneDbContext();
        private string strCart = "CART";
        private Payment payment;
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderNow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            if (Session[strCart] == null)
            {
                List<Data.Item> lsItem = new List<Data.Item>
                {
                    new Data.Item(db.tblProducts.Find(id),1)
                };
                Session[strCart] = lsItem;
            }
            else
            {
                List<Data.Item> lsItem = (List<Data.Item>)Session[strCart];
                int check = IsExistingCheck(id);
                if (check == -1)
                    lsItem.Add(new Data.Item(db.tblProducts.Find(id), 1));
                else
                    lsItem[check].Quantity++;
                Session[strCart] = lsItem;
            }
            return RedirectToAction("Index", "ProductUser");
        }
        private int IsExistingCheck(int? id)
        {
            List<Data.Item> lsItem = (List<Data.Item>)Session[strCart];
            for (int i = 0; i < lsItem.Count; i++)
            {
                if (lsItem[i].Product.phoneID == id) return i;
            }
            return -1;
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            int check = IsExistingCheck(id);
            List<Data.Item> lsItem = (List<Data.Item>)Session[strCart];
            lsItem.RemoveAt(check);
            return View("Index");
        }

        public ActionResult UpdateCart(FormCollection frc)
        {
            string[] quantities = frc.GetValues("quantity");
            List<Data.Item> lstItem = (List<Data.Item>)Session[strCart];
            for (int i = 0; i < lstItem.Count; i++)
            {
                lstItem[i].Quantity = Convert.ToInt32(quantities[i]);
            }
            Session[strCart] = lstItem;
            return View("Index");
        }
        public ActionResult CheckOut(string paymentMethod)
        {
            string outOfProcut = "";
            bool isEnough = true;
            foreach (var item in (List<Data.Item>)Session[strCart])
            {
                int quantityDB = db.tblProducts.Find(item.Product.phoneID).quantity;
                if (quantityDB < item.Quantity)
                {
                    isEnough = false;
                    outOfProcut += item.Product.name + " have " + quantityDB + " left\n";
                }
            }
            if (isEnough)
            {
                if (paymentMethod.Equals("Cash"))
                {
                    return RedirectToAction("ProcessOrder");
                }
                else if (paymentMethod.Equals("PayPal"))
                {
                    return RedirectToAction("PaymentWithPayPal");
                }

            }
            ViewBag.outOfProduct = outOfProcut;
            return View("Index");
        }
        public ActionResult ProcessOrder()
        {
            List<Data.Item> lstCart = (List<Data.Item>)Session[strCart];
            UserLogin user = (UserLogin) Session[AssignmentWebPhone.Common.CommonConstants.USER_SESSION];
            float total = float.Parse(lstCart.Sum(x => x.Quantity * x.Product.price).ToString());
            //1. Save the order into Order table
            var order = new Model.EF.tblOrder
            {

                userID = user.UserID,
                total = total,
                dateBuy = DateTime.Now,
                address = user.Address
            };
            db.tblOrders.Add(order);
            db.SaveChanges();

            //2. Save the order detail into Order Detail table
            foreach (Data.Item cart in lstCart)
            {
                tblOrderDetail orderDetail = new tblOrderDetail()
                {
                    orderID = order.orderID,
                    phoneID = cart.Product.phoneID,
                    quantity = cart.Quantity,
                    price = cart.Product.price

                };
                db.tblProducts.Find(cart.Product.phoneID).quantity -= cart.Quantity;
                db.tblOrderDetails.Add(orderDetail);
                db.SaveChanges();
            }
            //3. Remove shopping cart session
            Session.Remove(strCart);
            return View("OrderSuccess");
        }
        public ActionResult PaymentWithPayPal()
        {
            var apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    // Creating a payment
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/User/ShoppingCart/PaymentWithPaypal?";
                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = CreatePayment(apiContext, baseURI + "guid=" + guid);

                    // Get links returned from paypal response to create call funciton
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = string.Empty;

                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = link.href;
                        }
                    }

                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This one will be executed when we have received all the payment params from previous call
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        //Remove shopping cart session
                        // Session.Remove(strCart);
                        return View("Failure");
                    }
                }
            }
            catch (Exception ex)
            {
                //Remove shopping cart session
                //  Session.Remove(strCart);
                return View("Failure");
            }
            //Remove shopping cart session
            //Session.Remove(strCart);
            return RedirectToAction("ProcessOrder");
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var listItems = new ItemList() { items = new List<PayPal.Api.Item>() };

            List<Data.Item> listCarts = (List<Data.Item>)Session[strCart];
            foreach (var cart in listCarts)
            {
                listItems.items.Add(new PayPal.Api.Item()
                {
                    name = cart.Product.name,
                    currency = "USD",
                    price = cart.Product.price.ToString(),
                    quantity = cart.Quantity.ToString(),
                    sku = "sku"
                });
            }

            var payer = new Payer() { payment_method = "paypal" };

            // Do the configuration RedirectURLs here with redirectURLs object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            // Create details object
            var details = new Details()
            {
                tax = "1",
                shipping = "2",
                subtotal = listCarts.Sum(x => x.Quantity * x.Product.price).ToString()
            };

            // Create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = (Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString(),// tax + shipping + subtotal
                details = details
            };

            // Create transaction
            var transactionList = new List<Transaction>();
            transactionList.Add(new Transaction()
            {
                description = "Assigment PRN292 transaction description",
                invoice_number = "SE" + Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = listItems
            });

            payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            return payment.Create(apiContext);
        }
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            payment = new Payment() { id = paymentId };
            return payment.Execute(apiContext, paymentExecution);
        }

    }
}