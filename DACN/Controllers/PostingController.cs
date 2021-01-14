using DACN.Common;
using DACN.Models;
using DACN.Models.DAO;
using DACN.Models.EF;
using DACN.Models.Function;
using Newtonsoft.Json;
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
            var nhatro = new NhaTroDAO().GetByID(baiviet.idNT);
            var taikhoan = new UserDAO().GetByID(baiviet.idTK);
            var result = new List<DetailModel>();
            result.Add(new DetailModel
            {
                idBV = baiviet.idBV,
                tieude = baiviet.TieuDe,
                mota = baiviet.MoTa,
                ngaydang = baiviet.NgayDang.ToString().Substring(0, 10),
                hotentk = taikhoan.HoTen,
                ngaythamgia = taikhoan.NgayTao.ToString().Substring(0,10),
                email = taikhoan.Email,
                phone = taikhoan.SDT,
                gia = nhatro.Gia,
                lau = nhatro.Lau,
                phongngu = nhatro.PhongNgu,
                nhatam = nhatro.NhaTam,
                dientich = nhatro.DienTich
            });
            ViewBag.comment = db.Comments.SqlQuery("Select * from Comment c where c.idBV = " + id);
            ViewBag.Detail = result;
            ViewBag.listnew = db.BaiViets.SqlQuery("select * from BaiViet where NgayDang >= GETDATE()").ToList();
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
        public ActionResult Report(string tieude, string mota, string idbv)
        {
            var session = (DACN.Common.UserLogin)Session[DACN.Common.CommonConstants.USER_SESSION];
            BaoCao bc = new BaoCao();
            var bcdao = new ReportDao();
            bc.TieuDe = tieude;
            bc.TenBaoCao = mota;
            DateTime now = DateTime.Now;
            bc.NgayBC = now;
            bc.TrangThai = false;
            bc.idBV = Int32.Parse(idbv);
            bcdao.Insert(bc);
            return Json(new { Message = "success", JsonRequestBehavior = JsonRequestBehavior.AllowGet });

        }
        [HttpPost]
        public ActionResult SuaBaiViet(PostingModel model)
        {
            BaiViet bv = db.BaiViets.Find(model.idBV);
            NhaTro nt = db.NhaTroes.Find(bv.idNT);
            bv.TieuDe = model.TieuDe;
            bv.TieuDePhu = model.TieuDePhu;
            bv.MoTa = model.MoTa;

            nt.SoNha = model.SoNha;
            nt.Gia = model.Gia;
            nt.Lau = model.Lau;
            nt.idPhuong = model.idPhuong;
            nt.idQuan = model.idQuan;
            nt.PhongNgu = model.PhongNgu;
            nt.NhaTam = model.NhaTam;

            var bvdao = new BaiVietDAO();
            bvdao.Update(bv);
            var ntdao = new NhaTroDAO();
            int idnt = (int)bv.idNT;
            ntdao.Update(nt, idnt);

            return RedirectToAction("PostsManager","Posting");
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

        public ActionResult SaveWishlist(int id, int user_id)
        {
            var wishlist = new DanhSachYeuThich();
            wishlist.IdBV = id;
            wishlist.IdTK = user_id;
            wishlist.DaySave = DateTime.Now;
            db.DanhSachYeuThiches.Add(wishlist);
            db.SaveChanges();
            return Json(new { Message = "success", JsonRequestBehavior = JsonRequestBehavior.AllowGet });
        }
        //public ActionResult Comment(CommentModel model)
        //{
        //    var comment = new Comment();
        //    comment.UserName = model.UserName;
        //    comment.ContentComment = model.ContentComment;
        //    comment.DateComment = DateTime.Now;
        //    db.Comments.Add(comment);
        //    db.SaveChanges();
        //    return View();
        //}
        public ActionResult PostsManager()
        {
            ViewBag.TP = db.ThanhPhoes.ToList();
            ViewBag.Quan = db.Quans.ToList();
            ViewBag.Phuong = db.Phuongs.ToList();
            ViewBag.LoaiBDS = db.LoaiBDS.ToList();
            var session = (DACN.Common.UserLogin)Session[DACN.Common.CommonConstants.USER_SESSION];
            var postlist = db.BaiViets.SqlQuery("select * from BaiViet b, NhaTro n where b.idNT = n.idNT and b.idTK = "+session.userID);
            ViewBag.postlist = postlist;
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

            return RedirectToAction("PostsManager", "Posting");
        }
        public JsonResult Comment(string Message, string idbv)
        {
            var session = (DACN.Common.UserLogin)Session[DACN.Common.CommonConstants.USER_SESSION];
            TaiKhoan tk = db.TaiKhoans.Find(session.userID);
            Comment cm = new Comment();
            cm.ContentComment = Message;
            cm.DateComment =  DateTime.Now;
            cm.idBV = Int32.Parse(idbv);
            if (tk == null) cm.UserName = null;
            else cm.UserName = tk.Username;

            var commentdao = new CommentDAO();
            commentdao.Insert(cm);
            Comment temp = db.Comments.OrderByDescending(p => p.IdComment).FirstOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(temp, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetData(int idBV)
        {
            BaiViet bv = db.BaiViets.Where(x => x.idBV == idBV).SingleOrDefault();
            NhaTro nt = db.NhaTroes.Where(x => x.idNT == bv.idNT).SingleOrDefault();
            PostingModel view = new PostingModel();
            view.TieuDe = bv.TieuDe;
            view.TieuDePhu = bv.TieuDePhu;
            view.NgayDang = bv.NgayDang;
            view.DienTich = nt.DienTich;
            view.Lau = (int)nt.Lau;
            view.PhongNgu = (int)nt.PhongNgu;
            view.NhaTam = (int)nt.NhaTam;
            view.MoTa = bv.MoTa;
            view.SoNha = nt.SoNha;
            view.Gia = (int)nt.Gia;
            view.idQuan = (int)nt.idQuan;
            view.idPhuong = (int)nt.idPhuong;
            view.idBV = idBV;
            string value = string.Empty;
            value = JsonConvert.SerializeObject(view, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
    }
}