using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models
{
    public class DetailModel
    {
        public DetailModel() { }
        public int? idBV { get; set; }
        public string tieude { get; set; }
        public string tieudephu { get; set; }
        public string mota { get; set; }
        public string ngaydang { get; set; }
        public string hotentk { get; set; }
        public string ngaythamgia { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public int? dientich { get; set; }
        public int? phongngu { get; set; }
        public int? lau { get; set; }
        public int? nhatam { get; set; }
        public int? gia { get; set; }
        public string tenbds { get; set; }
        public string tenkieubds { get; set; }
    }
}