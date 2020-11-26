namespace DACN.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiBDS")]
    public partial class LoaiBD
    {
        [Key]
        public int idBDS { get; set; }

        [StringLength(50)]
        public string TenLoai { get; set; }
    }
}
