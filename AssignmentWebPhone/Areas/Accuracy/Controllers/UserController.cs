using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssignmentWebPhone.Areas.Accuracy.Data;
using AssignmentWebPhone.Common;
using Model.Dao;
using Model.EF;
using BotDetect.Web.Mvc;

namespace AssignmentWebPhone.Areas.Accuracy.Controllers
{
    public class UserController : Controller
    {
        // GET: Accuracy/User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return RedirectToAction("Index", "ProductUser", new { area = "User"});
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var result = dao.Login(model.UserID, Encryptor.MD5Hash(model.Password));
                if (result)
                {
                    var user = dao.GetByID(model.UserID);
                    var userSession = new UserLogin();
                    userSession.UserID = user.userID;
                    userSession.UserName = user.name;
                    userSession.Address = user.address;
                    userSession.RoleID = user.roleID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    Session["CART"] = null;
                    if (user.roleID.Equals("AD"))
                    {
                        return RedirectToAction("Index", "ProductAdmin", new { area = "Admin" });
                    }
                    else if(user.roleID.Equals("US"))
                    {
                        return RedirectToAction("Index", "ProductUser", new { area = "User" });
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("", "Error Login");
                }
            }
            return View();
        }

        [HttpPost]
        [CaptchaValidationActionFilter("CaptchaCode", "RegistrationCaptcha",
    "Incorrect CAPTCHA Code!")]

        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                if (dao.CheckUserID(model.UserName))
                {
                    ModelState.AddModelError("", "UserName has exits");
                }else if (dao.CheckEmail(model.EmailAddress))
                {
                    ModelState.AddModelError("", "Email has exits");
                }
                else
                {
                    var user = new tblUser();
                    user.userID = model.UserName;
                    user.name = model.FullName;
                    user.email = model.EmailAddress;
                    user.password = Encryptor.MD5Hash(model.Password);
                    user.address = model.Address;
                    user.roleID = "US";
                    string result = dao.Insert(user);
                    if (result.Length>0)
                    {
                        ViewBag.Success = "Registration success";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Registration failed");                       
                    }
                }

            }
            MvcCaptcha.ResetCaptcha("RegistrationCaptcha");
            return View(model);
        }
    }
}