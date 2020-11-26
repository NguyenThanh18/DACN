namespace DACN.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaoCao")]
    public partial class BaoCao
    {
        [Key]
        public int idBC { get; set; }

        [StringLength(255)]
        public string TieuDe { get; set; }

        [StringLength(100)]
        public string TenBaoCao { get; set; }

        public int? idBV { get; set; }

        public virtual BaiViet BaiViet { get; set; }
    }
}
