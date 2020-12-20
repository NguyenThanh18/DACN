using DACN.Models;
using DACN.Models.DAO;
using DACN.Models.EF;
using DACN.Models.Function;
using System;
using System.Collections.Generic;
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
            var HamP = new FunctionPosts();
            var HamNT = new FunctionNhaTro();
            var HamBC = new ReportDao();
            HamBC.Delete(Int32.Parse(id));
            HamP.Delete(bv.idBV);
            HamNT.Delete(nt.idNT);

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
            return RedirectToAction("QLBV", "Home");
        }
    }
}