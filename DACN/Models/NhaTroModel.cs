using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models
{
    public class NhaTroModel
    {
        public int? id { get; set; }
        public int dientich { get; set; }
        public int phongngu { get; set; }
        public int lau { get; set; }
        public int nhatam { get; set; }
        public int gia { get; set; }
        public string tenbds { get; set; }
        public string tenkieubds { get; set; }
    }
}