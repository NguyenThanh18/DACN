namespace DACN.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HinhAnh")]
    public partial class HinhAnh
    {
        [Key]
        public int idHA { get; set; }

        public string Link { get; set; }

        public int? idNT { get; set; }

        public int? idTK { get; set; }

        public virtual NhaTro NhaTro { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
