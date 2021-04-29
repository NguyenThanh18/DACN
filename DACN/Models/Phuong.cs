namespace DACN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Phuong")]
    public partial class Phuong
    {
        [Key]
        public int idPhuong { get; set; }

        [StringLength(50)]
        public string TenPhuong { get; set; }

        public int? idQuan { get; set; }

        [StringLength(50)]
        public string Alias { get; set; }

        public virtual Quan Quan { get; set; }
    }
}
