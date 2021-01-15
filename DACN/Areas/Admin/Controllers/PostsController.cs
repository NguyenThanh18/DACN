using DACN.Models;
using DACN.Models.DAO;
using DACN.Models.EF;
using DACN.Models.Function;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace DACN.Areas.Admin.Controllers
{
    public class PostsController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Admin/Posts
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult XoaBaiViet()
        {
            string id = RouteData.Values["id"].ToString();
            // Trước khi xóa bài viết sau đó xóa nhà trọ
            var bv = db.BaiViets.Find(Int32.Parse(id));
            var nv = db.NhaTroes.Find(bv.idNT);
            var HamP = new FunctionPosts();
            var HamNT = new FunctionNhaTro();
            HamP.Delete(Int32.Parse(id));
            HamNT.Delete(nv.idNT);

            return RedirectToAction("QLBV", "Home");
        }
        public ActionResult XoaBaoCao()
        {
            string id = RouteData.Values["id"].ToString();
            // Trước khi xóa bài viết sau đó xóa nhà trọ
            var bc = db.BaoCaos.Find(Int32.Parse(id));
            var bv = db.BaiViets.Find(bc.idBV);
            var nt = db.NhaTroes.Find(bv.idNT);
            var HamBC = new ReportDao();
            HamBC.Delete(Int32.Parse(id));

            return RedirectToAction("QLBC", "Home");
        }
        public ActionResult XemBaiViet()
        {
            return View();
        }
        public ActionResult DuyetBaiViet()
        {
            string id = RouteData.Values["id"].ToString();
            BaiViet bv = db.BaiViets.Find(Int32.Parse(id));
            if (bv.TrangThai == true)
            {
                bv.TrangThai = false;
            }
            else
            {
                bv.TrangThai = true;
            }
            return RedirectToAction("QLBV", "Home");
        }
        [HttpPost]
        public ActionResult SuaBaiViet(DetailModel model)
        {
            var bv = db.BaiViets.Find(model.idBV);
            var nt = new NhaTroDAO().GetByID(bv.idNT);
            bv.TieuDe = model.tieude;
            bv.TieuDePhu = model.tieudephu;
            bv.MoTa = model.mota;
            nt.Lau = model.lau;
            nt.NhaTam = model.nhatam;
            nt.PhongNgu = model.phongngu;
            nt.DienTich = model.dientich;
            var bvdao = new BaiVietDAO();
            bvdao.Update(bv);
            var ntdao = new NhaTroDAO();
            int idnt = (int)bv.idNT;
            ntdao.Update(nt, idnt);
            return RedirectToAction("QLBV", "Home");
        }
        public ActionResult SuaBaiViet(int idbv)
        {
            var bv = db.BaiViets.Find(idbv);
            var nt = new NhaTroDAO().GetByID(bv.idNT);
            var tk = new UserDAO().GetByID(bv.idTK);
            var result = new List<DetailModel>();
            result.Add(new DetailModel
            {
                idBV = bv.idBV,
                tieude = bv.TieuDe,
                tieudephu = bv.TieuDePhu,
                mota = bv.MoTa,
                ngaydang = bv.NgayDang.ToString().Substring(0, 10),
                hotentk = tk.HoTen,
                ngaythamgia = tk.NgayTao.ToString().Substring(0, 10),
                email = tk.Email,
                phone = tk.SDT,
                gia = nt.Gia,
                lau = nt.Lau,
                phongngu = nt.PhongNgu,
                nhatam = nt.NhaTam,
                dientich = nt.DienTich

            });
            ViewBag.TP = db.ThanhPhoes.ToList();
            ViewBag.Quan = db.Quans.ToList();
            ViewBag.Phuong = db.Phuongs.ToList();
            ViewBag.LoaiBDS = db.LoaiBDS.ToList();
            ViewBag.Detail = result;
            return View(result);
        }
        public ActionResult Posting()
        {
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