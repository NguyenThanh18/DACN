using DACN.Common;
using DACN.Models;
using DACN.Models.DAO;
using DACN.Models.EF;
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
                    var user = dao.GetByName(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.Username;
                    userSession.userID = user.idTK;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    //ViewBag.ErrorMessage = "Đăng Nhập Thành Công !"; 
                    return RedirectToAction("HomePage", "Home");
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

            return RedirectToAction("HomePage", "Home", model);
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return RedirectToAction("HomePage", "Home");
        }
        [HttpPost]
        public ActionResult Register(RegisterModel models)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                if (dao.CheckUserName(models.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng ký đã tồn tại !");
                }
                else if (dao.CheckEmail(models.Email))
                {
                    ModelState.AddModelError("", "Email đăng ký đã tồn tại !");
                }
                else
                {
                    var user = new TaiKhoan();
                    user.Username = models.UserName;
                    user.Pass = Encryptor.MD5Hash(models.Password);
                    user.Email = models.Email;
                    user.SDT = models.Phone;
                    user.RoleTK = CommonConstants.USER_GROUP;
                    
                    var result = dao.Insert(user);

                    if (result > 0)
                    {
                        var res = dao.Login(user.Username, user.Pass);
                        if (res == 1)
                        {
                            ModelState.AddModelError("", "Đăng Ký Thành Công !");
                            return Redirect("/");
                        }
                    }
                    else ModelState.AddModelError("", "Đăng Ký Thất Bại !");
                }
            }
            return View(models);
        }
    }
}