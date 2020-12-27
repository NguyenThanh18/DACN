using DACN.Common;
using DACN.Models;
using DACN.Models.DAO;
using DACN.Models.EF;
using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult Posting()
        {
            var session = (DACN.Common.UserLogin)Session[DACN.Common.CommonConstants.USER_SESSION];
            TaiKhoan tk = db.TaiKhoans.Find(session.userID);
            if (tk.QuyenHan.Equals("view"))
            {
                return View("XacThuc",tk);
            }
            ViewBag.TP = db.ThanhPhoes.ToList();
            ViewBag.Quan = db.Quans.ToList();
            ViewBag.Phuong = db.Phuongs.ToList();
            ViewBag.LoaiBDS = db.LoaiBDS.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Posting(PostingModel model)
        {

            if (ModelState.IsValid)
            {
                if (model.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                    //string extension = Path.GetExtension(model.ImageUpload.FileName);
                    //file.SaveAs(Server.MapPath("~/Content/Images/" + file.FileName));
                    model.Images = "~/Content/Images/" + fileName;
                }
                var daoNT = new NhaTroDAO();
                var daoPost = new BaiVietDAO();

                var bv = new BaiViet();
                var nt = new NhaTro();

                nt.DienTich = model.DienTich;
                nt.PhongNgu = model.PhongNgu;
                nt.Lau = model.Lau;
                nt.NhaTam = model.NhaTam;
                nt.Gia = model.Gia;
                nt.SoNha = model.SoNha;
                nt.idPhuong = model.idPhuong;
                nt.Image = model.Images;
                nt.idQuan = model.idQuan;
                daoNT.Insert(nt);

                NhaTro temp = db.NhaTroes.OrderByDescending(p => p.idNT).FirstOrDefault();
                bv.TieuDe = model.TieuDe;
                bv.TieuDePhu = model.TieuDePhu;
                bv.MoTa = model.MoTa;
                bv.TrangThai = false;
                DateTime now = DateTime.Now;
                bv.NgayDang = now;
                bv.idNT = temp.idNT;
                var session = (DACN.Common.UserLogin)Session[DACN.Common.CommonConstants.USER_SESSION];
                string ten = session.UserName;
                TaiKhoan tk = db.TaiKhoans.Find(session.userID);
                bv.idTK = tk.idTK;
                daoPost.Insert(bv);


            }
            return View("Posted", model);
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            file.SaveAs(Server.MapPath("~/Content/Images/" + file.FileName));
            return "/Content/Images" + file.FileName;
        }
    }
}