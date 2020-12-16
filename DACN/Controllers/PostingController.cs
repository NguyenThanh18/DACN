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
    public class PostingController : System.Web.Mvc.Controller
    {
        private DBContext db = new DBContext();
        // GET: Posting
        public ActionResult Index()
        {
            ViewBag.LoaiBDS = new LoaiBDSDAO().ListAll();
            return View();
        }
        //public ActionResult GetLoaiBaiViet(int? id)
        //{
        //    TempData["dataBDS"] = new LoaiDSDAO().GetListByID(id);
        //    if (TempData["dataBDS"] != null)
        //    {
        //        return RedirectToAction("ListBDS", "Posting");
        //    }
        //    string message = "success";
        //    return Json(new { Mesage = message, JsonRequestBehavior.AllowGet });
        //}
        public ActionResult ListBDS()
        {
            var list = db.BaiViets.SqlQuery("select * from BaiViet b, NhaTro n where b.idNT = n.idNT").ToList();
            //ViewBag.LoaiBDS = TempData["dataBDS"];
            ViewBag.listBV = list;
            return View();
        }
        public ActionResult DanhSachBV()
        {
            var list = db.BaiViets.SqlQuery("select * from BaiViet b, NhaTro n where b.idNT = n.idNT").ToList();
            //ViewBag.LoaiBDS = TempData["dataBDS"];
            ViewBag.listBV = list;
            return View();
        }
        public ActionResult Detail(int id)
        {
            var baiviet = new BaiVietDAO().GetByID(id);
            var nhatro = new NhaTroDAO().ListAll();
            var taikhoan = new UserDAO().GetByID(baiviet.idTK);
            var result = new List<DetailModel>();
            foreach (var item in nhatro.Where(x => x.idNT == baiviet.idNT))
            {
                result.Add(new DetailModel
                {
                    idBV = baiviet.idTK,
                    tieude = baiviet.TieuDe,
                    mota = baiviet.MoTa,
                    ngaydang = baiviet.NgayDang.ToString(),
                    hotentk = taikhoan.HoTen,
                    ngaythamgia = taikhoan.NgayTao.ToString(),
                    email = taikhoan.Email,
                    phone = taikhoan.SDT,
                });
            }
            ViewBag.Detail = result;
            return View();
        }
    }
}