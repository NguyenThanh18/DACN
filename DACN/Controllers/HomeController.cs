using DACN.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace DACN.Controllers
{
    public class HomeController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult BaiVietTrongThang()
        {
            //DateTime dt = DateTime.Now;
            //var fd = new DateTime(dt.Year, dt.Month, 1);
            //var ld = fd.AddMonths(1).AddDays(-1);
            //var res = db.BaiViets.Where(s => s.NgayDang >= new DateTime(dt.Year, dt.Month, 1) && s.NgayDang <= new DateTime(dt.Year, dt.Month, ld.Day)).ToList();
            //ViewBag.SLBV = res;
            var res = db.BaiViets.SqlQuery("select count(*) as SL from BaiViet b where YEAR(b.NgayDang) = YEAR(GETDATE()) and MONTH(NgayDang) = MONTH(GETDATE())").ToList();
            ViewBag.SL = res;
            return View();
        }
    }
}