namespace SmsMis.Models.Console.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("auditlog")]
    public partial class auditlog
    {
        public Int64 auditlogid { get; set; }

        public int userid { get; set; }

        public DateTime logdate { get; set; }

        [Required]
        [StringLength(1)]
        public string eventtype { get; set; }

        [Required]
        [StringLength(100)]
        public string tablename { get; set; }

        [Required]
        [StringLength(100)]
        public string pkrecordid { get; set; }

        [Required]
        [StringLength(100)]
        public string pkcolumnname { get; set; }

        public string Json { get; set; }

        public bool isAdmin { get; set; }

    }
}
