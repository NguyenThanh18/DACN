using DACN.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACN.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult QLTK()
        {
            var qltk = db.TaiKhoans.ToList();
            ViewBag.QLTK = qltk;
            return View();
        }
    }
}