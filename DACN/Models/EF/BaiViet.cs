namespace DACN.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaiViet")]
    public partial class BaiViet
    {
        [Key]
        public int idBV { get; set; }

        [StringLength(255)]
        public string TieuDe { get; set; }

        [StringLength(255)]
        public string TieuDePhu { get; set; }

        public string MoTa { get; set; }

        public int? idTK { get; set; }

        public int? idNT { get; set; }

        public bool? TrangThai { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDang { get; set; }

    }
}
