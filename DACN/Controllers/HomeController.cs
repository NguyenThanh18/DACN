using DACN.Models;
using DACN.Models.DAO;
using DACN.Models.EF;
using Newtonsoft.Json;
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
        public ActionResult HomePage()
        {
            ViewBag.listBaiViet = new BaiVietDAO().ListAll();
            ViewBag.Quan = db.Quans.ToList();
            ViewBag.listKieuBDS = db.KieuBDS.ToList();
            ViewBag.Phuong = db.Phuongs.SqlQuery("select distinct * from Phuong").ToList();
            ViewBag.listnew = db.BaiViets.SqlQuery("select * from BaiViet where NgayDang >= GETDATE()").ToList();
            return View();
        }
        public ActionResult ListKieuBDS()
        {
            ViewBag.listKieuBDS = new KieuBDSDAO().ListAll();
            return PartialView();
        }
        public JsonResult listPhuong(string name)
        {
            var listPhuong = db.Phuongs.ToList();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(listPhuong, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListPhuongByID(int id)
        {
            var listPhuong = new PhuongDAO().GetListByID(id);
            return Json(listPhuong, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListPhuongByName(string name)
        {
            var listAllPhuong = new PhuongDAO().ListAll();
            var listResult = new List<List<string>>();
            name = name.ToUpper();
            string convert = "ĂÂÀẰẦÁẮẤẢẲẨÃẴẪẠẶẬỄẼỂẺÉÊÈỀẾẸỆÔÒỒƠỜÓỐỚỎỔỞÕỖỠỌỘỢƯÚÙỨỪỦỬŨỮỤỰÌÍỈĨỊỲÝỶỸỴĐăâàằầáắấảẳẩãẵẫạặậễẽểẻéêèềếẹệôòồơờóốớỏổởõỗỡọộợưúùứừủửũữụựìíỉĩịỳýỷỹỵđ";
            string To = "AAAAAAAAAAAAAAAAAEEEEEEEEEEEOOOOOOOOOOOOOOOOOUUUUUUUUUUUIIIIIYYYYYDaaaaaaaaaaaaaaaaaeeeeeeeeeeeooooooooooooooooouuuuuuuuuuuiiiiiyyyyyd";
            for (int i = 0; i < To.Length; i++)
            {
                name = name.Replace(convert[i], To[i]);
            }
            foreach (var item in listAllPhuong)
            {
                for (int i = 0; i < To.Length; i++)
                {
                    item.TenPhuong = item.TenPhuong.Replace(convert[i], To[i]);
                }
                foreach (var phuong in listAllPhuong)
                {
                    if (phuong.idPhuong == item.idPhuong)
                    {
                        listResult.Add(new List<string>
                        {
                            phuong.idPhuong.ToString(),
                            phuong.TenPhuong
                        });
                    }
                }
            }
            return Json(listResult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListQuanByName(string name)
        {
            var listAllQuan = new QuanDAO().ListAll();
            var listAllPhuong = new PhuongDAO().ListAll();
            var listResult = new List<List<string>>();
            name = name.ToUpper();
            string convert = "ĂÂÀẰẦÁẮẤẢẲẨÃẴẪẠẶẬỄẼỂẺÉÊÈỀẾẸỆÔÒỒƠỜÓỐỚỎỔỞÕỖỠỌỘỢƯÚÙỨỪỦỬŨỮỤỰÌÍỈĨỊỲÝỶỸỴĐăâàằầáắấảẳẩãẵẫạặậễẽểẻéêèềếẹệôòồơờóốớỏổởõỗỡọộợưúùứừủửũữụựìíỉĩịỳýỷỹỵđ";
            string To = "AAAAAAAAAAAAAAAAAEEEEEEEEEEEOOOOOOOOOOOOOOOOOUUUUUUUUUUUIIIIIYYYYYDaaaaaaaaaaaaaaaaaeeeeeeeeeeeooooooooooooooooouuuuuuuuuuuiiiiiyyyyyd";
            for (int i = 0; i < To.Length; i++)
            {
                name = name.Replace(convert[i], To[i]);
            }
            foreach (var item in listAllPhuong)
            {
                item.TenPhuong = item.TenPhuong.ToUpper();
                for (int i = 0; i < To.Length; i++)
                {
                    item.TenPhuong = item.TenPhuong.Replace(convert[i], To[i]);
                }
                if (item.TenPhuong == name)
                {
                    foreach (var quan in listAllQuan)
                    {
                        if (quan.idQuan == item.idQuan)
                        {
                            listResult.Add(new List<string>
                            {
                                quan.idQuan.ToString(),
                                quan.TenQuan
                            });
                        }
                    }
                }

            }
            var list = listResult;
            return Json(listResult, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(SearchModel entity)
        {
            var listBaiViet = new List<Models.EF.BaiViet>();
            //Search Quan
            if (entity.idPhuong == 0 && entity.idBDS == 0 && entity.MucGia == 0)
            {
                listBaiViet = db.BaiViets.SqlQuery("select * from NhaTro n, BaiViet b where n.idNT = b.idNT and n.idQuan =" + entity.idQuan).ToList();
            }
            //Search Phuong
            if (entity.idQuan == 0 && entity.idBDS == 0 && entity.MucGia == 0)
            {
                listBaiViet = db.BaiViets.SqlQuery("select * from NhaTro n, BaiViet b, where n.idNT = b.idNT and n.idPhuong = " + entity.idPhuong).ToList();
            }
            //Search Phuong + Quan
            if (entity.idBDS == 0 && entity.MucGia == 0)
            {
                listBaiViet = db.BaiViets.SqlQuery("select * from NhaTro n, BaiViet b where n.idNT = b.idNT and n.idPhuong = " + entity.idPhuong + " and n.idQuan = " + entity.idQuan).ToList();
            }
            //Search Phuong + Quan + Dich vu
            if (entity.MucGia == 0)
            {
                listBaiViet = db.BaiViets.SqlQuery("select * from NhaTro n, BaiViet b where n.idNT = b.idNT and idQuan = " + entity.idQuan + " and idPhuong = " + entity.idPhuong + " and idKieuBDS = " + entity.idKieuBDS).ToList();
            }
            //Search Phuong + Quan + Dich vu + Gia
            if (entity.idQuan != 0 && entity.idPhuong != 0 && entity.idKieuBDS != 0 && entity.MucGia != 0)
            {
                listBaiViet = db.BaiViets.SqlQuery("select * from NhaTro n, BaiViet b where n.idNT = b.idNT and idQuan = " + entity.idQuan + " and idPhuong = " + entity.idPhuong + " and idKieuBDS = " + entity.idKieuBDS + " and Gia = " + entity.MucGia).ToList();
            }
            //Search Dich vu
            if (entity.idQuan == 0 && entity.idPhuong == 0 && entity.MucGia == 0)
            {
                listBaiViet = db.BaiViets.SqlQuery("select * from NhaTro n, BaiViet b where n.idNT = b.idNT and n.idKieuBDS = " + entity.idKieuBDS).ToList();
            }
            //Search Muc Gia
            if (entity.idQuan == 0 && entity.idPhuong == 0 && entity.idKieuBDS == 0)
            {
                listBaiViet = db.BaiViets.SqlQuery("select * from NhaTro n, BaiViet b where n.idNT = b.idNT and n.Gia = " + entity.MucGia).ToList();
            }
            //Search Muc Gia + Dich vu
            if (entity.idQuan == 0 && entity.idPhuong == 0)
            {
                listBaiViet = db.BaiViets.SqlQuery("select * from NhaTro n, BaiViet b where n.idNT = b.idNT and n.Gia = " + entity.MucGia + " and n.idKieuBDS = " + entity.idKieuBDS).ToList();
            }
            //Search Quan + Dich Vu
            if (entity.idPhuong == 0 && entity.MucGia == 0)
            {
                listBaiViet = db.BaiViets.SqlQuery("select * from NhaTro n, BaiViet b where n.idNT = b.idNT and n.idQuan = " + entity.idQuan + " and n.idKieuBDS = " + entity.idKieuBDS).ToList();
            }
            //Search Quan + Muc Gia
            if (entity.idPhuong == 0 && entity.idKieuBDS == 0)
            {
                listBaiViet = db.BaiViets.SqlQuery("select * from NhaTro n, BaiViet b where n.idNT = b.idNT and n.idQuan = " + entity.idQuan + " and n.Gia = " + entity.MucGia).ToList();
            }
            //Search Phuong + Dich Vu
            if (entity.idQuan == 0 && entity.MucGia == 0)
            {
                listBaiViet = db.BaiViets.SqlQuery("select * from NhaTro n, BaiViet b where n.idNT = b.idNT and n.idQuan = " + entity.idQuan + " and n.idKieuBDS = " + entity.idKieuBDS).ToList();
            }
            //Search Quan + Muc Gia
            if (entity.idPhuong == 0 && entity.idKieuBDS == 0)
            {
                listBaiViet = db.BaiViets.SqlQuery("select * from NhaTro n, BaiViet b where n.idNT = b.idNT and n.idQuan = " + entity.idQuan + " and n.Gia = " + entity.MucGia).ToList();
            }
            return Json(listBaiViet, JsonRequestBehavior.AllowGet);
        }
    }
}
