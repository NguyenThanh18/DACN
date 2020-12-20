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
        [Key]
        public int idQuan { get; set; }

        [StringLength(50)]
        public string TenQuan { get; set; }

        public int? idTP { get; set; }

    }
}
