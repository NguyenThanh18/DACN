using DACN.Models.EF;
using DACN.Models.Function;
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
            DateTime now = DateTime.Now;
            string dt = now.ToString().Substring(0, 10);
            var res = db.BaiViets.ToList();
            int sl = 0;
            int slBVChuaDuyet = 0;
            foreach (var item in res)
            {
                if (item.NgayDang.ToString().Substring(0, 10) == dt && item.TrangThai == true)
                    sl++;
                else if (item.TrangThai == false)
                    slBVChuaDuyet++;
            }
            ViewBag.SL = sl;
            ViewBag.SLBVCD = slBVChuaDuyet;
            return View();
        }
        public ActionResult QLTK()
        {
            var qltk = db.TaiKhoans.ToList();
            ViewBag.QLTK = qltk;

            return View();
        }
        public ActionResult QLBV()
        {
            var qltk = db.BaiViets.SqlQuery("select * from BaiViet b, NhaTro n where b.idNT = n.idNT").ToList();
            ViewBag.QLBV = qltk;
            
            ViewBag.TP = db.ThanhPhoes.ToList();
            ViewBag.Quan = db.Quans.ToList();
            ViewBag.Phuong = db.Phuongs.ToList();
            ViewBag.LoaiBDS = db.LoaiBDS.ToList();
            return View();
        }
        public ActionResult QLBVCD()
        {
            var qlbvcd = db.BaiViets.SqlQuery("select * from BaiViet b, NhaTro n where b.idNT = n.idNT and b.TrangThai = 0").ToList();
            ViewBag.QLBVCD = qlbvcd;
            return View();
        }
        public ActionResult QLBC()
        {
            var qltk = db.BaoCaos.ToList();
            ViewBag.QLBC = qltk;
            return View();
        }


    }
}