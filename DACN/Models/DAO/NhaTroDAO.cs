using DACN.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.DAO
{
    public class NhaTroDAO
    {
        DBContext dbContext = null;
        public NhaTroDAO()
        {
            dbContext = new DBContext();
        }
        public List<NhaTro> ListAll()
        {
            return dbContext.NhaTroes.ToList();
        }
        public int Delete(int id)
        {
            var dbEntry = dbContext.NhaTroes.Where(s => s.idNT == id).First();

            dbContext.NhaTroes.Remove(dbEntry);
            dbContext.SaveChanges();
            return id;
        }
        public int Insert(NhaTro model)
        {
            dbContext.NhaTroes.Add(model);
            dbContext.SaveChanges();
            return model.idNT;
        }
        public string Update(TaiKhoan model, int id)
        {
            TaiKhoan dbEntry = dbContext.TaiKhoans.Find(id);
            if (dbEntry == null)
            {
                return null;
            }
            dbContext.SaveChanges();
            return model.Username;
        }
    }
}