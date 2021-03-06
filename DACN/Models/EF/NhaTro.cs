namespace DACN.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhaTro")]
    public partial class NhaTro
    {
        [Key]
        public int idNT { get; set; }

        public int? DienTich { get; set; }

        public int? PhongNgu { get; set; }

        public int? Lau { get; set; }

        public int? NhaTam { get; set; }

        public int? Gia { get; set; }

        public int? idBDS { get; set; }

        public int? idKieuBDS { get; set; }

        public int? idQuan { get; set; }

        public string SoNha { get; set; }

        public int? idPhuong { get; set; }

        public string Image { get; set; }

    }
}
