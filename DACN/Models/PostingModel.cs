using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace DACN.Models
{
    public class PostingModel
    {
        public string TenTP { get; set; }
        public int idQuan { get; set; }
        public int idPhuong { get; set; }
        public string SoNha { get; set; }
        public string TenLoai { get; set; }
        public string TieuDe { get; set; }
        public string TieuDePhu { get; set; }
        public string MoTa { get; set; }
        public int Gia { get; set; }
        public int PhongNgu { get; set; }
        public int Lau { get; set; }
        public int NhaTam { get; set; }
        public int DienTich { get; set; }
        public string Images { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}