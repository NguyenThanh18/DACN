namespace DACN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KieuBDS")]
    public partial class KieuBD
    {
        [Key]
        public int idKieuBDS { get; set; }

        [StringLength(50)]
        public string TenKieu { get; set; }
    }
}
