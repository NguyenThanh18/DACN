using DACN.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.Function
{
    public class FunctionPosts
    {
        private DBContext db;
        public FunctionPosts()
        {
            db = new DBContext();
        }
        public IQueryable<BaiViet> BaiViets
        {
            get { return db.BaiViets; }
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
        public int Update(BaiViet model1, int id, NhaTro model2)
        {
            //BaiViet dbEntry = db.BaiViets.Find(id);
            //if (dbEntry == null)
            //{
            //    return null;
            //}
            
            db.SaveChanges();
            return model1.idBV;
        }
    }
}