using DACN.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.DAO
{
    public class PhuongDAO
    {
        DBContext dbContext = null;
        public PhuongDAO()
        {
            dbContext = new DBContext();
        }
        public List<Phuong> GetListByID(int id)
        {
            return dbContext.Phuongs.Where(x => x.idQuan == id).ToList();
        }
        public List<Phuong> ListAll()
        {
            return dbContext.Phuongs.ToList();
        }
    }
}