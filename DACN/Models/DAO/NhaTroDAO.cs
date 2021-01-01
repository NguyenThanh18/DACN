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
        public NhaTro GetByID(int? id)
        {
            return dbContext.NhaTroes.Find(id);
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
        public int Update(NhaTro model, int id)
        {
            NhaTro dbEntry = dbContext.NhaTroes.Find(id);
            if (dbEntry == null)
            {
                return 0;
            }
            dbContext.SaveChanges();
            return model.idNT;
        }
    }
}