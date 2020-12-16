using DACN.Models;
using DACN.Models.EF;
using DACN.Models.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Tokenizer;

namespace DACN.Controllers
{
    public class PostController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Posting
        public ActionResult Index()
        {
            return View();
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
                var daoNT = new FunctionNhaTro();
                var daoPost = new FunctionPosts();

                var bv = new BaiViet();
                var nt = new NhaTro();

                nt.DienTich = model.DienTich;
                nt.PhongNgu = model.PhongNgu;
                nt.Lau = model.Lau;
                nt.NhaTam = model.NhaTam;
                nt.Gia = model.Gia;
                nt.SoNha = model.SoNha;
                Quan quan = db.Quans.Where(s => s.Alias.Contains(model.TenQuan)).FirstOrDefault();
                nt.idQuan = quan.idQuan;
                daoNT.Insert(nt);

                NhaTro temp = db.NhaTroes.OrderByDescending(p => p.idNT).FirstOrDefault();
                bv.TieuDe = model.TieuDe;
                bv.TieuDePhu = model.TieuDePhu;
                bv.MoTa = model.MoTa;
                bv.TrangThai = false;
                DateTime now = DateTime.Now;
                bv.NgayDang = now;
                bv.idNT = temp.idNT;
                daoPost.Insert(bv);

                    
            }
            return View("Posted",model);
        }
    }
}