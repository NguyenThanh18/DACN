namespace DACN.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Quan")]
    public partial class Quan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Quan()
        {
            Phuongs = new HashSet<Phuong>();
        }

        [Key]
        public int idQuan { get; set; }

        [StringLength(50)]
        public string TenQuan { get; set; }

        public int? idTP { get; set; }

        [StringLength(50)]
        public string Alias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Phuong> Phuongs { get; set; }

        public virtual ThanhPho ThanhPho { get; set; }
    }
}
