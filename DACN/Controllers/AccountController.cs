using DACN.Common;
using DACN.Models;
using DACN.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACN.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return PartialView("Login");
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var resuft = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                if (resuft == 1)
                {
                    var user = dao.GetByID(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.Username;
                    userSession.userID = user.idTK;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    //ViewBag.ErrorMessage = "Đăng Nhập Thành Công !"; 
                    return RedirectToAction("Index", "Home");
                }

                else if (resuft == 0)
                {
                    ModelState.AddModelError("", "Account does not exist !");
                }
                else if (resuft == -1)
                {
                    ModelState.AddModelError("", "Your account has been locked !");
                }
                else if (resuft == -2)
                {
                    ModelState.AddModelError("", "Password does not corret !");
                }
                else
                {
                    ModelState.AddModelError("", "Login Failed.");
                }

            }

            return PartialView("Login", model);
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}