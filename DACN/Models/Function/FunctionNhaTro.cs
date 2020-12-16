using DACN.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.Function
{
    public class FunctionNhaTro
    {
        private DBContext db;
        public FunctionNhaTro()
        {
            db = new DBContext();
        }
        public IQueryable<NhaTro> NhaTros
        {
            get { return db.NhaTroes; }
        }
        public int Delete(int id)
        {
            var dbEntry = db.NhaTroes.Where(s => s.idNT == id).First();

            db.NhaTroes.Remove(dbEntry);
            db.SaveChanges();
            return id;
        }
        public int Insert(NhaTro model)
        {
            db.NhaTroes.Add(model);
            db.SaveChanges();
            return model.idNT;
        }
        public string Update(TaiKhoan model, int id)
        {
            TaiKhoan dbEntry = db.TaiKhoans.Find(id);
            if (dbEntry == null)
            {
                return null;
            }
            db.SaveChanges();
            return model.Username;
        }
    }
}