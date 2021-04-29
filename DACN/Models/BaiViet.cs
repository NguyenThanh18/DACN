namespace DACN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaiViet")]
    public partial class BaiViet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BaiViet()
        {
            BaoCaos = new HashSet<BaoCao>();
        }

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

        public virtual NhaTro NhaTro { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaoCao> BaoCaos { get; set; }
    }
}
