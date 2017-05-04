namespace SmsMis.Models.Console.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VoucherType")]
    public partial class VoucherType
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VoucherCode { get; set; }

        [Required]
        [StringLength(75)]
        public string VoucherName { get; set; }

        [Required]
        [StringLength(10)]
        public string ShortName { get; set; }

        [Required]
        [StringLength(1)]
        public string Category { get; set; }

        [Required]
        [StringLength(1)]
        public string Frequency { get; set; }

        public bool Status { get; set; }

        [Required]
        [StringLength(25)]
        public string AddByUserId { get; set; }

        public DateTime AddDateTime { get; set; }
    }
}
