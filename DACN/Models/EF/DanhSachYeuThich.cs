namespace DACN.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhSachYeuThich")]
    public partial class DanhSachYeuThich
    {
        [Key]
        public int IdBVYT { get; set; }

        public int? IdTK { get; set; }

        public int? IdBV { get; set; }

        public DateTime? DaySave { get; set; }
    }
}
