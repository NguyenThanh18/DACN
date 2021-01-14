namespace DACN.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        [Key]
        public int IdComment { get; set; }

        [StringLength(100)]
        public string ContentComment { get; set; }

        [StringLength(30)]
        public string UserName { get; set; }

        public DateTime? DateComment { get; set; }

        public int? idBV { get; set; }
    }
}
