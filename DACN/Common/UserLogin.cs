using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Common
{
    public class UserLogin
    {
        public long userID { get; set; }
        public string UserName { get; set; }
        public string HoTen { get; set; }
        public string GroupID { set; get; }
    }
}