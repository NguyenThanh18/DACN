﻿using DACN.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.DAO
{
    public class BaiVietDAO
    {
        DBContext db = new DBContext();
        public List<BaiViet> ListAll()
        {
            return db.BaiViets.ToList();
        }
        public BaiViet GetByID(int id)
        {
            return db.BaiViets.Find(id);
        }
        public int Delete(int idBV)
        {
            var dbEntry = db.BaiViets.Where(s => s.idBV == idBV).First();

            db.BaiViets.Remove(dbEntry);
            db.SaveChanges();
            return idBV;
        }
        public int Insert(BaiViet model)
        {
            db.BaiViets.Add(model);
            db.SaveChanges();
            return model.idBV;
        }
        public int Update(BaiViet model)
        {
            BaiViet dbEntry = db.BaiViets.Find(model.idBV);
            if (dbEntry == null)
            {
                return 0;
            }
            else
            {
                dbEntry.TieuDe = model.TieuDe;
                dbEntry.TieuDePhu = model.TieuDePhu;
                dbEntry.MoTa = model.MoTa;
            }
            db.SaveChanges();
            return model.idBV;
        }
    }
}