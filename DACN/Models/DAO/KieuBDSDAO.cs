using DACN.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.DAO
{
    public class KieuBDSDAO
    {
        DBContext dbContext = null;
        public KieuBDSDAO()
        {
            dbContext = new DBContext();
        }
        public List<KieuBD> GetListByID(int id)
        {
            return dbContext.KieuBDS.Where(x => x.idKieuBDS == id).ToList();
        }
        public List<KieuBD> ListAll()
        {
            return dbContext.KieuBDS.ToList();
        }
    }
}