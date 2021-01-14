using DACN.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.DAO
{
    public class BaoCaoDao
    {
        DBContext db = new DBContext();
        public int Delete(int idBC)
        {
            var dbEntry = db.BaoCaos.Where(s => s.idBC == idBC).First();

            db.BaoCaos.Remove(dbEntry);
            db.SaveChanges();
            return idBC;
        }
    }
}