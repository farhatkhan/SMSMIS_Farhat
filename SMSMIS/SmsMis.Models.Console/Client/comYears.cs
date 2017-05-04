namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class comYears
    {
        [Key]
        public int YearID { get; set; }

        [Required]
        [StringLength(10)]
        public string Display { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        public bool closed { get; set; }

        [Column(TypeName = "date")]
        public DateTime? closeDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime OpenDate { get; set; }

        public int? OpenedBy { get; set; }

        public int? ClosedBy { get; set; }
    }
}
