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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhaTro()
        {
            BaiViets = new HashSet<BaiViet>();
            HinhAnhs = new HashSet<HinhAnh>();
        }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiViet> BaiViets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HinhAnh> HinhAnhs { get; set; }
    }
}
