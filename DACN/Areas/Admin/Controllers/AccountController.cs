using DACN.Common;
using DACN.Models;
using DACN.Models.DAO;
using DACN.Models.EF;
using DACN.Models.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DACN.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Admin/Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new AdminDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetByID(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.Username;
                    userSession.userID = user.idTK;

                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khoá.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Tài khoản của bạn không có quyền đăng nhập.");
                }
                else
                {
                    ModelState.AddModelError("", "đăng nhập không đúng.");
                }
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
        public ActionResult XoaTaiKhoan()
        {
            string Username = RouteData.Values["id"].ToString();
            // Trước khi xóa Tài Khoản phải xóa tất cả bài viết liên quan

            // Sau đó mới xóa Tài Khoản
            var HamTK = new FunctionAccount();
            HamTK.Delete(Username);
            return RedirectToAction("QLTK", "Home");
        }
        [HttpPost]
        public ActionResult ThemTaiKhoan(RegisterModel models )
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
                    user.HoTen = models.FullName;
                    user.GioiTinh = models.GioiTinh;
                    DateTime now = DateTime.Now;

                    user.NgayTao = now;

                    var result = dao.Insert(user);

                    if (result > 0)
                    {
                        var res = dao.Login(user.Username, user.Pass);
                        if (res == 1)
                        {
                            ModelState.AddModelError("", "Đăng Ký Thành Công !");
                            return RedirectToAction("QLTK", "Home");
                        }
                    }
                    else ModelState.AddModelError("", "Đăng Ký Thất Bại !");
                }
            }
            return RedirectToAction("QLTK", "Home");
        }
        public ActionResult SuaTaiKhoan()
        {
            string idTK = RouteData.Values["id"].ToString();
            int temp = Int32.Parse(idTK);
            var tk = db.TaiKhoans.Find(temp);
            return View(tk);
        }
        [HttpPost]
        public ActionResult SuaTaiKhoan(TaiKhoan tk)
        {
            string idTK = RouteData.Values["id"].ToString();
            int temp = Int32.Parse(idTK);
            if (ModelState.IsValid)
            {
                var HamTK = new FunctionAccount();
                HamTK.Update(tk, temp);
                return RedirectToAction("QLTK", "Home");
            }
            return View(tk);
        }
    }
}